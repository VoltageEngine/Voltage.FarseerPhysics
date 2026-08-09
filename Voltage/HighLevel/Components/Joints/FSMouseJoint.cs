using FarseerPhysics.Dynamics.Joints;
using Microsoft.Xna.Framework;
using Voltage;


namespace Voltage.Farseer
{
	[ComponentId("fs_mouse_joint")]
	public partial class FSMouseJoint : FSJoint, IUpdatable
	{
		FSMouseJointDef _jointDef = new FSMouseJointDef();


		#region Configuration

		public Vector2 WorldAnchor
		{
			get => _jointDef.WorldAnchor;
			set
			{
				_jointDef.WorldAnchor = value;
				if (_joint != null)
					_joint.WorldAnchorB = value * FSConvert.DisplayToSim;
			}
		}


		public float Frequency
		{
			get => _jointDef.Frequency;
			set
			{
				_jointDef.Frequency = value;
				if (_joint != null)
					(_joint as FixedMouseJoint).Frequency = value;
			}
		}


		public float DampingRatio
		{
			get => _jointDef.DampingRatio;
			set
			{
				_jointDef.DampingRatio = value;
				if (_joint != null)
					(_joint as FixedMouseJoint).DampingRatio = value;
			}
		}


		public float MaxForce
		{
			get => _jointDef.MaxForce;
			set
			{
				_jointDef.MaxForce = value;
				if (_joint != null)
					(_joint as FixedMouseJoint).MaxForce = value;
			}
		}


		public FSMouseJoint SetWorldAnchor(Vector2 worldAnchor)
		{
			WorldAnchor = worldAnchor;
			return this;
		}


		public FSMouseJoint SetFrequency(float frequency)
		{
			Frequency = frequency;
			return this;
		}


		public FSMouseJoint SetDampingRatio(float dampingRatio)
		{
			DampingRatio = dampingRatio;
			return this;
		}


		public FSMouseJoint SetMaxForce(float maxForce)
		{
			MaxForce = maxForce;
			return this;
		}

		#endregion


		public virtual void Update()
		{
			if (_joint != null)
			{
				var pos = Core.Scene.Camera.ScreenToWorldPoint(Input.MousePosition);
				SetWorldAnchor(pos);
			}
		}


		internal override FSJointDef GetJointDef()
		{
			InitializeJointDef(_jointDef);
			if (_jointDef.BodyA == null)
				return null;

			return _jointDef;
		}
	}
}
