using Microsoft.Xna.Framework;


namespace Voltage.Farseer
{
	[ComponentId("fs_prismatic_joint")]
	public partial class FSPrismaticJoint : FSJoint
	{
		FSPrismaticJointDef _jointDef = new FSPrismaticJointDef();


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


		public Vector2 Axis
		{
			get => _jointDef.Axis;
			set
			{
				_jointDef.Axis = value;
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


		public float MaxMotorForce
		{
			get => _jointDef.MaxMotorForce;
			set
			{
				_jointDef.MaxMotorForce = value;
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


		public FSPrismaticJoint SetOwnerBodyAnchor(Vector2 ownerBodyAnchor)
		{
			OwnerBodyAnchor = ownerBodyAnchor;
			return this;
		}


		public FSPrismaticJoint SetOtherBodyAnchor(Vector2 otherBodyAnchor)
		{
			OtherBodyAnchor = otherBodyAnchor;
			return this;
		}


		public FSPrismaticJoint SetAxis(Vector2 axis)
		{
			Axis = axis;
			return this;
		}


		public FSPrismaticJoint SetLimitEnabled(bool limitEnabled)
		{
			LimitEnabled = limitEnabled;
			return this;
		}


		public FSPrismaticJoint SetLowerLimit(float lowerLimit)
		{
			LowerLimit = lowerLimit;
			return this;
		}


		public FSPrismaticJoint SetUpperLimit(float upperLimit)
		{
			UpperLimit = upperLimit;
			return this;
		}


		public FSPrismaticJoint SetMotorEnabled(bool motorEnabled)
		{
			MotorEnabled = motorEnabled;
			return this;
		}


		public FSPrismaticJoint SetMotorSpeed(float motorSpeed)
		{
			MotorSpeed = motorSpeed;
			return this;
		}


		public FSPrismaticJoint SetMaxMotorForce(float maxMotorForce)
		{
			MaxMotorForce = maxMotorForce;
			return this;
		}


		public FSPrismaticJoint SetMotorImpulse(float motorImpulse)
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
