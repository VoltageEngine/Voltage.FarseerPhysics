using System.Collections.Generic;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using Voltage;
using Transform = Voltage.Transform;


namespace Voltage.Farseer
{
	[ComponentId("fs_collision_chain")]
	public partial class FSCollisionChain : FSCollisionShape
	{
		List<Vector2> _verts = DefaultVerts();
		bool _loop;


		public FSCollisionChain()
		{
			_fixtureDef.Shape = new ChainShape();
		}


		public FSCollisionChain(List<Vector2> verts) : this()
		{
			_verts = verts;
		}


		public FSCollisionChain(Vector2[] verts) : this()
		{
			_verts = new List<Vector2>(verts);
		}


		#region Configuration

		/// <summary>
		/// the chain's points, in display units. They are scaled by the Transform and converted to sim
		/// units when the fixture is built.
		/// </summary>
		public List<Vector2> Verts
		{
			get => _verts;
			set
			{
				// a scene written before this was authorable has no verts at all, and the generated
				// setter turns that into an empty list — which would build a zero-point chain.
				_verts = value == null || value.Count == 0 ? DefaultVerts() : value;
				RecreateFixture();
			}
		}


		public bool Loop
		{
			get => _loop;
			set
			{
				_loop = value;
				RecreateFixture();
			}
		}


		public FSCollisionChain SetLoop(bool loop)
		{
			Loop = loop;
			return this;
		}


		public FSCollisionChain SetVertices(Vertices verts)
		{
			Verts = new List<Vector2>(verts);
			return this;
		}


		public FSCollisionChain SetVertices(List<Vector2> verts)
		{
			Verts = verts;
			return this;
		}


		public FSCollisionChain SetVertices(Vector2[] verts)
		{
			Verts = new List<Vector2>(verts);
			return this;
		}

		#endregion


		public override void OnEntityTransformChanged(Transform.Component comp)
		{
			if (comp == Transform.Component.Scale)
				RecreateFixture();
		}


		/// <summary>
		/// the chain's points have to reach the shape before the fixture is built, or a chain restored
		/// from a scene file is built from the placeholder default.
		/// </summary>
		internal override void CreateFixture()
		{
			UpdateShape();
			base.CreateFixture();
		}


		void UpdateShape()
		{
			Insist.IsNotNull(_verts, "verts cannot be null!");

			// scale our verts and convert them to sim units
			var verts = new Vertices(_verts);
			verts.Scale((Entity != null ? Transform.Scale : Vector2.One) * FSConvert.DisplayToSim);

			var chainShape = _fixtureDef.Shape as ChainShape;
			chainShape.SetVertices(verts, _loop);
		}


		void RecreateFixture()
		{
			// deserialization writes the configuration properties before the component has an Entity
			if (Entity == null)
				return;

			DestroyFixture();
			CreateFixture();
		}


		/// <summary>
		/// a short two-point chain, so a chain added from the editor and never configured is a valid
		/// shape rather than an assert at fixture-creation time.
		/// </summary>
		static List<Vector2> DefaultVerts() =>
			new List<Vector2> { new Vector2(-0.05f, 0), new Vector2(0.05f, 0) };
	}
}
