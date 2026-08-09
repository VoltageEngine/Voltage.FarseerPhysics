using Microsoft.Xna.Framework;


namespace Voltage.Farseer
{
	[ComponentId("fs_revolute_joint")]
	public partial class FSRevoluteJoint : FSJoint
	{
		FSRevoluteJointDef _jointDef = new FSRevoluteJointDef();


		#region Configuration

		public Vector2 OwnerBodyAnchor
		{
			get => _jointDef.OwnerBodyAnchor;
			set
			{
				_jointDef.OwnerBodyAnchor = value;
				RecreateJoint();
			}
		}


		public Vector2 OtherBodyAnchor
		{
			get => _jointDef.OtherBodyAnchor;
			set
			{
				_jointDef.OtherBodyAnchor = value;
				RecreateJoint();
			}
		}


		public bool LimitEnabled
		{
			get => _jointDef.LimitEnabled;
			set
			{
				_jointDef.LimitEnabled = value;
				RecreateJoint();
			}
		}


		public float LowerLimit
		{
			get => _jointDef.LowerLimit;
			set
			{
				_jointDef.LowerLimit = value;
				RecreateJoint();
			}
		}


		public float UpperLimit
		{
			get => _jointDef.UpperLimit;
			set
			{
				_jointDef.UpperLimit = value;
				RecreateJoint();
			}
		}


		public bool MotorEnabled
		{
			get => _jointDef.MotorEnabled;
			set
			{
				_jointDef.MotorEnabled = value;
				RecreateJoint();
			}
		}


		public float MotorSpeed
		{
			get => _jointDef.MotorSpeed;
			set
			{
				_jointDef.MotorSpeed = value;
				RecreateJoint();
			}
		}


		public float MaxMotorTorque
		{
			get => _jointDef.MaxMotorTorque;
			set
			{
				_jointDef.MaxMotorTorque = value;
				RecreateJoint();
			}
		}


		public float MotorImpulse
		{
			get => _jointDef.MotorImpulse;
			set
			{
				_jointDef.MotorImpulse = value;
				RecreateJoint();
			}
		}


		public FSRevoluteJoint SetOwnerBodyAnchor(Vector2 ownerBodyAnchor)
		{
			OwnerBodyAnchor = ownerBodyAnchor;
			return this;
		}


		public FSRevoluteJoint SetOtherBodyAnchor(Vector2 otherBodyAnchor)
		{
			OtherBodyAnchor = otherBodyAnchor;
			return this;
		}


		public FSRevoluteJoint SetLimitEnabled(bool limitEnabled)
		{
			LimitEnabled = limitEnabled;
			return this;
		}


		public FSRevoluteJoint SetLowerLimit(float lowerLimit)
		{
			LowerLimit = lowerLimit;
			return this;
		}


		public FSRevoluteJoint SetUpperLimit(float upperLimit)
		{
			UpperLimit = upperLimit;
			return this;
		}


		public FSRevoluteJoint SetMotorEnabled(bool motorEnabled)
		{
			MotorEnabled = motorEnabled;
			return this;
		}


		public FSRevoluteJoint SetMotorSpeed(float motorSpeed)
		{
			MotorSpeed = motorSpeed;
			return this;
		}


		public FSRevoluteJoint SetMaxMotorTorque(float maxMotorTorque)
		{
			MaxMotorTorque = maxMotorTorque;
			return this;
		}


		public FSRevoluteJoint SetMotorImpulse(float motorImpulse)
		{
			MotorImpulse = motorImpulse;
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
