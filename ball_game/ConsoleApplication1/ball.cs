using System;
using OpenTK;
using OpenTK.Graphics;
//using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using System.Timers;

namespace ConsoleApp2
{
    class ball 
    {
        public double intit_position;
        public double y_position=1;
        public static int textureBall;
        public int success = 0;
        public double speed;
        
        public ball(double x)
        {
            intit_position = x;
           textureBall = Utilities.LoadTexture(@"images\ball.png");
        }
        public void draw(double i,double speed1,float y_basket,float Xmin_basket)
        {
            speed = speed1;
            GL.PushMatrix();
            GL.BindTexture(TextureTarget.Texture2D, textureBall);
            GL.Begin(BeginMode.Polygon);
            GL.TexCoord2(0, 0);
            GL.Vertex2(intit_position, 1 - i);
            GL.TexCoord2(1, 0);
            GL.Vertex2(intit_position + 0.08, 1 - i);
            GL.TexCoord2(1, 1);
            GL.Vertex2(intit_position + 0.08, 0.92f - i);
            GL.TexCoord2(0, 1);
            GL.Vertex2(intit_position, 0.92f - i);
            GL.End();
            GL.BindTexture(TextureTarget.Texture2D, 0);

            if(success!=1)
            {
                y_position = 0.92f - i;
                checkSuccess(y_basket, Xmin_basket);

            }

        }

        public void checkSuccess( float y_basket, float Xmin_basket)
        {
   
            if (y_basket >= y_position&& y_basket-speed<=y_position)
            {
                if (intit_position > Xmin_basket && intit_position < 0.32f + Xmin_basket)
                {
                    success = 1;
                }
         
            }

        }
    }
}
