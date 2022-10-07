namespace Simofun.Unity.Genre.ArcadeIdle
{
    public interface IActivate
    {
        #region Methods
        void Activate();

        void Deactivate();
        #endregion
    }

    public interface IActivate<T> : IActivate
    {
        #region Methods
        void Activate(T parameter);

        void Deactivate(T parameter);
        #endregion
    }

    public interface IBoolActivate : IActivate<bool>
    {
    }
}
