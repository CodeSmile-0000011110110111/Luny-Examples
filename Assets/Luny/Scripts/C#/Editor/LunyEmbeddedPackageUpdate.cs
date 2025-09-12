using System;
using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using CompressionLevel = System.IO.Compression.CompressionLevel;

namespace CodeSmileEditor.Luny.Install
{
	/// <summary>
	/// Menu item to manually update the Luny package in the project. The Luny package is embedded
	/// since Package Manager will not resolve git URLs automatically. But I want the examples project
	/// to work out of the box, and to avoid breaking anything due to a Luny version mismatch.
	/// </summary>
	internal static class LunyEmbeddedPackageUpdate
	{
		private const String PackageName = "de.codesmile.luny";
		private const String LocalPackagePath = "P:/" + PackageName;
		private const String UpdateMenuItemName = "Window/CodeSmile/Update embedded Luny package";
		private const String RemoveMenuItemName = "Window/CodeSmile/Remove embedded Luny package";

		private static Boolean IsLocalPackageAvailable => Directory.Exists(LocalPackagePath);

		[MenuItem(UpdateMenuItemName)]
		private static void UpdateMenuItem() => ForceRecompile(UpdateEmbeddedPackage());

		[MenuItem(UpdateMenuItemName, true)]
		private static Boolean UpdateMenuItemValidate() => IsLocalPackageAvailable;

		[MenuItem(RemoveMenuItemName)]
		private static void RemoveMenuItem() => ForceRecompile(RemoveEmbeddedPackage());

		[MenuItem(RemoveMenuItemName, true)]
		private static Boolean RemoveMenuItemValidate() => IsLocalPackageAvailable;

		private static void ForceRecompile(String packagePath)
		{
			if (String.IsNullOrEmpty(packagePath))
				return;

			AssetDatabase.ImportAsset(packagePath, ImportAssetOptions.ImportRecursive | ImportAssetOptions.ForceUpdate);
			CompilationPipeline.RequestScriptCompilation(RequestScriptCompilationOptions.CleanBuildCache);
		}

		private static String RemoveEmbeddedPackage()
		{
			if (!IsLocalPackageAvailable)
				return null;

			var embeddedPackagePath = $"./Packages/{PackageName}/";

			// remove existing
			if (Directory.Exists(embeddedPackagePath))
			{
				Debug.Log($"Backup & delete embedded package: {embeddedPackagePath}");

				// zip it up just in case changes were made to the embedded package
				BackupEmbeddedPackage(embeddedPackagePath);
				Directory.Delete(embeddedPackagePath, true);
			}

			return embeddedPackagePath;
		}

		private static String UpdateEmbeddedPackage()
		{
			if (IsLocalPackageAvailable == false)
				return null;

			var embeddedPackagePath = RemoveEmbeddedPackage();
			Debug.Log($"Updating embedded package: {embeddedPackagePath}");

			Directory.CreateDirectory(embeddedPackagePath);

			var packageInfo = new DirectoryInfo(LocalPackagePath);
			var files = packageInfo.GetFiles();
			var directories = packageInfo.GetDirectories();

			foreach (var file in files)
			{
				if (file.Name[0] == '.' || file.Name.StartsWith("CopyLuaCSharpDLLs"))
					continue;

				//Debug.Log($"file: {file.FullName}, {file.Name}");
				FileUtil.CopyFileOrDirectory(file.FullName, embeddedPackagePath + file.Name);
			}

			foreach (var dir in directories)
			{
				if (dir.Name[0] == '.')
					continue;

				//Debug.Log($"dir: {dir.FullName}, {dir.Name}");
				FileUtil.CopyFileOrDirectory(dir.FullName, embeddedPackagePath + dir.Name);
			}

			return embeddedPackagePath;
		}

		private static void BackupEmbeddedPackage(String embeddedPackagePath)
		{
			var now = DateTime.Now;
			var zipFile = Path.GetFullPath(
				$"P:/LunyExamplesEmbeddedPackage_{now.Year}-{now.Month:D2}-{now.Day:D2}__{now.Hour:D2}-{now.Minute:D2}-{now.Second:D2}.zip");
			var sourcePath = Path.GetFullPath(embeddedPackagePath);
			Debug.Log($"Zipping {sourcePath} to {zipFile} ..");
			ZipFile.CreateFromDirectory(sourcePath, zipFile, CompressionLevel.Fastest, false);
		}
	}
}
