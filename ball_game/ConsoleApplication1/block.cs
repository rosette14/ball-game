using System;
using OpenTK;
using OpenTK.Graphics;
//using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using System.Timers;

namespace ConsoleApp2
{
    class block
    {
        public double init_position;
        public double end_position;
        public double y_position = 1;
        public static int textureBlock;
        public int finish = 0;
        public double i = 0;


        public block(double x)
        {
            init_position = x;
        
            textureBlock = Utilities.LoadTexture(@"images\block.png");

        }
        public void draw(double i,double width, float y_basket, float Xmin_basket)
        {
            //while (i<1.5)
            // {
            end_position = init_position + width;
            GL.PushMatrix();
            GL.BindTexture(TextureTarget.Texture2D, textureBlock);
            GL.Begin(BeginMode.Polygon);
            GL.TexCoord2(0, 0);
            GL.Vertex2(init_position, 1 - i);
            GL.TexCoord2(1, 0);
            GL.Vertex2(end_position, 1 - i);
            GL.TexCoord2(1, 1);
            GL.Vertex2(end_position, 0.95f - i);
            GL.TexCoord2(0, 1);
            GL.Vertex2(init_position, 0.95f - i);
            GL.End();
            GL.BindTexture(TextureTarget.Texture2D, 0);

            //  GL.PopMatrix();
            //   i += 0.05f;
            // }
            y_position = 0.95f - i;
             checkEnd(y_basket, Xmin_basket);
            //  OnRenderFrame();

        }
        public void checkEnd(float y_basket, float Xmin_basket)
        {
            if (y_basket >= y_position && y_basket - 0.04 <= y_position)
            {
                if ((init_position > Xmin_basket && init_position < 0.4f + Xmin_basket)||(end_position > Xmin_basket && end_position < 0.4f + Xmin_basket))
                {
                    finish = 1;
             
                }

            }

        }
    }
}
