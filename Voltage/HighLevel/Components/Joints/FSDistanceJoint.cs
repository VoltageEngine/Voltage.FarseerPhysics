using Microsoft.Xna.Framework;


namespace Voltage.Farseer
{
	[ComponentId("fs_distance_joint")]
	public partial class FSDistanceJoint : FSJoint
	{
		FSDistanceJointDef _jointDef = new FSDistanceJointDef();


		#region Configuration

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


		public FSDistanceJoint SetFrequency(float frequency)
		{
			Frequency = frequency;
			return this;
		}


		public FSDistanceJoint SetDampingRatio(float damping)
		{
			DampingRatio = damping;
			return this;
		}


		public FSDistanceJoint SetOwnerBodyAnchor(Vector2 ownerBodyAnchor)
		{
			OwnerBodyAnchor = ownerBodyAnchor;
			return this;
		}


		public FSDistanceJoint SetOtherBodyAnchor(Vector2 otherBodyAnchor)
		{
			OtherBodyAnchor = otherBodyAnchor;
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
