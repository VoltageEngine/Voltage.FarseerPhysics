namespace Voltage.Farseer
{
	[ComponentId("fs_angle_joint")]
	public partial class FSAngleJoint : FSJoint
	{
		FSAngleJointDef _jointDef = new FSAngleJointDef();


		#region Configuration

		public float MaxImpulse
		{
			get => _jointDef.MaxImpulse;
			set
			{
				_jointDef.MaxImpulse = value;
				RecreateJoint();
			}
		}


		public float BiasFactor
		{
			get => _jointDef.BiasFactor;
			set
			{
				_jointDef.BiasFactor = value;
				RecreateJoint();
			}
		}


		public float Softness
		{
			get => _jointDef.Softness;
			set
			{
				_jointDef.Softness = value;
				RecreateJoint();
			}
		}


		public FSAngleJoint SetMaxImpulse(float maxImpulse)
		{
			MaxImpulse = maxImpulse;
			return this;
		}


		public FSAngleJoint SetBiasFactor(float biasFactor)
		{
			BiasFactor = biasFactor;
			return this;
		}


		public FSAngleJoint SetSoftness(float softness)
		{
			Softness = softness;
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
