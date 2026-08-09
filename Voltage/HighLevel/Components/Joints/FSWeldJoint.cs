using Microsoft.Xna.Framework;


namespace Voltage.Farseer
{
	[ComponentId("fs_weld_joint")]
	public partial class FSWeldJoint : FSJoint
	{
		FSWeldJointDef _jointDef = new FSWeldJointDef();


		#region Configuration

		public float FrequencyHz
		{
			get => _jointDef.FrequencyHz;
			set
			{
				_jointDef.FrequencyHz = value;
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


		public FSWeldJoint SetFrequencyHz(float frequency)
		{
			FrequencyHz = frequency;
			return this;
		}


		public FSWeldJoint SetDampingRatio(float damping)
		{
			DampingRatio = damping;
			return this;
		}


		public FSWeldJoint SetOwnerBodyAnchor(Vector2 ownerBodyAnchor)
		{
			OwnerBodyAnchor = ownerBodyAnchor;
			return this;
		}


		public FSWeldJoint SetOtherBodyAnchor(Vector2 otherBodyAnchor)
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
