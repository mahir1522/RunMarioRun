using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
	public class ActionScene : GameScene
	{
		private SpriteBatch sb;
		Game1 game;
		private Random rnd;
		private Texture2D[] obTexArray;
		private Mario m;
		private Vector2 marioPos;
		private Texture2D marioTex;
		private Base b;
		private Vector2 obPos;
		private Texture2D obTex;
		private bool jumpActive = false;
		private bool gameOver = false;
		private Texture2D gameOverTex;
		private SpriteFont font;
		private SoundEffectInstance gameOverSoundInstance;



		private Vector2 pos;

		private SoundEffect gameOverSound;
		private SoundEffect marioJumpSound;


		private bool showRestartMess;
		private double timer;


		private int score;
		private Players currentPlayer;

		public ActionScene(Game game, string playerName) : base(game)
		{

			currentPlayer = new Players(playerName, 0);
			Reset();


			Game1 g = game as Game1;
			this.sb = g._spriteBatch;

			font = this.Game.Content.Load<SpriteFont>("font/RegularFont");
			gameOverTex =this.Game.Content.Load<Texture2D>("image/Gameover");


			marioTex = this.Game.Content.Load<Texture2D>("image/sprite_mario1");
			Texture2D MarioJump = this.Game.Content.Load<Texture2D>("image/mario_jump");
			marioPos = new Vector2(59, 263);
			m = new Mario(game, this, sb, marioTex, MarioJump, marioPos, 3);
			this.Components.Add(m);

			gameOverSound = this.Game.Content.Load<SoundEffect>("sound/mario_gameover");
			gameOverSoundInstance = gameOverSound.CreateInstance();

			marioJumpSound = this.Game.Content.Load<SoundEffect>("sound/mario_jump");



			Texture2D tex = this.Game.Content.Load<Texture2D>("Image/mario_base");
			Rectangle srcRect = new Rectangle(0,450,tex.Width,tex.Height-450);
			pos = new Vector2(0,Shared.stage.Y - srcRect.Height);
			Vector2 speed = new Vector2(5, 0);




			rnd = new Random();

			obTexArray = new Texture2D[]
			{

				this.Game.Content.Load<Texture2D>("Image/smallest_pillar"),
				this.Game.Content.Load<Texture2D>("Image/tall_pillar"),
				this.Game.Content.Load<Texture2D>("Image/smallest_pillar"),

			};

			obTex = obTexArray[rnd.Next(obTexArray.Length)];
			Rectangle obSrcRect = new Rectangle(0,0,obTex.Width,obTex.Height);
			
			obPos = new Vector2(pos.X + (srcRect.Width - obSrcRect.Width)/2, pos.Y - obSrcRect.Height);
			

			b = new Base(game,this, sb,tex,obTex,pos,obPos,srcRect,speed,obSrcRect);
			this.Components.Add(b);
			
		}

		public override void Update(GameTime gameTime)
		{
			KeyboardState ks = Keyboard.GetState();
			MouseState ms = Mouse.GetState();


			if ((ks.IsKeyDown(Keys.Space) || ms.LeftButton == ButtonState.Pressed)  && !jumpActive && !gameOver )
			{
				m.Jump();
				marioJumpSound.Play();
				jumpActive = true;
			}
			else if (ks.IsKeyUp(Keys.Space))
			{
				jumpActive = false;
			}

			marioPos = new Vector2(m.Position.X - marioTex.Width, m.Position.Y);
			obPos = new Vector2(b.ObPos.X - marioTex.Width, b.ObPos.Y);

			if(!gameOver)
			{
				score = score+1;
			}			

			if (Math.Abs(marioPos.X - obPos.X) < 5 && !gameOver && marioPos.Y > obTex.Width)
			{
				gameOverSoundInstance.Play();
				MediaPlayer.Pause();

				this.gameOver = true;
				currentPlayer.Score = score;
				this.Components.Remove(b);
				this.Components.Remove(m);

			}
			if (gameOver)
			{
				timer += gameTime.ElapsedGameTime.TotalMilliseconds;
				
				if (timer > 500)
				{
					showRestartMess = !showRestartMess;
					timer = 0;
				}
				if (ks.IsKeyDown(Keys.R))
				{
					Game1 g = this.Game as Game1;
					g.SwichToStartScene();
				}
			}

			if (ks.IsKeyDown(Keys.Escape))
			{

				Game1 g = this.Game as Game1;
				g.Exit();

			}

			base.Update(gameTime);
		}


		public void Reset()
		{
			Components.Clear();
			score = 0;
			gameOver = false;
		}

		public override void Draw(GameTime gameTime)
		{
			sb.Begin();

			if(gameOver)
			{
				sb.Draw(gameOverTex, new Rectangle(100,100,600,100), Color.White);
				
				if(currentPlayer.Name == string.Empty)
				{
					currentPlayer.Name = "Unknown";
				}

				sb.DrawString(font, "Score : " + currentPlayer.Score, new Vector2(Shared.stage.X/2- 90, Shared.stage.Y/2 + 20), Color.White);
				sb.DrawString(font, "Player : " + currentPlayer.Name, new Vector2(Shared.stage.X/2- 90, Shared.stage.Y/2 + 60), Color.White);

				if (showRestartMess)
				{
					sb.DrawString(font, "Press 'R' to Restart", new Vector2(230, Shared.stage.Y - 80), Color.White);
					sb.DrawString(font, "Press 'Esc' to exit", new Vector2(230, Shared.stage.Y - 40), Color.Red);
				}
			}
			else
			{
				sb.DrawString(font, "Score: " + score, new Vector2(0, 0), Color.White);
			}
	

			sb.End();

			base.Draw(gameTime);
		}
	}
}
