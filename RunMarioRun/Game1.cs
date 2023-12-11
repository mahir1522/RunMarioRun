using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RunMarioRun.Components;
using RunMarioRun.Scenes;

namespace RunMarioRun
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		public SpriteBatch _spriteBatch;

		private Texture2D bgTexture;

		private Song lobbyTheme;
		private Song backgroundMusic;



		private StartScene startScene;
		private ActionScene actionScene;
		private HelpScene helpScene;
		private AboutScene aboutScene;
		private LeaderboardScene leaderboardScene;

		private int selectedIndex = 0;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			Shared.stage = new System.Numerics.Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);


			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			bgTexture = this.Content.Load<Texture2D>("Image/mario");

			lobbyTheme = this.Content.Load<Song>("sound/lobby_theme");
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = 0.5f;
			MediaPlayer.Play(lobbyTheme);


			backgroundMusic = this.Content.Load<Song>("sound/Overworld");
			//MediaPlayer.IsRepeating = true;
			//MediaPlayer.Volume = 0.5f;
			//MediaPlayer.Play(backgroundMusic);




			//for start scene
			startScene = new StartScene(this);
			this.Components.Add(startScene);

			//for action scene
			//actionScene = new ActionScene(this);
			//this.Components.Add(actionScene);
			//actionScene.show();

			//for help scene
			helpScene = new HelpScene(this);
			this.Components.Add(helpScene);

			//for abotu scene
			aboutScene = new AboutScene(this);
			this.Components.Add(aboutScene);

			//for leaderboard scene
			leaderboardScene = new LeaderboardScene(this);
			this.Components.Add(leaderboardScene);
			



			startScene.show();
		}

		private void hideAllScenes()
		{
			foreach(GameComponent item in Components)
			{
				if(item is GameScene)
				{
					GameScene scene = (GameScene)item;
					scene.hide();
				}
			}
		}

		protected override void Update(GameTime gameTime)
		{
			// TODO: Add your update logic here

			//int selectedIndex = 0;

			KeyboardState ks = Keyboard.GetState();

			if(startScene.Enabled)
			{

				selectedIndex = startScene.Menu.SelectedIndex;

				if(selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();

					MediaPlayer.Stop();
					MediaPlayer.Play(backgroundMusic);
					actionScene = new ActionScene(this, startScene.GetPlayerName());
					this.Components.Add(actionScene);
					actionScene.show();

				}
				else if(selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					helpScene.show();
				}
				else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					leaderboardScene.show();
				}
				else if(selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
				{
					hideAllScenes();
					aboutScene.show();
				}
				else if(selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
				{
					Exit();
				}
			}
			else
			{

				if (ks.IsKeyDown(Keys.Escape))
				{
					this.Exit();
				}
				if(ks.IsKeyDown(Keys.Back))
				{

					hideAllScenes();
					startScene.show();
					MediaPlayer.Play(lobbyTheme);
				}
			}

			base.Update(gameTime);
		}


		public void SwichToStartScene()
		{

			hideAllScenes();
			actionScene = new ActionScene(this, startScene.GetPlayerName());
			startScene.show();
		}
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();

			// TODO: Add your drawing code here


			if (startScene.Enabled)
			{

				_spriteBatch.Draw(bgTexture, new Rectangle(500,0,bgTexture.Width, bgTexture.Height-50), Color.White);
			}

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}