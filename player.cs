using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Player
    {
        public int numOfBombs = 0;//数量
        public int limitOfBombs = 3;//限制数量
        public Vector2 playerPosition;//创建一个二维向量对象,用于玩家位置
        public float playerSpeed;//创建一个浮点数,用于玩家速度
        public AnimatedSprite playerSprite;//创建一个动画精灵对象

        public Player(Vector2 playerPosition, float playerSpeed, AnimatedSprite playerSprite)
        {
            this.playerPosition = playerPosition;
            this.playerSpeed = playerSpeed;
            this.playerSprite = playerSprite;
        }

        public void MoveUp(GameTime gameTime)
        {
            playerSprite.change(3);
            playerPosition.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void MoveDown(GameTime gameTime)
        {
            playerSprite.change(0);
            playerPosition.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void MoveLeft(GameTime gameTime)
        {
            playerSprite.change(1);
            playerPosition.X -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void MoveRight(GameTime gameTime)
        {
            playerSprite.change(2);
            playerPosition.X += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}