using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemyManager
{
	public static class EnemyManager
	{
		#region Declarations
		public static List<Enemy> Enemies = new List<Enemy>();
		public static List<Texture2D> enemyTextures = new List<Texture2D>();
		public static List<Rectangle> enemyInitialFrames = new List<Rectangle>();
		public static int MaxActiveEnemies = 30;
		#endregion

		#region Initialization
		public static void Initialize(Texture2D raiderTexture, Rectangle raiderInitialFrame, Texture2D baseStarTexture, Rectangle baseStarInitialFrame)
		{
			enemyTextures.Add(raiderTexture);
			enemyTextures.Add(baseStarTexture);
			enemyInitialFrames.Add(raiderInitialFrame);
			enemyInitialFrames.Add(baseStarInitialFrame);
		}
		#endregion

		#region Enemy Management
		public static void AddRaider(Vector2 worldLocation)
		{
			Enemy newEnemy = new Enemy(worldLocation, enemyTextures[0], enemyInitialFrames[0], 8, 10);
			Enemies.Add(newEnemy);
		}

		public static void AddBaseStar(Vector2 worldLocation)
		{
			Enemy newEnemy = new Enemy(worldLocation, enemyTextures[1], enemyInitialFrames[1], 75, 100);
			Enemies.Add(newEnemy);
		}
		#endregion

		#region Update and Draw
		public static void Update(GameTime gameTime)
		{
			for (int x = Enemies.Count - 1; x >= 0; x--)
			{
				Enemies[x].Update(gameTime);
				if (Enemies[x].Destroyed)
				{
					Enemies.RemoveAt(x);
				}
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
