using Arcono.Editor;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Arcono
{
	public class ArconoEnvironment : GameEnvironment
    {
        protected override void Initialize()
        {
            screen = new Point(1920, 1080);
            ApplyResolutionSettings();
            graphics.ToggleFullScreen();

            gameStateList.Add(new Level());
            gameStateList.Add(new LevelEditor(this));
            gameStateList.Add(new Menu());
            gameStateList.Add(new LevelSelect(gameStateList[1] as LevelEditor));

            SwitchTo(2, true);


            ScreenHeight = graphics.PreferredBackBufferHeight;
            ScreenWidth = graphics.PreferredBackBufferWidth;

            // Play and initialize audio
            Song song = Content.Load<Song>("Arcono Cavern");
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);

            base.Initialize();
        }
	}
}
