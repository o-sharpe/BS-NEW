using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weapons;

namespace EnemyManager
{
	public static class EnemyManager
	{
		#region Declarations
		public static List<Enemy> Enemies = new List<Enemy>();
		public static List<Texture2D> enemyTextures = new List<Texture2D>();
		public static List<Rectangle> enemyInitialFrames = new List<Rectangle>();
		public static int MaxActiveEnemies = 15;
		public static bool Active = false;

		private static float nextWaveTimer = 0.0f; 
		private static int MinShipsPerWave = 3;
		private static int MaxShipsPerWave = 6;
		private static float nextWaveMinTimer = 8.0f;
		private static float shipSpawnTimer = 0.0f;
		private static float shipSpawnWaitTime = 1.0f;
		private static float shipShotChance = 0.2f;
		private static List<List<Vector2>> pathWaypoints = new List<List<Vector2>>();
		private static Dictionary<int, int> waveSpawns = new Dictionary<int, int>();
		private static Random rand = new Random();
		#endregion

		#region Initialization
		public static void Initialize(Texture2D raiderTexture, Rectangle raiderInitialFrame, Texture2D baseStarTexture, Rectangle baseStarInitialFrame)
		{
			enemyTextures.Add(raiderTexture);
			enemyTextures.Add(baseStarTexture);
			enemyInitialFrames.Add(raiderInitialFrame);
			enemyInitialFrames.Add(baseStarInitialFrame);

			SetUpWaypoints();
		}
		#endregion

		#region Enemy Management
		public static void AddRaider(int path)
		{
			Enemy newEnemy = new Enemy(pathWaypoints[path][0], enemyTextures[0], enemyInitialFrames[0], 8, 5);
			for (int x = 0; x < pathWaypoints[path].Count(); x++)
			{
				newEnemy.AddWaypoint(pathWaypoints[path][x]);
			}
			Enemies.Add(newEnemy);
		}

		public static void AddBaseStar(Vector2 worldLocation)
		{
			Enemy newEnemy = new Enemy(worldLocation, enemyTextures[1], enemyInitialFrames[1], 75, 100);
			Enemies.Add(newEnemy);
		}

		private static void CreateWaypoint()
		{

		}

		private static void SetUpWaypoints()
		{
			List<Vector2> path0 = new List<Vector2>();
			path0.Add(new Vector2(850, 300));
			path0.Add(new Vector2(-100, 300));
			pathWaypoints.Add(path0);
			waveSpawns[0] = 0;

			List<Vector2> path1 = new List<Vector2>();
			path1.Add(new Vector2(-50, 225));
			path1.Add(new Vector2(850, 225));
			pathWaypoints.Add(path1);
			waveSpawns[1] = 0;

			List<Vector2> path2 = new List<Vector2>();
			path2.Add(new Vector2(-100, 50));
			path2.Add(new Vector2(150, 50));
			path2.Add(new Vector2(200, 75));
			path2.Add(new Vector2(200, 125));
			path2.Add(new Vector2(150, 150));
			path2.Add(new Vector2(150, 175));
			path2.Add(new Vector2(200, 200));
			path2.Add(new Vector2(600, 200));
			path2.Add(new Vector2(850, 600));
			pathWaypoints.Add(path2);
			waveSpawns[2] = 0;

			List<Vector2> path3 = new List<Vector2>();
			path3.Add(new Vector2(600, -100));
			path3.Add(new Vector2(600, 250));
			path3.Add(new Vector2(580, 275));
			path3.Add(new Vector2(500, 250));
			path3.Add(new Vector2(500, 200));
			path3.Add(new Vector2(450, 175));
			path3.Add(new Vector2(400, 150));
			path3.Add(new Vector2(-100, 150));
			pathWaypoints.Add(path3);
			waveSpawns[3] = 0;
		}

		private static void UpdateWaveSpawns(GameTime gameTime)
		{
			shipSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (shipSpawnTimer > shipSpawnWaitTime)
			{
				for (int x = waveSpawns.Count - 1; x >= 0; x--)
				{
					if (waveSpawns[x] > 0)
					{
						waveSpawns[x]--;
						AddRaider(x);
					}
				}
				shipSpawnTimer = 0f;
			}

			nextWaveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (nextWaveTimer > nextWaveMinTimer)
			{
				SpawnWave(rand.Next(0, pathWaypoints.Count));
				nextWaveTimer = 0f;
			}
		}

		private static void SpawnWave(int waveType)
		{
			waveSpawns[waveType] +=
				rand.Next(MinShipsPerWave, MaxShipsPerWave + 1);
		}
		#endregion

		#region Update and Draw
		public static void Update(GameTime gameTime)
		{
			for (int x = Enemies.Count - 1; x >= 0; x--)
			{
				Enemies[x].Update(gameTime);

				if (Enemies[x].IsActive() == false)
				{
					Enemies.RemoveAt(x);
				}
				else
				{
					if ((float)rand.Next(0, 1000) / 10 <= shipShotChance)
					{
						Vector2 fireLoc = Enemies[x].EnemyBase.ScreenLocation;
						fireLoc += Enemies[x].gunOffset;

						var aiming = new Vector2(rand.Next(-300, 300), rand.Next(-300, 300));
						Vector2 shotDirection = Battlestar.BattleStar.BaseSprite.ScreenCenter + aiming - fireLoc;

						shotDirection.Normalize();

						WeaponManager.FireWeapon(fireLoc, shotDirection * Weapons.WeaponManager.WeaponSpeed, false);
					}
				}
			}

			if (Active)
			{
				UpdateWaveSpawns(gameTime);
			}
		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			foreach (Enemy enemy in Enemies)
			{
				enemy.Draw(spriteBatch);
			}
		}
		#endregion
	}
}
