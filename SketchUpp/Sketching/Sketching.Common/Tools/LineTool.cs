﻿using System.Collections.Generic;
using Sketching.Common.Helper;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class LineTool : StrokeToolBase
	{
		private int GridSize => Config.GridSize;

		public LineTool() : this(ToolNames.LineTool, 1, 20, 8, null) { }

		public LineTool(string name, double minSize, double maxSize, double startSize, IEnumerable<Color> customColors)
		{
			CanUseFill = false;
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			Geometry.HighLight = false;
			CustomColors = customColors;
		}

		private void Snap(ref Point p)
		{
			if (GridSize < 0) return;
			var x = (int)((p.X + GridSize / 2.0) / GridSize) * GridSize;
			var y = (int)((p.Y + GridSize / 2.0) / GridSize) * GridSize;
			p.X = x;
			p.Y = y;

		}

		public override void TouchStart(Point p)
		{
			base.TouchStart(p);
			Snap(ref p);
			AddPoint(p);

		}

		public override void TouchMove(Point p)
		{
			base.TouchMove(p);
			Snap(ref p);
			AddPoint(p);

		}

		public override void TouchEnd(Point p)
		{
			base.TouchEnd(p);
			CreateNewGeometry();
		}

		private void AddPoint(Point p)
		{
			if (Geometry.Points.Count < 2)
			{
				Geometry.Points.Add(p);
			}
			else if (Geometry.Points.Count == 2)
			{
				Geometry.Points[1] = p;
			}
		}
	}
}
