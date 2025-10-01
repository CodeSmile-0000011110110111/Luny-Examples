// PoliceCarConfig.cs
using System;

using static LunyScratch.Actions;

namespace LunyScratch
{
	// Pure data class - engine agnostic
	[Serializable]
	public class PoliceCarConfig
	{
		public Single Acceleration = 0.012f;
	}

	// Engine-agnostic implementation
	public class PoliceCarActor : IScratchActor
	{
		private readonly PoliceCarConfig _config;
		private readonly IActorContext _context;
		private readonly IRigidbody _rigidbody;
		private readonly IEngineObject[] _lights;

		private Single _speed;
		private IVector3 _moveDirection;

		public PoliceCarActor(PoliceCarConfig config, IActorContext context, IRigidbody rigidbody, IEngineObject[] lights)
		{
			_config = config;
			_context = context;
			_rigidbody = rigidbody;
			_lights = lights;
		}

		public void OnStart()
		{
			_moveDirection = _context.Forward;

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

			if (_lights.Length >= 2)
			{
				Sequence.Run(
					RepeatForever(
						Enable(_lights[0]),
						Disable(_lights[1]),
						Wait(0.12f),
						Disable(_lights[0]),
						Enable(_lights[1]),
						Wait(0.1f)
					)
				);
			}
		}

		private void MoveCar()
		{
			_speed += _config.Acceleration;
			var velocity = _rigidbody.LinearVelocity;
			velocity.X += _moveDirection.X * _speed;
			velocity.Y += _moveDirection.Y * _speed;
			velocity.Z += _moveDirection.Z * _speed;
			_rigidbody.LinearVelocity = velocity;
		}
	}
}
