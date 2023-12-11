using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RunMarioRun.Components;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RunMarioRun.Scenes
{
	public class LeaderboardScene : GameScene
	{

		private SpriteBatch sb;
		private SpriteFont font;
		private Players players;

		public LeaderboardScene(Game game) : base(game)
		{
			Game1 g = game as Game1;
			this.sb = g._spriteBatch;
			font = g.Content.Load<SpriteFont>("font/HilightFont");
		}
		public override void Draw(GameTime gameTime)
		{
			sb.Begin();

			if (Players.AllPlayers.Count != 0)
			{


				var groupPlayers = Players.AllPlayers
					.GroupBy(p => p.Name)
					.Select(group => new { Name = group.Key, TotalScore = group.Sum(p => p.Score) })
					.OrderByDescending(group => group.TotalScore);

				int spacingY = 30;
				int offsetY = 50;

				int currentIndex = 0;

				foreach (var group in groupPlayers)
				{
					float x = 100;
					float y = offsetY + currentIndex*spacingY;

					sb.DrawString(font, " | Name: " + group.Name + "  ------> Score: " + group.TotalScore + " |", new Vector2(x,y), Color.White);
					currentIndex++;
				}
			}
			else
			{
				{
					sb.DrawString(font, "No player here", new Vector2(200, 200), Color.White);
				}
			}

			sb.DrawString(font, "Press 'Back' to go back", new Vector2(190, Shared.stage.Y - 80), Color.White);
			sb.DrawString(font, "Press 'Esc' to exit", new Vector2(230, Shared.stage.Y - 40), Color.White);

			sb.End();

			base.Draw(gameTime);
		}
	}
}
