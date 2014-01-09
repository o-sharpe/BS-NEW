using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Screen;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;

namespace HUD
{
	public static class Buttons
	{
		#region Declarations

		static List<Sprite> buttonsList = new List<Sprite>();
		static List<SpriteFont> fontList = new List<SpriteFont>();

		#endregion

		#region Adding Elements

		public static void AddButton(Texture2D buttonTexture, Rectangle turretFrame, Vector2 worldLocation, SpriteFont spriteFont)
		{
			buttonsList.Add(new Sprite(new Vector2(worldLocation.X, worldLocation.Y + 100 * buttonsList.Count), buttonTexture, turretFrame, Vector2.Zero));
			buttonsList[buttonsList.Count - 1].RotateTo(new Vector2(0, -1));
			fontList.Add(spriteFont);
		}

		#endregion

		#region Update and Draw

		public static void Draw(SpriteBatch spriteBatch)
		{
			for (int x = 0; x < buttonsList.Count; x++)
			{
				buttonsList[x].Draw(spriteBatch);
				spriteBatch.DrawString(fontList[x], string.Format("Gun: {0}", x + 1), new Vector2(buttonsList[x].ScreenLocation.X - buttonsList[x].ScreenRectangle.Width / 2, buttonsList[x].ScreenLocation.Y + buttonsList[x].ScreenRectangle.Height), Color.White);
			}
		}

		#endregion
	}
}
