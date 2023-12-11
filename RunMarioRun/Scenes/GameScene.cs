using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunMarioRun.Scenes
{
	public abstract class GameScene : DrawableGameComponent
	{
		private List<GameComponent> components;
		public List<GameComponent> Components { get => components;set => components = value; }

		public virtual void show()
		{
			this.Enabled = true;
			this.Visible = true;
		}
		public virtual void hide()
		{
			this.Enabled = false;
			this.Visible = false;
		}

		protected GameScene(Game game) : base(game)
		{
			components = new List<GameComponent>();
			hide();
		}
		public override void Update(GameTime gameTime)
		{
			foreach(GameComponent item in components)
			{
				if (item.Enabled)
				{
					item.Update(gameTime);
				}
			}

			base.Update(gameTime);
		}
		public override void Draw(GameTime gameTime)
		{
			foreach(GameComponent item in components)
			{
				if(item is DrawableGameComponent)
				{
					DrawableGameComponent comp = (DrawableGameComponent)item;
					if (comp.Visible)
					{
						comp.Draw(gameTime);
					}
				}
			}

			base.Draw(gameTime);
		}
	}

}
