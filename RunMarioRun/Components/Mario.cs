using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunMarioRun.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RunMarioRun.Components
{
	public class Mario : DrawableGameComponent
	{
		private SpriteBatch sb;
		private Texture2D tex;
		private Vector2 position;
		private Vector2 dimension;
		private List<Rectangle> frames;
		private int frameIndex = -1;
		private Texture2D jumpTex;
		private Texture2D runningTex;
		private ActionScene scene;
		private int delay;
		private int delayCounter;

		private const int ROWS = 1;
		private const int COLS = 3;
		private const int JUMP_FRAME = 0;
		private const float JUMP_HEIGHT = 100f;
		private float GRAVITY = 7f;

		private bool isJumping = false;
		private bool isFalling = true;
		private Vector2 originalPosition;


		public Vector2 Position { get => position; set => position = value; }

		public Mario(Game game, ActionScene scene, SpriteBatch sb,
			Texture2D runningTex, Texture2D jumpTex, Vector2 position, int delay) : base(game)
		{
			this.sb = sb;
			this.runningTex = runningTex;
			this.position = position;
			this.delay = delay;
			this.dimension = new Vector2(runningTex.Width / COLS, runningTex.Height / ROWS);
			this.jumpTex = jumpTex;
			createFrames();

			tex = runningTex;
		}
		public void Jump()
		{
			if (!isJumping)
			{
				isJumping = true;
				isFalling = false;
				frameIndex = JUMP_FRAME;
				originalPosition = position;
				//position.Y = JUMP_HEIGHT;
				tex = jumpTex;
			}
		}

		public void hide()
		{
			this.Enabled = false;
			this.Visible = false;
		}
		public void show()
		{
			this.Enabled = true;
			this.Visible = true;
		}

		private void createFrames()
		{
			frames = new List<Rectangle>();
			for (int i = 0; i < ROWS; i++)
			{
				for (int j = 0; j < COLS; j++)
				{
					int x = (int)(j * dimension.X);
					int y = (int)(i * dimension.Y);

					Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
					frames.Add(r);
				}
			}
		}
		public override void Update(GameTime gameTime)
		{
			delayCounter++;

			if (isJumping)
			{
				if (position.Y >= JUMP_HEIGHT && !isFalling)
				{
					position.Y -= GRAVITY;
				}
				else
				{
					isFalling = true;
					position.Y += GRAVITY;
				}

		

				if (position.Y >= originalPosition.Y)
				{
					isJumping = false;
					position = originalPosition;
					tex = runningTex;
				}

			}

			if (delayCounter > delay && !isJumping)
			{
				frameIndex++;

				if (frameIndex >= COLS)
				{
					frameIndex = 0;
				}
				delayCounter = 0;
			}

				base.Update(gameTime);
			}
		

		public override void Draw(GameTime gameTime)
		{
			sb.Begin();

			if (tex != null)
			{
				if (frameIndex >= 0 && frameIndex < frames.Count)
				{
					sb.Draw(tex, position, frames[frameIndex], Color.White);
				}
				else
				{
					sb.Draw(tex, position, Color.White);
				}
			}

			sb.End();
			base.Draw(gameTime);
		}
	}
}