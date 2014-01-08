using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlestar
{
	public static class BattleStar
	{
		#region Declarations

		public static Sprite BaseSprite;
		public static List<Sprite> TurretSprites = new List<Sprite>();
		public static Vector2 FireAngle = Vector2.Zero;
		public static Vector2 baseAngle = Vector2.Zero;
		private static float playerSpeed = 90f;
		private static Rectangle scrollArea = new Rectangle(150, 100, 500, 400);
		private static Random rand = new Random();

		#endregion

		#region Properties

		//public static Vector2 PathingNodePosition
		//{
		//	get { return TileMap.GetSquareAtPixel(BaseSprite.WorldCenter); }
		//}

		#endregion

		#region Initialization

		public static void Initialize(Texture2D texture, Rectangle baseInitialFrame, int baseFrameCount, Texture2D textureTurret, Rectangle turretInitialFrame, Vector2 worldLocation)
		{
			int frameWidth = baseInitialFrame.Width;
			int frameHeight = baseInitialFrame.Height;

			BaseSprite = new Sprite(worldLocation, texture, baseInitialFrame, Vector2.Zero);
			BaseSprite.BoundingXPadding = 4;
			BaseSprite.BoundingYPadding = 4;
			BaseSprite.AnimateWhenStopped = false;
			for (int x = 1; x < baseFrameCount; x++)
				BaseSprite.AddFrame(new Rectangle(baseInitialFrame.X + (frameHeight * x), baseInitialFrame.Y, frameWidth, frameHeight));

			for (int x = 0; x < 3; x++)
			{
				TurretSprites.Add(new Sprite(new Vector2(BaseSprite.WorldLocation.X + 50 + x * 100 - (float)15, BaseSprite.WorldCenter.Y - (float)28), textureTurret, turretInitialFrame, Vector2.Zero));
				TurretSprites[x].RotateTo(new Vector2(0, -1));
			}
		}

		#endregion

		#region Update and Draw !

		public static void Update(GameTime gameTime)
		{
		//	InputManager.HandleMouseInput(Mouse.GetState(), TurretSprite);	


			if (Weapons.WeaponManager.CanFireWeapon && FireAngle != Vector2.Zero)
			{
				for (int i = 0; i < TurretSprites.Count; i++)
				{
					//Vector2 left = new Vector2(TurretSprites[i].ScreenRectangle.X, TurretSprites[i].ScreenRectangle.Y);
					//Vector2 right = new Vector2(TurretSprites[i].ScreenRectangle.X + TurretSprites[i].ScreenRectangle.Height, TurretSprites[i].ScreenRectangle.Y);
					//if (rand.Next(1, 3) == 1)
					//	Weapons.WeaponManager.FireWeapon(TurretSprites[i].ScreenCenter, FireAngle * Weapons.WeaponManager.WeaponSpeed);
					//else
					//	Weapons.WeaponManager.FireWeapon(right, FireAngle * Weapons.WeaponManager.WeaponSpeed);

					Weapons.WeaponManager.FireWeapon(new Vector2(TurretSprites[i].ScreenCenter.X - Weapons.WeaponManager.ShotRectangle.Width / 2, TurretSprites[i].ScreenCenter.Y - Weapons.WeaponManager.ShotRectangle.Height / 2), FireAngle * Weapons.WeaponManager.WeaponSpeed);
				}
			}
		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			BaseSprite.Draw(spriteBatch);
			for (int x = 0; x < 3; x++)
				TurretSprites[x].Draw(spriteBatch);
		}

		#endregion
	}
}
