 using System;
using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMovementController : MonoBehaviour, IMove
    {
        #region Unity Fields
        [SerializeField]
        float speed = 5;

        [SerializeField]
        float rotationSpeed = 25;
        #endregion

        #region Fields
        CapsuleCollider capsuleCollider;

        Rigidbody body;

        Vector3 targetDirection;
        #endregion

        #region Properties
        public float Speed => this.Velocity.magnitude;

        public Vector3 Velocity
        {
            get
            {
                var velocity = default(Vector3);
                if (this.body)
                {
                    velocity = this.body.velocity;
                }

                return velocity;
            }
        }
        #endregion

        #region Unity Methods
        /// <inheritdoc />
        protected virtual void Awake()
        {
            this.body = this.GetComponent<Rigidbody>();
            this.capsuleCollider = this.GetComponent<CapsuleCollider>();
        }

        /// <inheritdoc />
        protected virtual void FixedUpdate()
        {
            var velocity = Vector3.zero;
            var direction = this.targetDirection;
            direction.y = 0;
            direction.Normalize();
            //var isDirectionSafe = EdgeDetector.Check(

           
                velocity += direction;
            

            this.body.velocity = velocity * this.speed;

            // Set direction to velocity lerp
            if (this.body.velocity == Vector3.zero)
            {
                return;
            }

            this.transform.rotation = Quaternion.Lerp(
                this.transform.rotation,
                Quaternion.LookRotation(this.body.velocity),
                Time.deltaTime * this.rotationSpeed);
        }
        #endregion

        #region Public Methods
        public void Activate()
        {
            this.enabled = true;
            this.body.velocity = Vector3.zero;
            this.capsuleCollider.enabled = true;
            this.body.isKinematic = false;
         
        }

        public void Deactivate()
        {
            this.enabled = false;

            if (this.body == null)
            {
                return;
            }

            this.body.velocity = Vector3.zero;
            this.body.isKinematic = true;
            this.capsuleCollider.enabled = false;
        }

        public void Move(Vector3 direction) => this.targetDirection = direction;

        public void Stop() => this.targetDirection = Vector3.zero;
        #endregion
    }

