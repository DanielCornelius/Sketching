﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public static class GeometryRenderer
	{
		static GeometryRenderer()
		{
			_renderers = new List<IGeometryRenderer>();

			AddRenderer(new StrokeRenderer());
			AddRenderer(new CircleRenderer());
			AddRenderer(new RectangleRenderer());
			AddRenderer(new TextRenderer());
			AddRenderer(new MarkRenderer());
		}
		private static List<IGeometryRenderer> _renderers;
		public static void AddRenderer(IGeometryRenderer renderer) 
		{
			var existing = _renderers.Where((arg) => arg.GeometryType.GetTypeInfo().IsAssignableFrom(renderer.GetType().GetTypeInfo()));
			if (existing.Any()) 
			{
				throw new InvalidOperationException("Duplicate renderer");
			}
			_renderers.Add(renderer);
		}
		public static void RemoveRenderer(IGeometryRenderer renderer) 
		{
			var r = _renderers.FirstOrDefault((arg) => arg.GeometryType == renderer.GeometryType);
			if (r != null) 
			{
				_renderers.Remove(r);
			}
		}
		public static void Render(SKCanvas canvas, IGeometryVisual geometry)
		{
			if (!geometry.IsValid) return;
			var renderer = _renderers.FirstOrDefault((arg) => arg.GeometryType.GetTypeInfo().IsAssignableFrom( geometry.GetType().GetTypeInfo()));
			if (renderer != null) renderer.Render(canvas, geometry);

		}

	}
}
