using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlestar
{
	public static class InputManager
	{
		#region Update

		public static void HandleMouseInput(MouseState mouseState, Sprite turretSprite)
		{
			//if (mouseState.LeftButton == ButtonState.Pressed && turretSprite.IsBoxColliding(new Rectangle(mouseState.X, mouseState.Y, 2, 2)))
			//{
			//	Vector2 mouseMovement = Vector2.Zero;
			//	mouseMovement.X = (float)(mouseState.X - turretSprite.ScreenCenter.X);
			//	mouseMovement.Y = (float)(mouseState.Y - turretSprite.ScreenCenter.Y);

			//	BattleStar.TurretSprite.RotateTo(mouseMovement);
			//}

		}

		#endregion
	}
}
