﻿// Copyright 2015 Xamarin Inc. All rights reserved.

#if !__WATCHOS__

using System;
#if XAMCORE_2_0
using Foundation;
using Metal;
using ObjCRuntime;
using SceneKit;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.Metal;
using MonoTouch.ObjCRuntime;
using MonoTouch.SceneKit;
using MonoTouch.UIKit;
#endif
using NUnit.Framework;

#if XAMCORE_2_0
using RectangleF=CoreGraphics.CGRect;
using SizeF=CoreGraphics.CGSize;
using PointF=CoreGraphics.CGPoint;
#else
using nfloat=global::System.Single;
using nint=global::System.Int32;
using nuint=global::System.UInt32;
#endif

namespace MonoTouchFixtures.SceneKit {

	[TestFixture]
	[Preserve (AllMembers = true)]
	public class GeometrySourceTest {

		[Test]
		public void FromMetalBuffer ()
		{
			if (!UIDevice.CurrentDevice.CheckSystemVersion (9,0))
				Assert.Inconclusive ("iOS 9+ required");
			if (Runtime.Arch != Arch.DEVICE)
				Assert.Inconclusive ("Metal tests only works on device so far");

			var device = MTLDevice.SystemDefault;
			if (device == null)
				Assert.Inconclusive ("Device does not support Metal");

			using (var buffer = device.CreateBuffer (1024, MTLResourceOptions.CpuCacheModeDefault)) {
				using (var source = SCNGeometrySource.FromMetalBuffer (buffer, MTLVertexFormat.Char2, SCNGeometrySourceSemantic.Vertex, 36, 0, 0)) {
					// the fact that it works means the lack of respondToSelector (in introspection tests) is no 
					// big deal and that the API really exists
					Assert.NotNull (source);
				}
			}
		}
	}
}

#endif // !__WATCHOS__
