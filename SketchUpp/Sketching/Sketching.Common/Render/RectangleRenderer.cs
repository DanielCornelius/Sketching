﻿using System;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public class RectangleRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IRectangle);

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale)
		{
			var rect = gemoetry as IRectangle;
			if (rect == null || !rect.IsValid) return;
			using (var paint = new SKPaint())
			{
				// Draw Rect
				paint.Color = rect.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)(rect.Size * scale);
				canvas.DrawRect(Helper.Converter.ToSKRect(rect.Start, rect.End, scale), paint);
				// Fill Rect
				if (!rect.IsFilled) return;
				paint.Color = rect.Color.ToFillColor().ToSkiaColor();
				paint.IsStroke = false;
				canvas.DrawRect(Helper.Converter.ToSKRect(rect.Start, rect.End, scale), paint);
			}
		}
	}
}
