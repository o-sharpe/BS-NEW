using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen
{
	public static class StarField
	{
		#region ---- Members ----

		private static List<Sprite> stars = new List<Sprite>();
		private static int screenWidth = 800;
		private static int screenHeight = 600;
		private static Random rand = new Random();
		private static Color[] colors = { Color.White, Color.Yellow, Color.Wheat, Color.WhiteSmoke, Color.SlateGray, Color.LightGoldenrodYellow };

		#endregion

		#region Properties

		public static List<Sprite> Stars
		{
			get { return stars; }
		}

		public static int ScreenWidth
		{
			get { return screenWidth; }
			set { screenWidth = value; }
		}

		public static int ScreenHeight
		{
			get { return screenHeight; }
			set { screenHeight = value; }
		}

		#endregion

		public static void Initialize(int screenWidth, int screenHeight, int starCount, Vector2 starVelocity, Texture2D texture, Rectangle frameRectangle)
		{
			ScreenHeight = screenHeight;
			ScreenWidth = screenWidth;

			for (int x = 0; x < starCount; x++)
			{
				stars.Add(new Sprite(new Vector2(rand.Next(0, screenWidth), rand.Next(0, screenHeight)), texture, frameRectangle, starVelocity));
				Color starColor = colors[rand.Next(0, colors.Count())];
				starColor *= (float)(rand.Next(30, 80) / 100f);
				stars[stars.Count() - 1].TintColor = starColor;
			}
		}

		#region ---- Methods ----

		public static void Draw(SpriteBatch spriteBatch)
		{
			foreach (Sprite star in stars)
			{
				star.Draw(spriteBatch);
			}
		}

		#endregion
	}
}
