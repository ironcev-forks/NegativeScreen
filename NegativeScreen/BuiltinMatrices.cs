﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace NegativeScreen
{
	/// <summary>
	/// Store various built in ColorMatrix
	/// </summary>
	public static class BuiltinMatrices
	{
		/// <summary>
		/// no color transformation
		/// </summary>
		public static float[,] Identity { get; private set; }
		/// <summary>
		/// simple colors inversion
		/// </summary>
		public static float[,] Negative { get; private set; }
		public static float[,] GrayScale { get; private set; }
		public static float[,] Sepia { get; private set; }
		public static float[,] NegativeSepia { get; private set; }
		/// <summary>
		/// theoretical optimal transfomation (but ugly desaturated pure colors due to "overflows"...)
		/// Many thanks to Tom MacLeod who gave me the idea for these inversion modes
		/// </summary>
		public static float[,] NegativeHueShift180 { get; private set; }
		/// <summary>
		/// high saturation, good pure colors
		/// </summary>
		public static float[,] NegativeHueShift180Variation1 { get; private set; }
		/// <summary>
		/// overall desaturated, yellows and blue plain bad. actually relaxing and very usable
		/// </summary>
		public static float[,] NegativeHueShift180Variation2 { get; private set; }
		/// <summary>
		/// high saturation. yellows and blues  plain bad. actually quite readable
		/// </summary>
		public static float[,] NegativeHueShift180Variation3 { get; private set; }
		/// <summary>
		/// //not so readable, good colors (CMY colors a bit desaturated, still more saturated than normal)
		/// </summary>
		public static float[,] NegativeHueShift180Variation4 { get; private set; }

		//hue 180
		//{ -0.3333333f,  0.6666667f,  0.6666667f,  0.0f,  0.0f },
		//{ 0.6666667f,  -0.3333333f,  0.6666667f,  0.0f,  0.0f },
		//{ 0.6666667f,  0.6666667f,  -0.3333333f,  0.0f,  0.0f },
		//{ 0.0f,  0.0f,  0.0f,  1.0f,  0.0f },
		//{ 0.0f,  0.0f,  0.0f,  0.0f,  1.0f }

		static BuiltinMatrices()
		{
			Identity = new float[,] {
				{  1.0f,  0.0f,  0.0f,  0.0f,  0.0f },
				{  0.0f,  1.0f,  0.0f,  0.0f,  0.0f },
				{  0.0f,  0.0f,  1.0f,  0.0f,  0.0f },
				{  0.0f,  0.0f,  0.0f,  1.0f,  0.0f },
				{  0.0f,  0.0f,  0.0f,  0.0f,  1.0f }
			};
			Negative = new float[,] {
				{ -1.0f,  0.0f,  0.0f,  0.0f,  0.0f },
				{  0.0f, -1.0f,  0.0f,  0.0f,  0.0f },
				{  0.0f,  0.0f, -1.0f,  0.0f,  0.0f },
				{  0.0f,  0.0f,  0.0f,  1.0f,  0.0f },
				{  1.0f,  1.0f,  1.0f,  0.0f,  1.0f }
			};
			GrayScale = new float[,] {
				{  0.3f,  0.3f,  0.3f,  0.0f,  0.0f },
				{  0.6f,  0.6f,  0.6f,  0.0f,  0.0f },
				{  0.1f,  0.1f,  0.1f,  0.0f,  0.0f },
				{  0.0f,  0.0f,  0.0f,  1.0f,  0.0f },
				{  0.0f,  0.0f,  0.0f,  0.0f,  1.0f }
			};
			Sepia = new float[,] {
				{ .393f, .349f, .272f, 0.0f, 0.0f},
				{ .769f, .686f, .534f, 0.0f, 0.0f},
				{ .189f, .168f, .131f, 0.0f, 0.0f},
				{  0.0f,  0.0f,  0.0f, 1.0f, 0.0f},
				{  0.0f,  0.0f,  0.0f, 0.0f, 1.0f}
			};
			NegativeSepia = new float[,] {
				{ -0.393f, -0.349f, -0.272f,  0.0f,  0.0f },
				{ -0.769f, -0.686f, -0.534f,  0.0f,  0.0f },
				{ -0.189f, -0.168f, -0.131f,  0.0f,  0.0f },
				{    0.0f,    0.0f,    0.0f,  1.0f,  0.0f },
				{  1.351f,  1.203f,  0.937f,  0.0f,  1.0f }
			};
			NegativeHueShift180 = new float[,] {
				{ 0.3333333f,  -0.6666667f, -0.6666667f, 0.0f, 0.0f },
				{ -0.6666667f,  0.3333333f, -0.6666667f, 0.0f, 0.0f },
				{ -0.6666667f, -0.6666667f,  0.3333333f, 0.0f, 0.0f },
				{        0.0f,        0.0f,        0.0f, 1.0f, 0.0f },
				{        1.0f,        1.0f,        1.0f, 0.0f, 1.0f }
			};
			NegativeHueShift180Variation1 = new float[,] {
				//most simple working method for shifting hue 180deg.
				{  1.0f, -1.0f, -1.0f, 0.0f, 0.0f },
				{ -1.0f,  1.0f, -1.0f, 0.0f, 0.0f },
				{ -1.0f, -1.0f,  1.0f, 0.0f, 0.0f },
				{  0.0f,  0.0f,  0.0f, 1.0f, 0.0f },
				{  1.0f,  1.0f,  1.0f, 0.0f, 1.0f }
			};
			NegativeHueShift180Variation2 = new float[,] {
				//used QColorMatrix http://www.codeguru.com/Cpp/G-M/gdi/gdi/article.php/c3667 for generation
				{  0.39f, -0.62f, -0.62f, 0.0f, 0.0f },
				{ -1.21f, -0.22f, -1.22f, 0.0f, 0.0f },
				{ -0.16f, -0.16f,  0.84f, 0.0f, 0.0f },
				{   0.0f,   0.0f,   0.0f, 1.0f, 0.0f },
				{   1.0f,   1.0f,   1.0f, 0.0f, 1.0f }
			};
			NegativeHueShift180Variation3 = new float[,] {
				{     1.089508f,   -0.9326327f, -0.932633042f,  0.0f,  0.0f },
				{  -1.81771779f,    0.1683074f,  -1.84169245f,  0.0f,  0.0f },
				{ -0.244589478f, -0.247815639f,    1.7621845f,  0.0f,  0.0f },
				{          0.0f,          0.0f,          0.0f,  1.0f,  0.0f },
				{          1.0f,          1.0f,          1.0f,  0.0f,  1.0f }
			};
			NegativeHueShift180Variation4 = new float[,] {
				{  0.50f, -0.78f, -0.78f, 0.0f, 0.0f },
				{ -0.56f,  0.72f, -0.56f, 0.0f, 0.0f },
				{ -0.94f, -0.94f,  0.34f, 0.0f, 0.0f },
				{   0.0f,   0.0f,   0.0f, 1.0f, 0.0f },
				{   1.0f,   1.0f,   1.0f, 0.0f, 1.0f }
			};
		}

		public static void ChangeColorEffect(IntPtr hwndMag, float[,] matrix)
		{
			if (!NativeMethods.MagSetColorEffect(hwndMag, new ColorEffect(matrix)))
			{
				throw new Exception("MagSetColorEffect()", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));
			}
		}

		public static void ChangeColorEffect(IEnumerable<NegativeOverlay> overlays, float[,] matrix)
		{
			foreach (var item in overlays)
			{
				ChangeColorEffect(item.HwndMag, matrix);
			}
		}

		public static float[,] MoreBlue(float[,] colorMatrix)
		{
			float[,] temp = (float[,])colorMatrix.Clone();
			temp[2, 4] += 0.1f;//or remove 0.1 off the red
			return temp;
		}

		public static float[,] MoreGreen(float[,] colorMatrix)
		{
			float[,] temp = (float[,])colorMatrix.Clone();
			temp[1, 4] += 0.1f;
			return temp;
		}

		public static float[,] MoreRed(float[,] colorMatrix)
		{
			float[,] temp = (float[,])colorMatrix.Clone();
			temp[0, 4] += 0.1f;
			return temp;
		}
	}
}
