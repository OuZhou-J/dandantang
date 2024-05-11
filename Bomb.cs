using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Bomb
    {
        public Vector2 bombPosition { get; set; }//创建一个二维向量对象,用于炸弹位置
        public double bombTime { get; set; }//创建一个浮点数,用于记录创建炸弹时间
        public int bombSpectrum { get; set; }//创建一个整数,用于炸弹范围
        public bool isVisible { get; set; }//创建一个布尔值,用于判断炸弹是否可
        public Texture2D bombTexture { get; set; }//创建一个纹理对象,用于炸弹纹理
        public int bombRows { get; set; }//创建一个整数,用于炸弹png行数
        public int bombColumns { get; set; }//创建一个整数,用于炸弹png列数
        private int bombcurrentFrame { get; set; }//创建一个整数,用于炸弹当前帧
        private int bombtotalFrames;
        public int how;

        public AnimatedSprite bombSprite;//创建一个动画精灵对象


        public Bomb(Texture2D texture, GameTime gameTime, int x, int y,int how)
        {
            bombTexture = texture;//炸弹纹理
            bombSprite = new AnimatedSprite(bombTexture, 1, 4);//创建一个动画精灵对象
            bombPosition = new Vector2(x, y);//炸弹位置
            bombTime = gameTime.TotalGameTime.TotalSeconds;//炸弹时间

            bombRows = 1;   //炸弹png行数
            bombColumns = 4;//炸弹png列数
            bombtotalFrames = bombRows * bombColumns;//炸弹总帧数
            bombcurrentFrame = 0;
            this.how=how;
        }
        public bool bombTimeUpdate(GameTime gameTime)
        {
            bombSprite.bombUpdate(gameTime.TotalGameTime.TotalSeconds,bombTime);
            if (gameTime.TotalGameTime.TotalSeconds > bombTime + 4)//如果当前时间大于炸弹时间加3秒
            {

                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bombSprite.Draw(spriteBatch, bombPosition);
        }
    }
}