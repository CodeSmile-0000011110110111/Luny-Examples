using System;
using System.IO;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

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
		private const String MenuItemName = "Window/CodeSmile/Update embedded Luny package";

		[MenuItem(MenuItemName)]
		private static void MenuItem() => UpdateEmbeddedPackage();

		[MenuItem(MenuItemName, true)]
		private static Boolean IsLocalLunyPackageAvailable() => Directory.Exists(LocalPackagePath);

		private static void UpdateEmbeddedPackage()
		{
			if (IsLocalLunyPackageAvailable() == false)
				return;

			var embeddedPackagePath = $"./Packages/{PackageName}/";
			Debug.Log($"Updating embedded package: {embeddedPackagePath}");

			// remove existing
			if (Directory.Exists(embeddedPackagePath))
				Directory.Delete(embeddedPackagePath, true);

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

			AssetDatabase.ImportAsset(embeddedPackagePath, ImportAssetOptions.ImportRecursive);
			CompilationPipeline.RequestScriptCompilation();
		}
	}
}
