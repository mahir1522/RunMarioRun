using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RunMarioRun.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunMarioRun
{
	public class MenuComponent : DrawableGameComponent
	{
		private SpriteBatch sb;
		private SpriteFont regFont, hiFont;
		private List<string> menuItems;

		public int SelectedIndex { get; set; }

		private Vector2 position;
		private Color regColor = Color.Black;
		private Color hiColor = Color.Red;

		private KeyboardState oldState;

		public MenuComponent(Game game, SpriteBatch sb, SpriteFont regFont,
			SpriteFont hiFont, string[] menus) : base(game)
		{
			this.sb = sb;
			this.regFont = regFont;
			this.hiFont = hiFont;
			menuItems = menus.ToList();
			position = new Vector2((Shared.stage.X / 2) - 350, Shared.stage.Y / 2 + 20);

		}

		public override void Update(GameTime gameTime)
		{
			KeyboardState ks = Keyboard.GetState();

			if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
			{
				SelectedIndex++;
				if (SelectedIndex == menuItems.Count)
				{
					SelectedIndex = 0;
				}
			}
			if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
			{
				SelectedIndex--;
				if (SelectedIndex == -1)
				{
					SelectedIndex = menuItems.Count - 1;
				}
			}
			oldState = ks;

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			Vector2 tempPos = position;

			sb.Begin();

			for (int i = 0; i < menuItems.Count; i++)
			{
				if (i == SelectedIndex)
				{
					sb.DrawString(hiFont, menuItems[i], tempPos, hiColor);
					tempPos.Y += hiFont.LineSpacing;
				}
				else
				{
					sb.DrawString(regFont, menuItems[i], tempPos, regColor);
					tempPos.Y += regFont.LineSpacing;
				}
			}

			base.Draw(gameTime);
			sb.End();
		}
	}
}
