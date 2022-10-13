using System;

	public interface ICharacterDetector : IActivate
	{
		#region Events
		event Action<ICharacter> OnCharacterEnter;

		event Action<ICharacter> OnCharacterExit;

		event Action<ICharacter> OnCharacterStay;
		#endregion

		#region Methods
		bool ResetCharacterStayTime(ICharacter character);

		float GetCharacterStayTime(ICharacter character);
		#endregion
	}

