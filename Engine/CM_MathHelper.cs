using Microsoft.Xna.Framework;
using System;

namespace Engine
{
	public static class CM_MathHelper
	{
		public static float ClampAngleBetween360Degrees(float angle)
		{
			if (angle < 0)
				angle += 360;
			else if (angle > 360)
				angle -= 360;

			return angle;
		}
	}
}
