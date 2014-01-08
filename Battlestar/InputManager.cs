using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Battlestar
{
	public static class InputManager
	{
		#region Handling Methods

		public static void HandleMouseInput(TouchCollection mouseState, Sprite turretSprite)
		{
			//if (mouseState.LeftButton == ButtonState.Pressed && turretSprite.IsBoxColliding(new Rectangle(mouseState.X, mouseState.Y, 2, 2)))
			//{
			//	Vector2 mouseMovement = Vector2.Zero;
			//	mouseMovement.X = (float)(mouseState.X - turretSprite.ScreenCenter.X);
			//	mouseMovement.Y = (float)(mouseState.Y - turretSprite.ScreenCenter.Y);

			//	BattleStar.TurretSprite.RotateTo(mouseMovement);
			//}

		}

		public static void HandleJoystickTouch(TouchCollection touchState, Sprite joystick)
		{
			foreach (TouchLocation tl in touchState)
			{
				var touchPos = new Vector2(tl.Position.Y, Camera.PhoneHeightAndWidth.Y - tl.Position.X);
				var test = joystick.ScaledScreenRectangle;
				var test2 = joystick.ScaledScreenPosition;
				if (joystick.ScaledScreenRectangle.Contains(tl.Position.Y, Camera.PhoneHeightAndWidth.Y - tl.Position.X))
				{
					foreach (Sprite turret in BattleStar.TurretSprites)
					{
						Vector2 touchFromCenter = Vector2.Zero;
						touchFromCenter.X = (float)(tl.Position.Y - joystick.ScaledScreenPosition.X);
						touchFromCenter.Y = (float)(Camera.PhoneHeightAndWidth.Y - tl.Position.X - joystick.ScaledScreenPosition.Y);
						touchFromCenter.Normalize();
						BattleStar.FireAngle = touchFromCenter;
						turret.RotateTo(touchFromCenter);
					}
				}
			}
		}

		#endregion
	}
}
