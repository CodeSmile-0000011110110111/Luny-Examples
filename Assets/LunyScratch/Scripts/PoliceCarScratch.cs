using LunyScratch.Unity;
using System;
using UnityEditor;
using UnityEngine;
using static LunyScratch.Actions;

//using static LunyScratch.Sequence;

namespace LunyScratch
{
	public sealed class PoliceCarScratch : MonoBehaviour
	{
		[SerializeField] [Range(0.001f, 1f)] private Single acceleration = 0.012f;

		private Single speed;
		private Vector3 moveDirection;

		private void Start()
		{
			var lights = GetComponentsInChildren<Light>();
			moveDirection = transform.forward;

			Sequence.Run(
				Say("3"),
				Wait(1),
				Say("2"),
				Wait(1),
				Say("1"),
				Wait(1),
				Say("GO!"),
				PlaySound("gogogo"),
				RepeatForever(MoveCar)
			);

			var light0 = new UnityEngineObject(lights[0]);
			var light1 = new UnityEngineObject(lights[1]);
			Sequence.RepeatForever(
				Enable(light0),
				Disable(light1),
				Wait(0.12),
				Disable(light0),
				Enable(light1),
				Wait(0.1)
			);
		}

		private void MoveCar()
		{
			speed += acceleration;
			var rb = GetComponentInChildren<Rigidbody>();
			rb.linearVelocity += moveDirection * speed;
		}
	}
}
