using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunMarioRun.Scenes
{
	public class HelpScene : GameScene
	{
		private SpriteBatch sb;
		private Texture2D tex;
		private SpriteFont font;


		public HelpScene(Game game) : base(game)
		{
			Game1 g = game as Game1;
			this.sb = g._spriteBatch;
			tex = g.Content.Load<Texture2D>("Image/helpScene");
			font = g.Content.Load<SpriteFont>("font/RegularFont");
		}
		public override void Draw(GameTime gameTime)
		{
			sb.Begin();

			sb.Draw(tex, Vector2.Zero, Color.White);

			sb.DrawString(font, "Press 'Back' to go back", new Vector2(190, Shared.stage.Y - 80), Color.White);
			sb.DrawString(font, "Press 'Esc' to exit", new Vector2(230, Shared.stage.Y - 40), Color.White);

			sb.End();

			base.Draw(gameTime);
		}
	}
}
