using System;
using UnityEngine;

    public class JoystickInput : MonoBehaviour, IInput
    {
        #region Fields
        Vector2 centerPosition;

        Vector2 previousPosition;
        #endregion

        #region Events
        public event Action OnDown;

        public event Action OnRelease;

        public event Action OnHold;
        #endregion

        #region Properties
        public Vector3 WorldPosition { get => Camera.main.ScreenToWorldPoint(this.Position); }

        public Vector2 Position { get; protected set; }

        public Vector2 Delta { get; protected set; }

        public Vector2 Direction { get; protected set; }
        #endregion


        #region Unity Methods
        /// <inheritdoc />
        protected virtual void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = touch.position;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        {
                            this.OnInputDown(touchPosition);

                            break;
                        }
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        {
                            this.OnInputHold(touchPosition);

                            break;
                        }
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        {
                            this.OnInputRelease(touchPosition);

                            break;
                        }
                }

                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                this.OnInputDown(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                this.OnInputHold(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                this.OnInputRelease(Input.mousePosition);
            }
        }
        #endregion

        #region Methods
        void OnInputDown(Vector2 pos)
        {
            this.Position = pos;
            this.centerPosition = pos;
            this.Direction = Vector2.zero;
            this.Delta = Vector2.zero;
            this.previousPosition = pos;
            this.OnDown?.Invoke();
        }

        void OnInputHold(Vector2 pos)
        {
            this.Direction = pos - this.centerPosition;

            if (this.Direction.magnitude > 75)
            {
                var direction = (this.centerPosition - pos).normalized;
                this.centerPosition = direction * 75 + pos;
            }

            this.Delta = pos - this.previousPosition;
            this.previousPosition = pos;
            this.Position = pos;

            this.OnHold?.Invoke();
        }

        void OnInputRelease(Vector2 pos)
        {
            this.Position = pos;
            this.Direction = Vector2.zero;
            this.centerPosition = Vector2.zero;
            this.Delta = Vector2.zero;
            this.previousPosition = pos;
            this.OnRelease?.Invoke();
        }
        #endregion
    }

