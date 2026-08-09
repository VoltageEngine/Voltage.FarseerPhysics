using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Voltage;
using Voltage.Persistence;
using Voltage.Utils.Collections;


namespace Voltage.Farseer
{
	[ComponentId("fs_rigid_body")]
	public partial class FSRigidBody : Component, IUpdatable
	{
		// Runtime handle owned by the physics world, not authorable state.
		[JsonExclude]
		public Body Body;

		FSBodyDef _bodyDef = new FSBodyDef();
		bool _ignoreTransformChanges;
		internal List<FSJoint> _joints = new List<FSJoint>();

		public FSRigidBody()
		{
		}

		#region Configuration

		// The authorable surface. Properties rather than fields so the inspector reaches the live Body
		// the way the fluent setters do, and so the ComponentData generator sees them — _bodyDef is not
		// a serializable type. The getters deliberately read the def and not the Body: the def is the
		// authored intent, and reading the Body would persist whatever the simulation happened to be
		// doing at the moment the scene was saved.

		public BodyType BodyType
		{
			get => _bodyDef.BodyType;
			set
			{
				_bodyDef.BodyType = value;
				if (Body != null)
					Body.BodyType = value;
			}
		}


		public Vector2 LinearVelocity
		{
			get => _bodyDef.LinearVelocity;
			set
			{
				_bodyDef.LinearVelocity = value;
				if (Body != null)
					Body.LinearVelocity = value;
			}
		}


		public float AngularVelocity
		{
			get => _bodyDef.AngularVelocity;
			set
			{
				_bodyDef.AngularVelocity = value;
				if (Body != null)
					Body.AngularVelocity = value;
			}
		}


		public float LinearDamping
		{
			get => _bodyDef.LinearDamping;
			set
			{
				_bodyDef.LinearDamping = value;
				if (Body != null)
					Body.LinearDamping = value;
			}
		}


		public float AngularDamping
		{
			get => _bodyDef.AngularDamping;
			set
			{
				_bodyDef.AngularDamping = value;
				if (Body != null)
					Body.AngularDamping = value;
			}
		}


		public bool IsBullet
		{
			get => _bodyDef.IsBullet;
			set
			{
				_bodyDef.IsBullet = value;
				if (Body != null)
					Body.IsBullet = value;
			}
		}


		public bool IsSleepingAllowed
		{
			get => _bodyDef.IsSleepingAllowed;
			set
			{
				_bodyDef.IsSleepingAllowed = value;
				if (Body != null)
					Body.IsSleepingAllowed = value;
			}
		}


		public bool IsAwake
		{
			get => _bodyDef.IsAwake;
			set
			{
				_bodyDef.IsAwake = value;
				if (Body != null)
					Body.IsAwake = value;
			}
		}


		public bool FixedRotation
		{
			get => _bodyDef.FixedRotation;
			set
			{
				_bodyDef.FixedRotation = value;
				if (Body != null)
					Body.FixedRotation = value;
			}
		}


		public bool IgnoreGravity
		{
			get => _bodyDef.IgnoreGravity;
			set
			{
				_bodyDef.IgnoreGravity = value;
				if (Body != null)
					Body.IgnoreGravity = value;
			}
		}


		public float GravityScale
		{
			get => _bodyDef.GravityScale;
			set
			{
				_bodyDef.GravityScale = value;
				if (Body != null)
					Body.GravityScale = value;
			}
		}


		public float Mass
		{
			get => _bodyDef.Mass;
			set
			{
				_bodyDef.Mass = value;
				if (Body != null)
					Body.Mass = value;
			}
		}


		public float Inertia
		{
			get => _bodyDef.Inertia;
			set
			{
				_bodyDef.Inertia = value;
				if (Body != null)
					Body.Inertia = value;
			}
		}


		public FSRigidBody SetBodyType(BodyType bodyType)
		{
			BodyType = bodyType;
			return this;
		}


		public FSRigidBody SetLinearVelocity(Vector2 linearVelocity)
		{
			LinearVelocity = linearVelocity;
			return this;
		}


		public FSRigidBody SetAngularVelocity(float angularVelocity)
		{
			AngularVelocity = angularVelocity;
			return this;
		}


		public FSRigidBody SetLinearDamping(float linearDamping)
		{
			LinearDamping = linearDamping;
			return this;
		}


