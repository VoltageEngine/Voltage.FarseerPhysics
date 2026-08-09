using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework;


namespace Voltage.Farseer
{
	[ComponentId("fs_collision_edge")]
	public partial class FSCollisionEdge : FSCollisionShape
	{
		Vector2 _vertex1 = new Vector2(-0.01f, 0);
		Vector2 _vertex2 = new Vector2(0.01f, 0);


		public FSCollisionEdge()
		{
			_fixtureDef.Shape = new EdgeShape();
		}


		#region Configuration

		public Vector2 Vertex1
		{
			get => _vertex1;
			set
			{
				_vertex1 = value;
				RecreateFixture();
			}
		}


		public Vector2 Vertex2
		{
			get => _vertex2;
			set
			{
				_vertex2 = value;
				RecreateFixture();
			}
		}


		public FSCollisionEdge SetVertices(Vector2 vertex1, Vector2 vertex2)
		{
			_vertex1 = vertex1;
			_vertex2 = vertex2;
			RecreateFixture();
			return this;
		}

		#endregion


		/// <summary>
		/// the vertices have to reach the shape before the fixture is built, or an edge restored from a
		/// scene file keeps the near-zero-length default.
		/// </summary>
		internal override void CreateFixture()
		{
			UpdateShape();
			base.CreateFixture();
		}


		void UpdateShape()
		{
			var scale = Entity != null ? Transform.Scale : Vector2.One;
			var edgeShape = _fixtureDef.Shape as EdgeShape;
			edgeShape.Vertex1 = _vertex1 * scale * FSConvert.DisplayToSim;
			edgeShape.Vertex2 = _vertex2 * scale * FSConvert.DisplayToSim;
		}


		void RecreateFixture()
		{
			// deserialization writes the configuration properties before the component has an Entity
			if (Entity == null)
				return;

			DestroyFixture();
			CreateFixture();
		}
	}
}
