using FarseerPhysics.Dynamics;
using Voltage;
using Voltage.Utils.Extensions;


namespace Voltage.Farseer
{
	public abstract class FSCollisionShape : Component
	{
		internal FSFixtureDef _fixtureDef = new FSFixtureDef();
		protected Fixture _fixture;


		#region Configuration

		// The authorable surface. These are properties rather than fields so that a value written
		// by the inspector reaches the live Fixture the same way the fluent setters do, and so the
		// ComponentData generator picks them up — _fixtureDef itself is not a serializable type.

		public float Friction
		{
			get => _fixtureDef.Friction;
			set
			{
				_fixtureDef.Friction = value;
				if (_fixture != null)
				{
					_fixture.Friction = value;

					var body = this.GetComponent<FSRigidBody>().Body;
					var contactEdge = body.ContactList;
					while (contactEdge != null)
					{
						var contact = contactEdge.Contact;
						if (contact.FixtureA == _fixture || contact.FixtureB == _fixture)
							contact.ResetFriction();
						contactEdge = contactEdge.Next;
					}
				}
			}
		}


		public float Restitution
		{
			get => _fixtureDef.Restitution;
			set
			{
				_fixtureDef.Restitution = value;
				if (_fixture != null)
				{
					_fixture.Restitution = value;

					var body = this.GetComponent<FSRigidBody>().Body;
					var contactEdge = body.ContactList;
					while (contactEdge != null)
					{
						var contact = contactEdge.Contact;
						if (contact.FixtureA == _fixture || contact.FixtureB == _fixture)
							contact.ResetRestitution();
						contactEdge = contactEdge.Next;
					}
				}
			}
		}


		public float Density
		{
			get => _fixtureDef.Density;
			set
			{
				_fixtureDef.Density = value;
				if (_fixture != null)
					_fixture.Shape.Density = value;
			}
		}


		public bool IsSensor
		{
			get => _fixtureDef.IsSensor;
			set
			{
				_fixtureDef.IsSensor = value;
				if (_fixture != null)
					_fixture.IsSensor = value;
			}
		}


		public Category CollidesWith
		{
			get => _fixtureDef.CollidesWith;
			set
			{
				_fixtureDef.CollidesWith = value;
				if (_fixture != null)
					_fixture.CollidesWith = value;
			}
		}


		public Category CollisionCategories
		{
			get => _fixtureDef.CollisionCategories;
			set
			{
				_fixtureDef.CollisionCategories = value;
				if (_fixture != null)
					_fixture.CollisionCategories = value;
			}
		}


		public Category IgnoreCCDWith
		{
			get => _fixtureDef.IgnoreCCDWith;
			set
			{
				_fixtureDef.IgnoreCCDWith = value;
				if (_fixture != null)
					_fixture.IgnoreCCDWith = value;
			}
		}


		public short CollisionGroup
		{
			get => _fixtureDef.CollisionGroup;
			set
			{
				_fixtureDef.CollisionGroup = value;
				if (_fixture != null)
					_fixture.CollisionGroup = value;
			}
		}


		public FSCollisionShape SetFriction(float friction)
		{
			Friction = friction;
			return this;
		}


		public FSCollisionShape SetRestitution(float restitution)
		{
			Restitution = restitution;
			return this;
		}


		public FSCollisionShape SetDensity(float density)
		{
			Density = density;
			return this;
		}


		public FSCollisionShape SetIsSensor(bool isSensor)
		{
			IsSensor = isSensor;
			return this;
		}


		public FSCollisionShape SetCollidesWith(Category collidesWith)
		{
			CollidesWith = collidesWith;
			return this;
		}


		public FSCollisionShape SetCollisionCategories(Category collisionCategories)
		{
			CollisionCategories = collisionCategories;
			return this;
		}


		public FSCollisionShape SetIgnoreCCDWith(Category ignoreCCDWith)
		{
			IgnoreCCDWith = ignoreCCDWith;
			return this;
		}


		public FSCollisionShape SetCollisionGroup(short collisionGroup)
		{
			CollisionGroup = collisionGroup;
			return this;
		}

		#endregion


		#region Component lifecycle

		public override void OnStart()
		{
			CreateFixture();
		}


		public override void OnRemovedFromEntity()
		{
			DestroyFixture();
		}


		public override void OnEnabled()
		{
			CreateFixture();
		}


		public override void OnDisabled()
		{
			DestroyFixture();
		}

		#endregion


		/// <summary>
		/// wakes any contacting bodies. Useful when creating a fixture or changing something that won't trigger the bodies to wake themselves
		/// such as Circle.center.
		/// </summary>
		protected void WakeAnyContactingBodies()
		{
			var body = this.GetComponent<FSRigidBody>().Body;
			var contactEdge = body.ContactList;
			while (contactEdge != null)
			{
				var contact = contactEdge.Contact;
				if (contact.FixtureA == _fixture || contact.FixtureB == _fixture)
				{
					contact.FixtureA.Body.IsAwake = true;
					contact.FixtureB.Body.IsAwake = true;
				}

				contactEdge = contactEdge.Next;
			}
		}


		internal virtual void CreateFixture()
		{
			if (_fixture != null)
				return;

			var rigidBody = this.GetComponent<FSRigidBody>();
			if (rigidBody == null || rigidBody.Body == null)
				return;

			var body = rigidBody.Body;
			_fixtureDef.Shape.Density = _fixtureDef.Density;
			_fixture = body.CreateFixture(_fixtureDef.Shape, this);
			_fixture.Friction = _fixtureDef.Friction;
			_fixture.Restitution = _fixtureDef.Restitution;
			_fixture.IsSensor = _fixtureDef.IsSensor;
			_fixture.CollidesWith = _fixtureDef.CollidesWith;
			_fixture.CollisionCategories = _fixtureDef.CollisionCategories;
			_fixture.IgnoreCCDWith = _fixtureDef.IgnoreCCDWith;
			_fixture.CollisionGroup = _fixtureDef.CollisionGroup;
		}


		internal virtual void DestroyFixture()
		{
			if (_fixture == null)
				return;

			var rigidBody = this.GetComponent<FSRigidBody>();
			if (rigidBody == null || rigidBody.Body == null)
				return;

			rigidBody.Body.DestroyFixture(_fixture);
			_fixture = null;
		}
	}
}