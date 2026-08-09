using Microsoft.Xna.Framework;


namespace Voltage.Farseer
{
	[ComponentId("fs_wheel_joint")]
	public partial class FSWheelJoint : FSJoint
	{
		FSWheelJointDef _jointDef = new FSWheelJointDef();


		#region Configuration

		public Vector2 Anchor
		{
			get => _jointDef.Anchor;
			set
			{
				_jointDef.Anchor = value;
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


		public float Frequency
		{
			get => _jointDef.Frequency;
			set
			{
				_jointDef.Frequency = value;
				RecreateJoint();
			}
		}


		public float DampingRatio
		{
			get => _jointDef.DampingRatio;
			set
			{
				_jointDef.DampingRatio = value;
				RecreateJoint();
			}
		}


		public FSWheelJoint SetAnchor(Vector2 anchor)
		{
			Anchor = anchor;
			return this;
		}


		public FSWheelJoint SetAxis(Vector2 axis)
		{
			Axis = axis;
			return this;
		}


		public FSWheelJoint SetMotorEnabled(bool motorEnabled)
		{
			MotorEnabled = motorEnabled;
			return this;
		}


		public FSWheelJoint SetMotorSpeed(float motorSpeed)
		{
			MotorSpeed = motorSpeed;
			return this;
		}


		public FSWheelJoint SetMaxMotorTorque(float maxMotorTorque)
		{
			MaxMotorTorque = maxMotorTorque;
			return this;
		}


		public FSWheelJoint SetFrequency(float frequency)
		{
			Frequency = frequency;
			return this;
		}


		public FSWheelJoint SetDampingRatio(float damping)
		{
			DampingRatio = damping;
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
