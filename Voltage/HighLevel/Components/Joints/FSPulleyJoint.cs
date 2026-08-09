using Microsoft.Xna.Framework;


namespace Voltage.Farseer
{
	[ComponentId("fs_pulley_joint")]
	public partial class FSPulleyJoint : FSJoint
	{
		FSPulleyJointDef _jointDef = new FSPulleyJointDef();


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


		public Vector2 OwnerBodyGroundAnchor
		{
			get => _jointDef.OwnerBodyGroundAnchor;
			set
			{
				_jointDef.OwnerBodyGroundAnchor = value;
				RecreateJoint();
			}
		}


		public Vector2 OtherBodyGroundAnchor
		{
			get => _jointDef.OtherBodyGroundAnchor;
			set
			{
				_jointDef.OtherBodyGroundAnchor = value;
				RecreateJoint();
			}
		}


		public float Ratio
		{
			get => _jointDef.Ratio;
			set
			{
				_jointDef.Ratio = value;
				RecreateJoint();
			}
		}


		public FSPulleyJoint SetOwnerBodyAnchor(Vector2 ownerBodyAnchor)
		{
			OwnerBodyAnchor = ownerBodyAnchor;
			return this;
		}


		public FSPulleyJoint SetOtherBodyAnchor(Vector2 otherBodyAnchor)
		{
			OtherBodyAnchor = otherBodyAnchor;
			return this;
		}


		public FSPulleyJoint SetOwnerBodyGroundAnchor(Vector2 ownerBodyGroundAnchor)
		{
			OwnerBodyGroundAnchor = ownerBodyGroundAnchor;
			return this;
		}


		public FSPulleyJoint SetOtherBodyGroundAnchor(Vector2 otherBodyGroundAnchor)
		{
			OtherBodyGroundAnchor = otherBodyGroundAnchor;
			return this;
		}


		public FSPulleyJoint SetRatio(float ratio)
		{
			Ratio = ratio;
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
