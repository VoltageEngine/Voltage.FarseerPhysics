using System.Collections.Generic;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;


namespace Voltage.Farseer
{
	[ComponentId("fs_collision_box")]
	public partial class FSCollisionBox : FSCollisionPolygon
	{
		float _width = 0.1f;
		float _height = 0.1f;


		public FSCollisionBox()
		{
		}


		public FSCollisionBox(float width, float height)
		{
			_width = width;
			_height = height;
			_areVertsDirty = true;
		}


		#region Configuration

		public float Width
		{
			get => _width;
			set
			{
				_width = value;
				_areVertsDirty = true;
				RecreateFixture();
			}
		}


		public float Height
		{
			get => _height;
			set
			{
				_height = value;
				_areVertsDirty = true;
				RecreateFixture();
			}
		}


		public FSCollisionBox SetSize(float width, float height)
		{
			_width = width;
			_height = height;
			_areVertsDirty = true;
			RecreateFixture();
			return this;
		}

		#endregion


		protected override void RebuildVerts()
		{
			_verts = new List<Vector2>(PolygonTools.CreateRectangle(FSConvert.DisplayToSim * _width / 2,
				FSConvert.DisplayToSim * _height / 2));
		}
	}
}
