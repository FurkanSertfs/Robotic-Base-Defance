namespace Simofun.Unity.Genre.ArcadeIdle.Input
{
	using System;
	using UnityEngine;

	public interface IInput
	{
		#region Events
		event Action OnDown;

		event Action OnHold;

		event Action OnRelease;
		#endregion

		#region Properties
		Vector2 Delta { get; }

		Vector2 Direction { get; }

		Vector2 Position { get; }

		Vector3 WorldPosition { get; }
		#endregion
	}
}
