using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework;
using Voltage;


namespace Voltage.Farseer
{
	[ComponentId("fs_collision_circle")]
	public partial class FSCollisionCircle : FSCollisionShape
	{
		Vector2 _center;
		float _radius = 0.1f;


		public FSCollisionCircle()
		{
			_fixtureDef.Shape = new CircleShape();
		}


		public FSCollisionCircle(float radius) : this()
		{
			_radius = radius;
			_fixtureDef.Shape.Radius = _radius * FSConvert.DisplayToSim;
		}


		#region Configuration

		public float Radius
		{
			get => _radius;
			set
			{
				_radius = value;
				RecreateFixture();
			}
		}


		public Vector2 Center
		{
			get => _center;
			set
			{
				_center = value;
				RecreateFixture();
			}
		}


		public FSCollisionCircle SetRadius(float radius)
		{
			Radius = radius;
			return this;
		}


		public FSCollisionCircle SetCenter(Vector2 center)
		{
			Center = center;
			return this;
		}

		#endregion


		public override void OnEntityTransformChanged(Transform.Component comp)
		{
			if (comp == Transform.Component.Scale)
				RecreateFixture();
		}


		/// <summary>
		/// the shape carries no state of its own until this runs, so it has to happen before the
		/// fixture is built — otherwise a radius restored from a scene file is never applied.
		/// </summary>
		internal override void CreateFixture()
		{
			UpdateShape();
			base.CreateFixture();
		}


		void UpdateShape()
		{
			var scale = Entity != null ? Transform.Scale.X : 1f;
			_fixtureDef.Shape.Radius = _radius * scale * FSConvert.DisplayToSim;
			(_fixtureDef.Shape as CircleShape).Position = FSConvert.DisplayToSim * _center;
		}


		void RecreateFixture()
		{
			// deserialization writes these properties before the component has an Entity
			if (Entity == null)
				return;

			UpdateShape();

			if (_fixture != null)
			{
				var circleShape = _fixture.Shape as CircleShape;
				circleShape.Radius = _fixtureDef.Shape.Radius;
				circleShape.Position = FSConvert.DisplayToSim * _center;

				// wake the body if it is asleep to update collisions
				WakeAnyContactingBodies();
			}
		}
	}
}