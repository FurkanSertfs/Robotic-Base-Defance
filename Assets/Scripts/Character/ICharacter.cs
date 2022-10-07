namespace Simofun.Unity.Genre.ArcadeIdle.Controller
{
    public interface ICharacter : IBoolActivate
    {
        ICharacterWrapper Wrapper { get; }
    }
}