		public FSRigidBody SetAngularDamping(float angularDamping)
		{
			AngularDamping = angularDamping;
			return this;
		}


		public FSRigidBody SetIsBullet(bool isBullet)
		{
			IsBullet = isBullet;
			return this;
		}


		public FSRigidBody SetIsSleepingAllowed(bool isSleepingAllowed)
		{
			IsSleepingAllowed = isSleepingAllowed;
			return this;
		}


		public FSRigidBody SetIsAwake(bool isAwake)
		{
			IsAwake = isAwake;
			return this;
		}


		public FSRigidBody SetFixedRotation(bool fixedRotation)
		{
			FixedRotation = fixedRotation;
			return this;
		}


		public FSRigidBody SetIgnoreGravity(bool ignoreGravity)
		{
			IgnoreGravity = ignoreGravity;
			return this;
		}


		public FSRigidBody SetGravityScale(float gravityScale)
		{
			GravityScale = gravityScale;
			return this;
		}


		public FSRigidBody SetMass(float mass)
		{
			Mass = mass;
			return this;
		}


		public FSRigidBody SetInertia(float inertia)
		{
			Inertia = inertia;
			return this;
		}

		#endregion


		#region Component lifecycle

		public override void OnAddedToEntity()
		{
			CreateBody();
		}


		public override void OnStart()
		{
			CreateBody();
		}


		public override void OnRemovedFromEntity()
		{
			DestroyBody();
		}


		public override void OnEnabled()
		{
			if (Body != null)
				Body.Enabled = true;
		}


		public override void OnDisabled()
		{
			if (Body != null)
				Body.Enabled = false;
		}


		public override void OnEntityTransformChanged(Transform.Component comp)
		{
			if (_ignoreTransformChanges || Body == null)
				return;

			if (comp == Transform.Component.Position)
				Body.Position = Transform.Position * FSConvert.DisplayToSim;
			else if (comp == Transform.Component.Rotation)
				Body.Rotation = Transform.Rotation;
		}

		#endregion


		public virtual void Update()
		{
			if (Body == null || !Body.IsAwake)
				return;

			_ignoreTransformChanges = true;
			Transform.Position = FSConvert.SimToDisplay * Body.Position;
			Transform.Rotation = Body.Rotation;
			_ignoreTransformChanges = false;
		}


		void CreateBody()
		{
			if (Body != null)
				return;

			var world = Entity.Scene.GetOrCreateSceneComponent<FSWorld>();
			Body = new Body(world, Transform.Position * FSConvert.DisplayToSim, Transform.Rotation, _bodyDef.BodyType,
				this);
			Body.LinearVelocity = _bodyDef.LinearVelocity;
			Body.AngularVelocity = _bodyDef.AngularVelocity;
			Body.LinearDamping = _bodyDef.LinearDamping;
			Body.AngularDamping = _bodyDef.AngularDamping;

			Body.IsBullet = _bodyDef.IsBullet;
			Body.IsSleepingAllowed = _bodyDef.IsSleepingAllowed;
			Body.IsAwake = _bodyDef.IsAwake;
			Body.Enabled = Enabled;
			Body.FixedRotation = _bodyDef.FixedRotation;
			Body.IgnoreGravity = _bodyDef.IgnoreGravity;
			Body.GravityScale = _bodyDef.GravityScale;
			Body.Mass = _bodyDef.Mass;
			Body.Inertia = _bodyDef.Inertia;

			var collisionShapes = Entity.GetComponents<FSCollisionShape>();
			for (var i = 0; i < collisionShapes.Count; i++)
				collisionShapes[i].CreateFixture();
			ListPool<FSCollisionShape>.Free(collisionShapes);

			for (var i = 0; i < _joints.Count; i++)
				_joints[i].CreateJoint();
		}


		void DestroyBody()
		{
			for (var i = 0; i < _joints.Count; i++)
				_joints[i].DestroyJoint();
			_joints.Clear();

			var collisionShapes = Entity.GetComponents<FSCollisionShape>();
			for (var i = 0; i < collisionShapes.Count; i++)
				collisionShapes[i].DestroyFixture();
			ListPool<FSCollisionShape>.Free(collisionShapes);

			Body.World.RemoveBody(Body);
			Body = null;
		}
	}
}