using Microsoft.Xna.Framework;
using System;

namespace Engine
{
	public static class Vector2Helper
	{
		public static float GetAngle(Vector2 point1, Vector2 point2)
		{
			float relX = point2.X - point1.X;
			float relY = point2.Y - point1.Y;

			return (float)Math.Atan2(relY, relX);
		}
	}
}
