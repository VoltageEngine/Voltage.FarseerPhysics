using System.Collections.Generic;
using FarseerPhysics;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using Voltage;


namespace Voltage.Farseer
{
	[ComponentId("fs_collision_ellipse")]
	public partial class FSCollisionEllipse : FSCollisionPolygon
	{
		float _xRadius = 0.1f;
		float _yRadius = 0.1f;
		int _edgeCount = Settings.MaxPolygonVertices;


		public FSCollisionEllipse()
		{
		}


		public FSCollisionEllipse(float xRadius, float yRadius) : this(xRadius, yRadius, Settings.MaxPolygonVertices)
		{
		}


		public FSCollisionEllipse(float xRadius, float yRadius, int edgeCount)
		{
			Insist.IsFalse(edgeCount > Settings.MaxPolygonVertices,
				"edgeCount must be less than Settings.maxPolygonVertices");

			_xRadius = xRadius;
			_yRadius = yRadius;
			_edgeCount = edgeCount;
			_areVertsDirty = true;
		}


		#region Configuration

		public float XRadius
		{
			get => _xRadius;
			set
			{
				_xRadius = value;
				_areVertsDirty = true;
				RecreateFixture();
			}
		}


		public float YRadius
		{
			get => _yRadius;
			set
			{
				_yRadius = value;
				_areVertsDirty = true;
				RecreateFixture();
			}
		}


		public int EdgeCount
		{
			get => _edgeCount;
			set
			{
				Insist.IsFalse(value > Settings.MaxPolygonVertices,
					"edgeCount must be less than Settings.maxPolygonVertices");

				_edgeCount = value;
				_areVertsDirty = true;
				RecreateFixture();
			}
		}


		public FSCollisionEllipse SetRadii(float xRadius, float yRadius)
		{
			_xRadius = xRadius;
			_yRadius = yRadius;
			_areVertsDirty = true;
			RecreateFixture();
			return this;
		}


		public FSCollisionEllipse SetEdgeCount(int edgeCount)
		{
			EdgeCount = edgeCount;
			return this;
		}

		#endregion


		protected override void RebuildVerts()
		{
			_verts = new List<Vector2>(PolygonTools.CreateEllipse(_xRadius * FSConvert.DisplayToSim,
				_yRadius * FSConvert.DisplayToSim, _edgeCount));
		}
	}
}
