using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace EnemyManager
{
	public class Enemy
	{
		#region Declarations
		public Sprite EnemyBase;
		public float EnemySpeed = 60f;
		public Vector2 currentTargetSquare;
		public int Health = 10;
        public int MaxHealth = 10;
		public bool Destroyed = false;
		private int collisionRadius = 16;
		#endregion

		#region Constructor
		public Enemy(Vector2 worldLocation, Texture2D texture, Rectangle initialFrame, int collisionRadius, int health)
		{
			EnemyBase = new Sprite(worldLocation, texture, initialFrame, Vector2.Zero);

			EnemyBase.CollisionRadius = collisionRadius;
			Health = health;
            MaxHealth = health;

			Rectangle turretFrame = initialFrame;
		}
		#endregion

		#region Update and Draw
		public void Update(GameTime gameTime)
		{
			if (!Destroyed)
			{

			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!Destroyed)
			{
				EnemyBase.Draw(spriteBatch);
			}
		}
		#endregion

	}
}
