using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Steering
{
    class EnemyTeapot:Teapot
    {
        Quaternion quat;
        Quaternion fromQuat;
        Quaternion toQuat;

        float t;
        bool slerping = false;

        public EnemyTeapot()
            : base()
        {
            quat = Quaternion.Identity;
            fromQuat = Quaternion.Identity;
            toQuat = Quaternion.Identity;
            t = 0.0f;
            colour = Color.Blue;
        }

        public override void Update(GameTime gameTime)
        {
            Vector3 toEnemy = XNAGame.Instance().Dalek.pos - pos;
            toEnemy.Normalize();
            float timeDelta = (float) gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Space))
            {
                slerping = true;
            }

            if (slerping)
            {
               
            }

            worldTransform = Matrix.CreateScale(15) * Matrix.CreateFromQuaternion(quat) * Matrix.CreateTranslation(pos);
            
        }

        private void setYaw(float p)
        {
            look.X = -(float) Math.Sin(p);
            look.Y = 0.0f;
            look.Z = - (float) Math.Cos(p);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            XNAGame.Instance().SpriteBatch.DrawString(spriteFont, "Yaw: " + getYaw(), new Vector2(10, 100), Color.White);
            XNAGame.Instance().SpriteBatch.DrawString(spriteFont, "Look: " + look.X + " " + look.Y + " " + look.Z, new Vector2(10, 120), Color.White);
            
            //XNAGame.Instance().SpriteBatch.DrawString(spriteFont, "Yaw: " + getYaw(), new Vector2(10, 100), Color.White);
        }
    }
}
