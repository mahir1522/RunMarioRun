using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RunMarioRun.Components;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RunMarioRun.Scenes
{
	public class StartScene : GameScene
	{ 
		public MenuComponent Menu {  get; set; }
		private SpriteBatch sb;
		string[] menuItems = { "Play", "Help", "Leaderboard", "Credit", "Quit" };

		private Texture2D symbol;


		private StringBuilder playerNameBuilder;
		private SpriteFont playerNameFont;

		private HashSet<Keys> previousKeys = new HashSet<Keys>();


		public StartScene(Game game) : base(game)
		{
			Game1 g = game as Game1;
			this.sb = g._spriteBatch;

	
			SpriteFont refont = game.Content.Load<SpriteFont>("font/RegularFont");
			SpriteFont hiFont = game.Content.Load<SpriteFont>("font/HilightFont");



			Menu = new MenuComponent(game, this.sb, refont, hiFont, menuItems);
			this.Components.Add(Menu);

			symbol = game.Content.Load<Texture2D>("Image/Run-mario-run");


			playerNameBuilder = new StringBuilder();
			playerNameFont = game.Content.Load<SpriteFont>("font/HilightFont");
		}

		public string GetPlayerName()
		{

			KeyboardState ks = Keyboard.GetState();

			foreach (Keys key in ks.GetPressedKeys())
			{
				if (ks.IsKeyDown(key) && !previousKeys.Contains(key))
				{

					if ((key >= Keys.A && key <= Keys.Z))
					{
						playerNameBuilder.Append(key.ToString());
					}
					else if(key == Keys.Space)
					{
						playerNameBuilder.Append(" ");
					}
					else if (key == Keys.Back && playerNameBuilder.Length > 0)
					{
						playerNameBuilder.Length--;
					}
				}
			}
			previousKeys = new HashSet<Keys>(ks.GetPressedKeys());

			return playerNameBuilder.ToString();
		}

		public override void Update(GameTime gameTime)
		{

			base.Update(gameTime);

			GetPlayerName();
		}



		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
			sb.Begin();

			sb.Draw(symbol, new Rectangle(220, 0,300,200), Color.White);
			sb.DrawString(playerNameFont,"Enter your name here : ", new Vector2(10,215), Color.White);
			sb.DrawString(playerNameFont,playerNameBuilder, new Vector2(380, 215), Color.Wheat);
			sb.End();
		}
	}
}
