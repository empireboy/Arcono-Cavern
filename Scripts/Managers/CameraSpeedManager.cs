using Engine;
using Microsoft.Xna.Framework.Input;

namespace Arcono.Editor.Managers
{
	public class CameraSpeedManager : GameObject
	{
		private readonly float cameraSpeedFactor;

		public CameraSpeedManager()
		{
			cameraSpeedFactor = 3f;
		}

		public override void HandleInput(InputHelper inputHelper)
		{
			if (inputHelper.KeyPressed(Keys.LeftShift))
			{
				GameEnvironment.cameraMover.speed *= cameraSpeedFactor;
			}
			else if (inputHelper.KeyReleased(Keys.LeftShift))
			{
				GameEnvironment.cameraMover.speed /= cameraSpeedFactor;
			}
		}
	}
}