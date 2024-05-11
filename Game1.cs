using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;

namespace MyGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Texture2D playerTexture;//创建一个纹理对象,用于玩家
        Texture2D playerTexture1;//创建一个纹理对象,用于玩家
        Vector2 playerPosition;//创建一个二维向量对象,用于玩家位置
        Vector2 playerPosition1;//创建一个二维向量对象,用于玩家位置
        float playerSpeed;//创建一个浮点数,用于玩家速度
        int numOfBombs = 0;//炸弹数量
        int limitOfBombs = 5;//炸弹限制数量
        float playerSpeed1;//创建一个浮点数,用于玩家速度
        int numOfBombs1 = 0;//炸弹数量
        int limitOfBombs1 = 5;//炸弹限制数量
        Texture2D bombTexture;//创建一个纹理对象,用于炸弹
        bool isHarm1 = false;
        bool isHarm2 = false;
        Player player1;
        Player player2;


        GraphicsDeviceManager graphics;//创建一个图形设备管理对象
        SpriteBatch spriteBatch;//创建一个精灵批处理对象
        private AnimatedSprite animatedSprite;//创建一个动画精灵对象
        private AnimatedSprite animatedSprite1;//创建一个动画精灵对象


        private List<Bomb> bombs = new List<Bomb>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;//设置窗口宽度
            graphics.PreferredBackBufferHeight = 450;//设置窗口高度
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //实例化玩家1
            Texture2D player1Texture = null;
            AnimatedSprite player1Sprite = new AnimatedSprite(player1Texture, 4, 4);
            Vector2 player1inti = new Vector2(2 * graphics.GraphicsDevice.Viewport.Width / 3, graphics.GraphicsDevice.Viewport.Height / 2);
            player1 = new Player(player1inti, 200.0f, player1Sprite);
            //实例化玩家2
            Texture2D player2Texture = null;
            AnimatedSprite player2Sprite = new AnimatedSprite(player2Texture, 4, 4);
            Vector2 player2inti = new Vector2(graphics.GraphicsDevice.Viewport.Width / 3, graphics.GraphicsDevice.Viewport.Height / 2);
            player2 = new Player(player2inti, 200.0f, player2Sprite);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);//创建一个SpriteBatch对象

            playerTexture = Content.Load<Texture2D>("5ewvq");//加载纹理
            animatedSprite = new AnimatedSprite(playerTexture, 4, 4);//创建一个动画精灵对象
            playerTexture1 = Content.Load<Texture2D>("eGfRl");//加载纹理
            animatedSprite1 = new AnimatedSprite(playerTexture1, 4, 4);//创建一个动画精灵对象

            bombTexture = Content.Load<Texture2D>("bomb");//加载纹理

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //查看炸弹是否爆炸
            //查看炸弹是否爆炸
            foreach (Bomb bomb in bombs)
            {
                if (bomb.bombTimeUpdate(gameTime))
                {
                    if (bomb.how == 0)
                        player1.numOfBombs--;
                    else if (bomb.how == 1)
                        player2.numOfBombs--;
                    bombs.Remove(bomb);
                    isHarm1 = (player1.playerPosition.X < bomb.bombPosition.X + bombTexture.Width / 4 && player1.playerPosition.X > bomb.bombPosition.X) ||
                    (player1.playerPosition.Y < bomb.bombPosition.Y + bombTexture.Height && player1.playerPosition.Y > bomb.bombPosition.Y);
                    isHarm2 = (player2.playerPosition.X < bomb.bombPosition.X + bombTexture.Width / 4 && player2.playerPosition.X > bomb.bombPosition.X) ||
                    (player2.playerPosition.Y < bomb.bombPosition.Y + bombTexture.Height && player2.playerPosition.Y > bomb.bombPosition.Y);
                    break;
                }
            }
            //获取键盘状态
            KeyboardState keyboardState = Keyboard.GetState();
            //根据键盘状态更新玩家位置
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                player1.MoveDown(gameTime);
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                player1.MoveLeft(gameTime);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                player1.MoveRight(gameTime);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                player1.MoveUp(gameTime);
            }

            //玩家2操作
            if (keyboardState.IsKeyDown(Keys.S))
            {
                player2.MoveDown(gameTime);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                player2.MoveLeft(gameTime);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                player2.MoveRight(gameTime);
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                player2.MoveUp(gameTime);
            }

            //放炸弹
            if (keyboardState.IsKeyDown(Keys.Space))
            {

                //判断炸弹位置是否重叠
                foreach (Bomb bomb in bombs)
                {
                    // bomb.png 1 * 4
                    if (!(player1.playerPosition.X < bomb.bombPosition.X - bombTexture.Width / 4 ||
                        player1.playerPosition.X > bomb.bombPosition.X + bombTexture.Width / 4 ||
                        player1.playerPosition.Y < bomb.bombPosition.Y - bombTexture.Height ||
                        player1.playerPosition.Y > bomb.bombPosition.Y + bombTexture.Height)
                        )
                    {
                        return;//如果炸弹位置重叠,则不放炸弹
                    }
                }
                if (player1.numOfBombs < player1.limitOfBombs)
                {
                    player1.numOfBombs++;
                    bombs.Add(new Bomb(bombTexture, gameTime, (int)player1.playerPosition.X, (int)player1.playerPosition.Y, 0));
                }
                else
                {
                    //如果炸弹数量超过限制,则不放炸弹
                    return;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Add))
            {

                //判断炸弹位置是否重叠
                foreach (Bomb bomb in bombs)
                {
                    // bomb.png 1 * 4
                    if (!(player2.playerPosition.X < bomb.bombPosition.X - bombTexture.Width / 4 ||
                        player2.playerPosition.X > bomb.bombPosition.X + bombTexture.Width / 4 ||
                        player2.playerPosition.Y < bomb.bombPosition.Y - bombTexture.Height ||
                        player2.playerPosition.Y > bomb.bombPosition.Y + bombTexture.Height)
                        )
                    {
                        return;//如果炸弹位置重叠,则不放炸弹
                    }
                }
                if (player2.numOfBombs < player2.limitOfBombs)
                {
                    player2.numOfBombs++;
                    bombs.Add(new Bomb(bombTexture, gameTime, (int)player2.playerPosition.X, (int)player2.playerPosition.Y, 1));
                }
                else
                {
                    //如果炸弹数量超过限制,则不放炸弹
                    return;
                }
            }
            //边界检测
            player1.playerPosition.X = MathHelper.Clamp(player1.playerPosition.X, 0, GraphicsDevice.Viewport.Width - player1.playerSprite.Texture.Width / 4);//限制玩家位置在0到屏幕宽度-玩家纹理宽度之间
            player1.playerPosition.Y = MathHelper.Clamp(player1.playerPosition.Y, 0, GraphicsDevice.Viewport.Height - player1.playerSprite.Texture.Height / 4);//限制玩家位置在0到屏幕高度-玩家纹理高度之间
            player2.playerPosition.X = MathHelper.Clamp(player2.playerPosition.X, 0, GraphicsDevice.Viewport.Width - player2.playerSprite.Texture.Width / 4);//限制玩家位置在0到屏幕宽度-玩家纹理宽度之间
            player2.playerPosition.Y = MathHelper.Clamp(player2.playerPosition.Y, 0, GraphicsDevice.Viewport.Height - player2.playerSprite.Texture.Height / 4);//限制玩家位置在0到屏幕高度-玩家纹理高度之间
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            animatedSprite.Draw(spriteBatch, playerPosition);
            animatedSprite1.Draw(spriteBatch, playerPosition1);
            //绘制炸弹
            foreach (Bomb bomb in bombs)
            {
                bomb.Draw(spriteBatch);
            }
            //平局
            if (isHarm1 && isHarm2)
            {
                GraphicsDevice.Clear(Color.Green);
            }
            //2获胜
            if (isHarm1 && !isHarm2)
            {
                GraphicsDevice.Clear(Color.White);
            }
            //1获胜
            if (!isHarm1 && isHarm2)
            {
                GraphicsDevice.Clear(Color.Blue);
            }
            base.Draw(gameTime);
        }
    }
}