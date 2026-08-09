using Microsoft.Xna.Framework;


namespace Voltage.Farseer
{
	[ComponentId("fs_friction_joint")]
	public partial class FSFrictionJoint : FSJoint
	{
		FSFrictionJointDef _jointDef = new FSFrictionJointDef();
		Vector2 _anchor;


		#region Configuration

		public Vector2 Anchor
		{
			get => _anchor;
			set
			{
				_anchor = value;
				RecreateJoint();
			}
		}


		public float MaxForce
		{
			get => _jointDef.MaxForce;
			set
			{
				_jointDef.MaxForce = value;
				RecreateJoint();
			}
		}


		public float MaxTorque
		{
			get => _jointDef.MaxTorque;
			set
			{
				_jointDef.MaxTorque = value;
				RecreateJoint();
			}
		}


		public FSFrictionJoint SetAnchor(Vector2 anchor)
		{
			Anchor = anchor;
			return this;
		}


		public FSFrictionJoint SetMaxForce(float maxForce)
		{
			MaxForce = maxForce;
			return this;
		}


		public FSFrictionJoint SetMaxTorque(float maxTorque)
		{
			MaxTorque = maxTorque;
			return this;
		}

		#endregion


		internal override FSJointDef GetJointDef()
		{
			InitializeJointDef(_jointDef);
			if (_jointDef.BodyA == null || _jointDef.BodyB == null)
				return null;

			_jointDef.Anchor = FSConvert.DisplayToSim * _anchor;

			return _jointDef;
		}
	}
}
