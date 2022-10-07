namespace Simofun.Unity.Genre.ArcadeIdle
{
    using System;
    using Simofun.Unity.Genre.ArcadeIdle.Input;
    using Simofun.Unity.Genre.ArcadeIdle.Controller;
    using UnityEngine;

    public class ArcadeIdlePlayer : Controller.Character
    {
        #region Protected Properties
        protected IInput Input { get; set; }

        protected IMove Controller { get; set; }
        #endregion

        #region Unity Methods
        /// <inheritdoc />
        protected virtual void Awake()
        {
            this.Controller = this.GetComponent<IMove>();
            this.Input = this.GetComponent<IInput>();

            this.Input.OnDown += this.OnDown;
            this.Input.OnHold += this.OnHold;
            this.Input.OnRelease += this.OnRelease;
        }

        /// <inheritdoc />
        protected virtual void OnDestroy()
        {
            this.Input.OnDown -= this.OnDown;
            this.Input.OnHold -= this.OnHold;
            this.Input.OnRelease -= this.OnRelease;
        }
        #endregion

        #region Methods
        #region Event Handlers
        protected virtual void OnDown() { }

        protected virtual void OnHold() =>
            this.Controller.Move(new Vector3(this.Input.Direction.x, 0, this.Input.Direction.y));

        protected virtual void OnRelease() => this.Controller.Stop();
        #endregion
        #endregion
    }
}
