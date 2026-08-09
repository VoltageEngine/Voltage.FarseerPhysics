using Microsoft.Xna.Framework;


namespace Voltage.Farseer
{
	[ComponentId("fs_rope_joint")]
	public partial class FSRopeJoint : FSJoint
	{
		FSRopeJointDef _jointDef = new FSRopeJointDef();


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


		public float MaxLength
		{
			get => _jointDef.MaxLength;
			set
			{
				_jointDef.MaxLength = value;
				RecreateJoint();
			}
		}


		public FSRopeJoint SetOwnerBodyAnchor(Vector2 ownerBodyAnchor)
		{
			OwnerBodyAnchor = ownerBodyAnchor;
			return this;
		}


		public FSRopeJoint SetOtherBodyAnchor(Vector2 otherBodyAnchor)
		{
			OtherBodyAnchor = otherBodyAnchor;
			return this;
		}


		public FSRopeJoint SetMaxLength(float maxLength)
		{
			MaxLength = maxLength;
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
