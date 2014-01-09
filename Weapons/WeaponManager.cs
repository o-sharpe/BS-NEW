using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weapons
{
	public static class WeaponManager
	{
		#region declarations
		static public List<Particle> PlayerShots = new List<Particle>();
		static public List<Particle> EnemyShots = new List<Particle>();
		static public Texture2D PlayerTexture;
		static public Texture2D EnemyTexture;
		static public Rectangle PlayerShotRectangle = new Rectangle();
		static public Rectangle EnemyShotRectangle = new Rectangle();
		static public float WeaponSpeed = 600f;

		static private float shotTimer = 0f;
		static private float shotMinTimer = 0.20f;
		public enum WeaponType { Normal };
		static public WeaponType CurrentWeaponType = WeaponType.Normal;
		static public float WeaponTimeRemaining = 0.0f;

		#endregion

		#region Init

		public static void Initialize(Texture2D playerTexture, Texture2D enemyTexture, Rectangle playerShotRectangle, Rectangle enemyShotRectangle)
		{
			PlayerTexture = playerTexture;
			EnemyTexture = enemyTexture;
			PlayerShotRectangle = playerShotRectangle;
			EnemyShotRectangle = enemyShotRectangle;
		}

		#endregion

		#region Properties
		static public float WeaponFireDelay
		{
			get
			{
				return shotMinTimer;
			}
		}


		static public bool CanFireWeapon
		{
			get
			{
				return (shotTimer >= WeaponFireDelay);
			}
		}
		#endregion

		#region Effects Management Methods

		private static void AddShot(Vector2 location, Vector2 velocity,	int frame, bool isPlayer)
		{
			if (isPlayer)
			{
				Particle shot = new Particle(
					location,
					PlayerTexture,
					PlayerShotRectangle,
					velocity,
					Vector2.Zero,
					400f,
					120,
					Color.White,
					Color.White);

				shot.AddFrame(new Rectangle(
					PlayerShotRectangle.X + PlayerShotRectangle.Width,
					PlayerShotRectangle.Y,
					PlayerShotRectangle.Width,
					PlayerShotRectangle.Height));

				shot.Animate = false;
				shot.Frame = frame;
				shot.RotateTo(velocity);
				PlayerShots.Add(shot);
			}
			else
			{
				Particle shot = new Particle(
					location,
					EnemyTexture,
					EnemyShotRectangle,
					velocity,
					Vector2.Zero,
					400f,
					120,
					Color.White,
					Color.White);

				shot.AddFrame(new Rectangle(
					PlayerShotRectangle.X + PlayerShotRectangle.Width,
					PlayerShotRectangle.Y,
					PlayerShotRectangle.Width,
					PlayerShotRectangle.Height));

				shot.Animate = false;
				shot.Frame = frame;
				shot.RotateTo(velocity);
				EnemyShots.Add(shot);
			}

		}


		//private static void createLargeExplosion(Vector2 location)
		//{
		//	EffectsManager.AddLargeExplosion(
		//		location + new Vector2(-10, -10));
		//	EffectsManager.AddLargeExplosion(
		//		location + new Vector2(-10, 10));
		//	EffectsManager.AddLargeExplosion(
		//		location + new Vector2(10, 10));
		//	EffectsManager.AddLargeExplosion(
		//		location + new Vector2(10, -10));
		//	EffectsManager.AddLargeExplosion(location);
		//}
		#endregion

		#region Weapons Management Methods
		private static void checkWeaponUpgradeExpire(float elapsed)
		{
			if (CurrentWeaponType != WeaponType.Normal)
			{
				WeaponTimeRemaining -= elapsed;
				if (WeaponTimeRemaining <= 0)
				{
					CurrentWeaponType = WeaponType.Normal;
				}
			}
		}

		public static void FireWeapon(Vector2 location, Vector2 velocity, bool isPlayer)
		{
			switch (CurrentWeaponType)
			{
				case WeaponType.Normal:
					AddShot(location, velocity, 0, isPlayer);
					break;
			}

			shotTimer = 0.0f;
		}


		//private static void tryToSpawnPowerup(int x, int y, WeaponType type)
		//{
		//	if (PowerUps.Count >= maxActivePowerups)
		//	{
		//		return;
		//	}

		//	Rectangle thisDestination =	TileMap.SquareWorldRectangle(new Vector2(x, y));

		//	foreach (Sprite powerup in PowerUps)
		//	{
		//		if (powerup.WorldRectangle == thisDestination)
		//		{
		//			return;
		//		}
		//	}

		//	if (!(PathFinder.FindPath(
		//		new Vector2(x, y),
		//		Player.PathingNodePosition) == null))
		//	{
		//		Sprite newPowerup = new Sprite(
		//			new Vector2(thisDestination.X, thisDestination.Y),
		//			Texture,
		//			new Rectangle(64, 128, 32, 32),
		//			Vector2.Zero);
		//		newPowerup.Animate = false;
		//		newPowerup.CollisionRadius = 14;
		//		newPowerup.AddFrame(new Rectangle(96, 128, 32, 32));
		//		if (type == WeaponType.Rocket)
		//			newPowerup.Frame = 1;
		//		PowerUps.Add(newPowerup);
		//		timeSinceLastPowerup = 0.0f;
		//	}
		//}

		//private static void checkPowerupSpawns(float elapsed)
		//{
		//	timeSinceLastPowerup += elapsed;
		//	if (timeSinceLastPowerup >= timeBetweenPowerups)
		//	{
		//		WeaponType type = WeaponType.Triple;
		//		if (rand.Next(0, 2) == 1)
		//		{
		//			type = WeaponType.Rocket;
		//		}
		//		tryToSpawnPowerup(
		//			rand.Next(0, TileMap.MapWidth),
		//			rand.Next(0, TileMap.MapHeight),
		//			type);
		//	}
		//}

		#endregion

		#region Collision Detection

		//private static void checkShotWallImpacts(Sprite shot)
		//{
		//	if (shot.Expired)
		//	{
		//		return;
		//	}

		//	if (TileMap.IsWallTile(
		//		TileMap.GetSquareAtPixel(shot.WorldCenter)))
		//	{
		//		shot.Expired = true;

		//		if (shot.Frame == 0)
		//		{
		//			EffectsManager.AddSparksEffect(
		//				shot.WorldCenter,
		//				shot.Velocity);
		//		}
		//		else
		//		{
		//			createLargeExplosion(shot.WorldCenter);
		//			checkRocketSplashDamage(shot.WorldCenter);
		//		}
		//	}
		//}

		//private static void checkShotEnemyImpacts(Sprite shot)
		//{
		//	if (shot.Expired)
		//	{
		//		return;
		//	}

		//	foreach (Enemy enemy in EnemyManager.EnemyManager.Enemies)
		//	{
		//		if (!enemy.Destroyed)
		//		{
		//			if (shot.IsCircleColliding(	enemy.EnemyBase.WorldCenter, enemy.EnemyBase.CollisionRadius))
		//			{
		//				shot.Expired = true;
		//				Screen.Effects.AddSparksEffect(shot.WorldCenter, shot.Velocity);
		//				enemy.Health--;
		//				if (enemy.Health == 0)
		//				{
		//					enemy.Destroyed = true;
		//					Screen.Effects.AddExplosion(shot.WorldCenter, shot.Velocity / 30);
		//					//Screen.Effects.AddExplosion(enemy.EnemyBase.WorldCenter, enemy.EnemyBase.Velocity / 30);
		//				}
		//			}
		//		}
		//	}
		//}

		#endregion

		#region Update and Draw
		static public void Update(GameTime gameTime)
		{
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			shotTimer += elapsed;
			checkWeaponUpgradeExpire(elapsed);

			for (int x = PlayerShots.Count - 1; x >= 0; x--)
			{
				PlayerShots[x].Update(gameTime);

				if (PlayerShots[x].Expired)
				{
					PlayerShots.RemoveAt(x);
				}
			}
			for (int x = EnemyShots.Count - 1; x >= 0; x--)
			{
				EnemyShots[x].Update(gameTime);

				if (EnemyShots[x].Expired)
				{
					EnemyShots.RemoveAt(x);
				}
			}
		}

		static public void Draw(SpriteBatch spriteBatch)
		{
			foreach (Particle sprite in PlayerShots)
			{
				sprite.Draw(spriteBatch);
			}
			foreach (Particle sprite in EnemyShots)
			{
				sprite.Draw(spriteBatch);
			}
		}
		#endregion
	}
}
