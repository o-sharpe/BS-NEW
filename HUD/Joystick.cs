using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUD
{
	public static class Joystick
	{
		public static Sprite joystick;

		#region Init

		public static void Initialize(Texture2D joystickTexture, Rectangle joystickFrame, Vector2 worldLocation)
		{
			joystick = new Sprite(new Vector2(worldLocation.X, worldLocation.Y), joystickTexture, joystickFrame, Vector2.Zero);
		}

		#endregion

		#region Update and Draw

		public static void Draw(SpriteBatch spriteBatch)
		{
			joystick.Draw(spriteBatch);
		}

		#endregion

	}
}
