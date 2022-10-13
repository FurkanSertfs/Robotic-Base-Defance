using UnityEngine;

	[RequireComponent(typeof(IInput))]
	public class InputVisualizer : MonoBehaviour
	{
		#region Fields
		bool isHold;

		IInput input;

		Material lineMat;
		#endregion

		#region Unity Methods
		/// <inheritdoc />
		protected virtual void Awake()
		{
			this.input = this.GetComponent<IInput>();

			this.input.OnDown += this.HandleOnDown;
			this.input.OnHold += this.HandleOnHold;
			this.input.OnRelease += this.HandleOnRelease;

			this.lineMat = Resources.Load<Material>("LineMat");
		}

		/// <inheritdoc />
		protected virtual void OnPostRender()
		{
			if (!this.isHold)
			{
				return;
			}

			GL.PushMatrix();
			GL.LoadOrtho();

			GL.Begin(GL.LINES);
			this.lineMat.SetPass(0);
			GL.Color(Color.red);
			GL.Vertex(this.input.Position);
			GL.Vertex(this.input.Position + this.input.Direction * 0.5f);
			GL.End();
			GL.PopMatrix();
		}

		/// <inheritdoc />
		protected virtual void OnDestroy()
		{
			this.input.OnDown -= this.HandleOnDown;
			this.input.OnHold -= this.HandleOnHold;
			this.input.OnRelease -= this.HandleOnRelease;
		}
		#endregion

		#region Methods
		#region Event Handlers
		void HandleOnDown() => this.isHold = true;

		void HandleOnHold() => this.isHold = true;

		void HandleOnRelease() => this.isHold = false;
		#endregion
		#endregion
	}

