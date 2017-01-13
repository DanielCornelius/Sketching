﻿using Sketching.Common;
using Sketching.UWP;
using Sketching.UWP.Helper;

// ReSharper disable once CheckNamespace
namespace Sketching
{
	public static class Platform
	{
		public static void Init()
		{
			Bootstrap.Init();
			
			Sketching.Common.Helper.Image.ImageMetaDataImplementation = new ImageMetaData();
#pragma warning disable 219

			var ignore1 = new SketchAreaRenderer();
			var ignore2 = new FixedRotationPageRenderer();

#pragma warning restore 219

		}
	}
}
