namespace Voltage.Farseer
{
	[ComponentId("fs_gear_joint")]
	public partial class FSGearJoint : FSJoint
	{
		FSGearJointDef _jointDef = new FSGearJointDef();
		FSJoint _ownerJoint;
		FSJoint _otherJoint;


		#region Configuration

		/// <summary>
		/// serialized as a ComponentReference, so the pairing survives a scene round-trip.
		/// </summary>
		public FSJoint OwnerJoint
		{
			get => _ownerJoint;
			set
			{
				if (_ownerJoint != null)
					_ownerJoint._attachedJoint = null;

				_ownerJoint = value;

				if (_ownerJoint != null)
					_ownerJoint._attachedJoint = this;

				RecreateJoint();
			}
		}


		public FSJoint OtherJoint
		{
			get => _otherJoint;
			set
			{
				if (_otherJoint != null)
					_otherJoint._attachedJoint = null;

				_otherJoint = value;

				if (_otherJoint != null)
					_otherJoint._attachedJoint = this;

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


		public FSGearJoint SetOwnerJoint(FSJoint ownerJoint)
		{
			OwnerJoint = ownerJoint;
			return this;
		}


		public FSGearJoint SetOtherJoint(FSJoint otherJoint)
		{
			OtherJoint = otherJoint;
			return this;
		}


		public FSGearJoint SetRatio(float ratio)
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

			if (_ownerJoint == null || _otherJoint == null)
				return null;

			if (_ownerJoint._joint == null || _otherJoint._joint == null)
				return null;

			_jointDef.OwnerJoint = _ownerJoint._joint;
			_jointDef.OtherJoint = _otherJoint._joint;

			return _jointDef;
		}
	}
}
