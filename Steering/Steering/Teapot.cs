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
using Primitives3D;

namespace Steering
{   
    public class Teapot:Entity
    {
        public Vector3 targetPos = Vector3.Zero;
        private Teapot target = null;
        public Vector3 offset;
        bool added = false;
        public Teapot Target
        {
            get { return target; }
            set { target = value; }
        }
        private Teapot leader = null;

        public Teapot Leader
        {
            get { return leader; }
            set { leader = value; }
        }

        protected SpriteFont spriteFont;
        int points = 6;
        VertexPositionColor[] pointList;
        public float maxSpeed = 5.0f;
        BasicEffect basicEffect;
        bool drawAxis;


        List<Vector3> feelers = new List<Vector3>();
        
        public List<Vector3> Feelers
        {
            get { return feelers; }
            set { feelers = value; }
        }

        public bool DrawAxis
        {
            get { return drawAxis; }
            set { drawAxis = value; }
        }

        public Teapot()
        {
            worldTransform = Matrix.Identity;
            pos = new Vector3(0, 5, 0);
            look = new Vector3(0, 0, -1);
            right = new Vector3(1, 0, 0);
            up = new Vector3(0, 1, 0);
            globalUp = new Vector3(0, 1, 0);
            drawAxis = false;            

            pointList = new VertexPositionColor[points];

            basicEffect = new BasicEffect(XNAGame.Instance().GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            
        }

        public override void LoadContent()
        {
            primitive = new TeapotPrimitive(XNAGame.Instance().GraphicsDevice);
            spriteFont = XNAGame.Instance().Content.Load<SpriteFont>("Verdana");
        }

        public override void UnloadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float) gameTime.ElapsedGameTime.TotalSeconds;
            float speed = 5.0f;
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Up))
            {
                walk(speed * timeDelta);
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                walk(-speed * timeDelta);
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                yaw(speed * timeDelta);
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                yaw(-speed * timeDelta);
            }

            if (keyState.IsKeyDown(Keys.K))
            {
                pos.Y += speed * timeDelta;
            }

            if (keyState.IsKeyDown(Keys.M))
            {
                pos.Y -= speed * timeDelta;
            }
            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            XNAGame.Instance().SpriteBatch.DrawString(spriteFont, "Pos: " + pos.X + " " + pos.Y + " " + pos.Z, new Vector2(10, 10), Color.White);
            XNAGame.Instance().SpriteBatch.DrawString(spriteFont, "Look: " + look.X + " " + look.Y + " " + look.Z, new Vector2(10, 30), Color.White);
            XNAGame.Instance().SpriteBatch.DrawString(spriteFont, "Right: " + right.X + " " + right.Y + " " + right.Z, new Vector2(10, 50), Color.White);
            XNAGame.Instance().SpriteBatch.DrawString(spriteFont, "Up: " + up.X + " " + up.Y + " " + up.Z, new Vector2(10, 70), Color.White);

            

            basicEffect.World = Matrix.Identity;
            basicEffect.View = XNAGame.Instance().Camera.getView();
            basicEffect.Projection = XNAGame.Instance().Camera.getProjection();

            if (drawAxis)
            {
                pointList[0] = new VertexPositionColor(pos, Color.Red);
                pointList[1] = new VertexPositionColor(pos + (look * 15), Color.Red);

                foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    XNAGame.Instance().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, pointList, 0, 1);
                }

                pointList[0] = new VertexPositionColor(pos, Color.Blue);
                pointList[1] = new VertexPositionColor(pos + (right * 15), Color.Blue);
                
                foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    XNAGame.Instance().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, pointList, 0, 1);
                }

                pointList[0] = new VertexPositionColor(pos, Color.White);
                pointList[1] = new VertexPositionColor(pos + (up * 15), Color.White);

                foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    XNAGame.Instance().GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, pointList, 0, 1);
                }
            }

            primitive.Draw(worldTransform, XNAGame.Instance().Camera.getView(), XNAGame.Instance().Camera.getProjection(), colour);
        }
    }
}
