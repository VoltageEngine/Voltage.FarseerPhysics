using System.Collections.Generic;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using Voltage;
using Transform = Voltage.Transform;


namespace Voltage.Farseer
{
	[ComponentId("fs_collision_polygon")]
	public partial class FSCollisionPolygon : FSCollisionShape
	{
		/// <summary>
		/// verts are stored in sim units. A plain List (rather than Farseer's Vertices) because the
		/// ComponentData generator only recognizes List&lt;T&gt; — a subclass of it serializes as nothing.
		/// </summary>
		protected List<Vector2> _verts = DefaultVerts();

		Vector2 _center;
		protected bool _areVertsDirty = true;


		public FSCollisionPolygon()
		{
			_fixtureDef.Shape = new PolygonShape();
		}


		public FSCollisionPolygon(List<Vector2> vertices) : this()
		{
			_verts = ToSimUnits(vertices);
		}


		public FSCollisionPolygon(Vector2[] vertices) : this()
		{
			_verts = ToSimUnits(vertices);
		}


		#region Configuration

		/// <summary>
		/// the polygon's vertices, in sim units. Derived shapes (box, ellipse) compute this from their
		/// own dimensions and overwrite whatever is set here.
		/// </summary>
		public List<Vector2> Verts
		{
			get => _verts;
			set
			{
				// a scene written before this was authorable has no verts at all, and the generated
				// setter turns that into an empty list — which would build a zero-vertex shape.
				_verts = value == null || value.Count == 0 ? DefaultVerts() : value;
				_areVertsDirty = true;
				RecreateFixture();
			}
		}


		public Vector2 Center
		{
			get => _center;
			set
			{
				_center = value;
				_areVertsDirty = true;
				RecreateFixture();
			}
		}


		public FSCollisionPolygon SetVertices(Vertices vertices)
		{
			Verts = new List<Vector2>(vertices);
			return this;
		}


		public FSCollisionPolygon SetVertices(List<Vector2> vertices)
		{
			Verts = new List<Vector2>(vertices);
			return this;
		}


		public FSCollisionPolygon SetCenter(Vector2 center)
		{
			Center = center;
			return this;
		}

		#endregion


		public override void OnStart()
		{
			UpdateVerts();
			CreateFixture();
		}


		public override void OnEntityTransformChanged(Transform.Component comp)
		{
			if (comp == Transform.Component.Scale)
			{
				// UpdateVerts re-reads Transform.Scale, but only when it thinks the verts are stale
				_areVertsDirty = true;
				RecreateFixture();
			}
		}


		internal override void CreateFixture()
		{
			UpdateVerts();
			base.CreateFixture();
		}


		protected void RecreateFixture()
		{
			// deserialization writes the configuration properties before the component has an Entity
			if (Entity == null)
				return;

			DestroyFixture();
			UpdateVerts();
			CreateFixture();
		}


		/// <summary>
		/// hook for shapes whose vertices are derived from other authored values rather than authored
		/// directly. Runs whenever the verts are about to be pushed into the shape.
		/// </summary>
		protected virtual void RebuildVerts()
		{
		}


		protected void UpdateVerts()
		{
			if (!_areVertsDirty)
				return;

			_areVertsDirty = false;

			RebuildVerts();
			Insist.IsNotNull(_verts, "verts cannot be null!");

			var shapeVerts = (_fixtureDef.Shape as PolygonShape).Vertices;
			shapeVerts.attachedToBody = false;

			shapeVerts.Clear();
			shapeVerts.AddRange(_verts);
			shapeVerts.Scale(Entity != null ? Transform.Scale : Vector2.One);
			shapeVerts.Translate(ref _center);

			(_fixtureDef.Shape as PolygonShape).SetVerticesNoCopy(shapeVerts);
		}


		/// <summary>
		/// a 0.1 x 0.1 box, so a polygon added from the editor and never configured is still a valid shape
		/// rather than an assert at fixture-creation time.
		/// </summary>
		static List<Vector2> DefaultVerts() =>
			new List<Vector2>(PolygonTools.CreateRectangle(FSConvert.DisplayToSim * 0.05f,
				FSConvert.DisplayToSim * 0.05f));


		static List<Vector2> ToSimUnits(IEnumerable<Vector2> vertices)
		{
			var verts = new Vertices(vertices);
			verts.Scale(new Vector2(FSConvert.DisplayToSim));
			return new List<Vector2>(verts);
		}
	}
}
