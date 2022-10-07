namespace Simofun.Unity.Genre.ArcadeIdle.Controller
{
	public interface ICharacterWrapper
	{
		#region Properties
		

		

		CharacterMovementController MovementController { get; }


		ICharacter Character { get; }
		#endregion
	}
}
