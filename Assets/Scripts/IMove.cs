
namespace Simofun.Unity.Genre.ArcadeIdle
{
	using UnityEngine;

	public interface IMove : IActivate
	{
		#region Properties
		float Speed { get; }

		Vector3 Velocity { get; }
		#endregion

		#region Public Methods
		void Move(Vector3 direction);

		void Stop();
		#endregion
	}
}
