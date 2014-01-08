using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screen
{
	public static class Effects
	{
		#region Declarations
		static public List<Particle> EffectsList = new List<Particle>();
		static Random rand = new Random();
		static public Texture2D TextureParticule;
		static public Texture2D TextureBoom;
		static public Rectangle ParticleFrame = new Rectangle(0, 288, 2, 2);
		static public List<Rectangle> ExplosionFrames = new List<Rectangle>();
		#endregion

		#region Initialization
		public static void Initialize(Texture2D textureParicule, Rectangle particleFrame, Texture2D textureBoom, Rectangle boomFrame)
		{
			TextureParticule = textureParicule;
			TextureBoom = textureBoom;
			ParticleFrame = particleFrame;
			ExplosionFrames.Clear();
			int nbFrames = 0;
			for (int x = 0; x < 5; x++)
			{
				for (int y = 0; y < 5; y++)
				{
					if (nbFrames == 23)
						continue;

					ExplosionFrames.Add(boomFrame);
					boomFrame.Offset(boomFrame.Width, 0);
					nbFrames++;
				}
				boomFrame.Offset(0, boomFrame.Height);
				boomFrame.Offset(-boomFrame.Width * 5, 0);
			}
		}
		#endregion

		#region Helper Methods
		public static Vector2 randomDirection(float scale)
		{
			Vector2 direction;
			do
			{
				direction = new Vector2(
				rand.Next(0, 100) - 50,
				rand.Next(0, 100) - 50);
			} while (direction.Length() == 0);
			direction.Normalize();
			direction *= scale;

			return direction;
		}
		#endregion

		#region Update and Draw !
		static public void Update(GameTime gameTime)
		{
			for (int x = EffectsList.Count - 1; x >= 0; x--)
			{
				EffectsList[x].Update(gameTime);
				if (EffectsList[x].Expired)
				{
					EffectsList.RemoveAt(x);
				}
			}
		}

		static public void Draw(SpriteBatch spriteBatch)
		{
			foreach (Sprite sprite in EffectsList)
			{
				sprite.Draw(spriteBatch);
			}
		}
		#endregion

		#region Public Methods

		public static void AddExplosion(Vector2 location, Vector2 momentum, int minPointCount, int maxPointCount, int minPieceCount,
									int maxPieceCount, float pieceSpeedScale, int duration, Color initialColor, Color finalColor)
		{
			float explosionMaxSpeed = 30f;
			int pointSpeedMin = (int)pieceSpeedScale * 2;
			int pointSpeedMax = (int)pieceSpeedScale * 3;

			Vector2 pieceLocation = location - new Vector2(ExplosionFrames[0].Width / 2, ExplosionFrames[0].Height / 2);

			int pieces = rand.Next(minPieceCount, maxPieceCount + 1);
			Particle boom = new Particle(pieceLocation, TextureBoom, ExplosionFrames[0], randomDirection(pieceSpeedScale) + momentum,
						Vector2.Zero, explosionMaxSpeed, duration, Color.White, Color.White);
			boom.Animate = true;
			boom.Collidable = false;
			boom.AnimateWhenStopped = true;
			for (int x = 1; x < minPieceCount; x++)
			{
				boom.AddFrame(ExplosionFrames[x]);
			}

			int points = rand.Next(minPointCount, maxPointCount + 1);
			for (int x = 0; x < points; x++)
			{
				EffectsList.Add(new Particle(location, TextureParticule, ParticleFrame, randomDirection((float)rand.Next(pointSpeedMin, pointSpeedMax)) + momentum,
						Vector2.Zero, explosionMaxSpeed, duration, initialColor, finalColor));

			}
		}

		public static void AddExplosion(Vector2 location, Vector2 momentum)
		{
			AddExplosion(location, momentum, 15, 20, 23, 23, 6.0f, 90, new Color(1.0f, 0.3f, 0f, 0.5f), new Color(0.0f, 0.0f, 0f, 0f));
		}

		public static void AddSparksEffect(Vector2 location, Vector2 impactVelocity)
		{
			int particleCount = rand.Next(10, 20);
			for (int x = 0; x < particleCount; x++)
			{
				Particle particle = new Particle(location - (impactVelocity / 60), TextureParticule, ParticleFrame, randomDirection((float)rand.Next(10, 20)),
												Vector2.Zero, 60, 20, Color.Yellow, Color.Orange);
				EffectsList.Add(particle);
			}
		}

		#endregion

	}
}
