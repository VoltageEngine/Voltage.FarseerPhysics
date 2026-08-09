using Microsoft.Xna.Framework;


namespace Voltage.Farseer
{
	[ComponentId("fs_motor_joint")]
	public partial class FSMotorJoint : FSJoint
	{
		FSMotorJointDef _jointDef = new FSMotorJointDef();


		#region Configuration

		public Vector2 LinearOffset
		{
			get => _jointDef.LinearOffset;
			set
			{
				_jointDef.LinearOffset = value;
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


		public float AngularOffset
		{
			get => _jointDef.AngularOffset;
			set
			{
				_jointDef.AngularOffset = value;
				RecreateJoint();
			}
		}


		public FSMotorJoint SetLinearOffset(Vector2 linearOffset)
		{
			LinearOffset = linearOffset;
			return this;
		}


		public FSMotorJoint SetMaxForce(float maxForce)
		{
			MaxForce = maxForce;
			return this;
		}


		public FSMotorJoint SetMaxTorque(float maxTorque)
		{
			MaxTorque = maxTorque;
			return this;
		}


		public FSMotorJoint SetAngularOffset(float angularOffset)
		{
			AngularOffset = angularOffset;
			return this;
		}

		#endregion


		internal override FSJointDef GetJointDef()
		{
			InitializeJointDef(_jointDef);
			if (_jointDef.BodyA == null || _jointDef.BodyB == null)
				return null;

			return _jointDef;
		}
	}
}
