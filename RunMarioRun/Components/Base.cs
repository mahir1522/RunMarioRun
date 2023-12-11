using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunMarioRun.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RunMarioRun.Components
{
	public class Base : DrawableGameComponent
	{
		private SpriteBatch sb;
		private Texture2D tex;
		private Vector2 position1, position2;
		private Rectangle srcRect;
		private Vector2 speed;
		private ActionScene scene;

		private Texture2D obTex;
		private Vector2 obPos;
		private Rectangle obSrcRect;

		private Texture2D bushTex;
		private Vector2 bushPos;

		public Vector2 ObPos { get => obPos; set => obPos = value; }

		public Base(Game game, ActionScene scene, SpriteBatch sb,
			Texture2D tex, Texture2D obTex,
			Vector2 position, Vector2 obPos,
			Rectangle srcRect, Vector2 speed, Rectangle obSrcRect) : base(game)
		{
			this.sb = sb;
			this.tex = tex;
			this.obTex = obTex;
			this.speed = speed;
			this.position1 = position;
			this.position2 = new Vector2(position.X + srcRect.Width, position.Y);
			this.srcRect = srcRect;
			this.obSrcRect = obSrcRect;
			this.obPos = obPos;


		}

		public override void Update(GameTime gameTime)
		{


			position1 -= speed * 2.0f;
			position2 -= speed * 2.0f;

			obPos -= speed * 2.0f;

			bushPos -= speed * 2.0f;

			if (position1.X + srcRect.Width < 0)
			{
				position1.X = position2.X + srcRect.Width;
				obPos.X = position1.X + (srcRect.Width - srcRect.Width) / 2;

			}
			if (position2.X + srcRect.Width < 0)
			{
				position2.X = position1.X + srcRect.Width;
				obPos.X = position2.X + (srcRect.Width - srcRect.Width) / 2;

			}


			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			sb.Begin();



			sb.Draw(tex,position1,srcRect,Color.White);
			sb.Draw(tex,position2,srcRect,Color.White);


			sb.Draw(obTex, obPos,obSrcRect, Color.White);

			sb.End();

			base.Draw(gameTime);
		}

	}
}