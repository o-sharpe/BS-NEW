using Microsoft.Xna.Framework;
using Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsManager
{
	public static class PhysicsManager
	{
		private static bool checkShotImpact(Sprite shot, EnemyManager.Enemy en)
		{
			if (shot.Expired)
			{
				return false;
			}

			if (en != null)
			{
				if (shot.IsCircleColliding(en.EnemyBase.ScreenCenter, en.EnemyBase.CollisionRadius))
				{
					shot.Expired = true;
					Screen.Effects.AddSparksEffect(shot.WorldCenter, shot.Velocity);
					GameManager.GameManager.UpdateScore(en);
					return true;
				}
			}
			else
			{
				if (shot.IsBoxColliding(new Rectangle(Battlestar.BattleStar.BaseSprite.ScreenRectangle.X + 20, Battlestar.BattleStar.BaseSprite.ScreenRectangle.Y + 40, 260, 29)))
				{
					shot.Expired = true;
					Screen.Effects.AddSparksEffect(shot.WorldCenter, shot.Velocity);
					return true;
				}
			}


			return false;
		}

		public static void Update(GameTime gameTime)
		{
			foreach (var shot in Weapons.WeaponManager.PlayerShots)
			{
				foreach (var enemy in EnemyManager.EnemyManager.Enemies)
				{
					if (checkShotImpact(shot, enemy))
						enemy.Health--;
				}
			}
			foreach (var shot in Weapons.WeaponManager.EnemyShots)
			{
				if(checkShotImpact(shot, null))
					Battlestar.BattleStar.Damage(1);
			}
		}
	}
}
