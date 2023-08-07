using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcono.Editor.Managers
{
	public class LevelEditor_InfoRenderer : GameObject
	{
		private readonly LevelEditor_ItemManager itemManager;

		private readonly Texture2D infoBackgroundTexture;
		private readonly SpriteFont spriteFont;

		private Vector2 mousePosition;

		public LevelEditor_InfoRenderer(LevelEditor_ItemManager itemManager)
		{
			this.itemManager = itemManager;

			spriteFont = GameEnvironment.AssetManager.Content.Load<SpriteFont>("Font");
			infoBackgroundTexture = new Texture2D(GameEnvironment.Graphics.GraphicsDevice, 300, GameEnvironment.Screen.Y);

			// Set infoBackgroundTexture pixel data
			Color[] infoBackgroundTextureData = new Color[infoBackgroundTexture.Width * infoBackgroundTexture.Height];

			for (int i = 0; i < infoBackgroundTextureData.Length; ++i)
				infoBackgroundTextureData[i] = Color.Black;

			infoBackgroundTexture.SetData(infoBackgroundTextureData);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);

			DrawInfo(spriteBatch);
		}

		public override void HandleInput(InputHelper inputHelper)
		{
			base.HandleInput(inputHelper);

			mousePosition = GameEnvironment.cameraMover.position - new Vector2(GameEnvironment.Screen.X, GameEnvironment.Screen.Y) / 2 + inputHelper.MousePosition;
		}

		private void DrawInfo(SpriteBatch spriteBatch)
		{
			Vector2 startPosition = new Vector2(10, 10);
			float scale = 1f;
			float lineHeight = 20 * scale;
			float width = (210 - startPosition.Y) * scale;
			float mouseThreshold = 100;

			// Dont draw info if the mouse is hovering over the text
			if (
				mousePosition.X <= GameEnvironment.cameraMover.position.X - GameEnvironment.ScreenWidth / 2 + 64 + startPosition.X + width + mouseThreshold &&
				mousePosition.X >= GameEnvironment.cameraMover.position.X - GameEnvironment.ScreenWidth / 2 + 64 + startPosition.X - mouseThreshold
			)
				return;

			// Initialize info text
			InfoText[] infoTexts = new InfoText[14]
			{
				new InfoText(spriteFont, Color.LightGreen, "Selected Item: " + itemManager.SelectedItem.ToString()),
				new InfoText(spriteFont, Color.LightGray, "> Next selected item"),
				new InfoText(spriteFont, Color.LightGray, "   {E, ScrollWheelUp}"),
				new InfoText(spriteFont, Color.LightGray, "< Previous selected item"),
				new InfoText(spriteFont, Color.LightGray, "   {Q, ScrollWheelDown}"),
				new InfoText(spriteFont, Color.LightGray, "Move camera {W, A, S, D}"),
				new InfoText(spriteFont, Color.LightGray, "Faster camera speed {Left Shift}"),
				new InfoText(spriteFont, Color.LightGray, "Resize grid {Arrow Keys}"),
				new InfoText(spriteFont, Color.LightGreen, "Play {P}"),
				new InfoText(spriteFont, Color.LightSeaGreen, "- NP is NumPad"),
				new InfoText(spriteFont, Color.LightGray, "Save Level {NP1, NP2, NP3, NP4}"),
				new InfoText(spriteFont, Color.LightGray, "Load Level {F1, F2, F3, F4}"),
				new InfoText(spriteFont, Color.LightBlue, "Place Item {Mouse Left}"),
				new InfoText(spriteFont, Color.LightBlue, "Remove Item {Mouse Right}")
			};

			Vector2 drawPosition = new Vector2(
				GameEnvironment.cameraMover.position.X - ArconoEnvironment.ScreenWidth / 2 + 64,
				GameEnvironment.cameraMover.position.Y - ArconoEnvironment.ScreenHeight / 2 + 64
			);

			// Draw background
			spriteBatch.Draw(infoBackgroundTexture, drawPosition, Color.White * 0.9f);

			DrawInfoText(spriteBatch, infoTexts, drawPosition + startPosition, lineHeight, scale);
		}

		private void DrawInfoText(SpriteBatch spriteBatch, InfoText[] infoTexts, Vector2 startPosition, float lineHeight, float scale)
		{
			for (int i = 0; i < infoTexts.Length; i++)
				infoTexts[i].Draw(spriteBatch, startPosition, lineHeight, i, scale);
		}

		private class InfoText
		{
			public SpriteFont Font { get; set; }
			public Color TextColor { get; set; }
			public string Text { get; set; }

			public InfoText(SpriteFont font, Color textColor, string text)
			{
				Font = font;
				TextColor = textColor;
				Text = text;
			}

			public void Draw(SpriteBatch spriteBatch, Vector2 startPosition, float lineHeight, int lineIndex, float scale)
			{
				spriteBatch.DrawString(Font, Text, new Vector2(startPosition.X, startPosition.Y + lineHeight * lineIndex), TextColor, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
			}
		}
	}
}
