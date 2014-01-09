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
		public float EnemySpeed = 100f;
		public Vector2 currentTargetSquare;
		public int Health = 10;
        public int MaxHealth = 10;
		public bool Destroyed = false;

		public Vector2 gunOffset = new Vector2(8, 8);
		private Queue<Vector2> waypoints = new Queue<Vector2>();
		private Vector2 currentWaypoint = Vector2.Zero;
		private int enemyRadius = 15;
		private Vector2 previousLocation = Vector2.Zero;
		#endregion

		#region Constructor
		public Enemy(Vector2 worldLocation, Texture2D texture, Rectangle initialFrame, int collisionRadius, int health)
		{
			EnemyBase = new Sprite(worldLocation, texture, initialFrame, Vector2.Zero);
			currentWaypoint = worldLocation;
			EnemyBase.CollisionRadius = collisionRadius;
			Health = health;
            MaxHealth = health;

			Rectangle turretFrame = initialFrame;
		}
		#endregion

		public bool WaypointReached()
		{
			if (Vector2.Distance(EnemyBase.ScreenLocation, currentWaypoint) < (float)EnemyBase.Source.Width / 2)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool IsActive()
		{
			if (Destroyed)
			{
				return false;
			}

			if (waypoints.Count > 0)
			{
				return true;
			}

			if (WaypointReached())
			{
				if (MaxHealth == 5)
					return false;
			}

			return true;
		}

		public void AddWaypoint(Vector2 waypoint)
		{
			waypoints.Enqueue(waypoint);
		}

		#region Update and Draw
		public void Update(GameTime gameTime)
		{
			if (Health == 0)
			{
				Destroyed = true;
				Screen.Effects.AddExplosion(EnemyBase.WorldCenter, EnemyBase.Velocity / 30);
			}

			if (MaxHealth > 5 && WaypointReached() && waypoints.Count == 0)
				return;

			if (IsActive())
			{
				Vector2 heading = currentWaypoint - EnemyBase.ScreenLocation;
				if (heading != Vector2.Zero)
				{
					heading.Normalize();
				}
				heading *= EnemySpeed;
				EnemyBase.Velocity = heading;
				previousLocation = EnemyBase.ScreenLocation;
				EnemyBase.Update(gameTime);
				EnemyBase.Rotation = (float)Math.Atan2(EnemyBase.ScreenLocation.Y - previousLocation.Y,	EnemyBase.ScreenLocation.X - previousLocation.X);

				if (WaypointReached())
				{
					if (waypoints.Count > 0)
					{
						currentWaypoint = waypoints.Dequeue();
					}
					else
					{
						if (MaxHealth == 5)
						{
							Battlestar.BattleStar.Damage(Health);
							Screen.Effects.CreateLargeExplosion(EnemyBase.ScreenLocation);
						}
						else
						{
							EnemySpeed = 0;
						}
					}
				}
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
