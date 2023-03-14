using System;
using OpenTK;
using OpenTK.Graphics;
//using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using System.Timers;

namespace ConsoleApp2
{
    class Program : GameWindow
    {
        OpenTK.Graphics.TextPrinter text = new OpenTK.Graphics.TextPrinter();
        public int missed_ball=0;
        public int enter = 0;
        public double speed=0.05f;
        public int caught_balls = 0;
        public int theEnd = 0;
        public int pause = 0;
        public int win = 0;
        public float dx=0;
        public float dy = 0;
        public static string TITLE = " Catch the ball ";
        public static int WIDTH = 600;
        public static int HEIGHT = 600;
        public float i = 0;
        public double []b = new double[100];
        public double[] c_block = new double[50];///for block

        public float y_basket = 0;
        public float Xmin_basket = 0;
        public double block_width = 0.1f;
        public int score = 0;
       // public static float Xmax_basket = 0;
        public int j = 0;
        public static int textureSky;
        public static int textureBasket;
        public static int textureNumber;
        public static int texturePause;
        public static int textureOperate;

        public ball[] a = new ball[100];
        public  block[] Block = new block[50];

        public System.Timers.Timer Timer_ball = new System.Timers.Timer(4500);
        public static System.Timers.Timer Timer_ball1 = new System.Timers.Timer(2500);
        public static System.Timers.Timer Timer_block = new System.Timers.Timer(10000);

        Random element = new Random();
        public double rand_y=0;///for ball
        public double rand_x_block = 0;///for block

        public int after_while_ball = 1;
        public int after_while_block = 0;

        public Program() : base(WIDTH, HEIGHT, GraphicsMode.Default, TITLE) { }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           
            GL.Enable(EnableCap.Texture2D);
          //  GL.ClearColor(Color4.CadetBlue);
            //////////////loading textures///////////////
            textureSky = Utilities.LoadTexture(@"images\7.jpg");
            textureBasket = Utilities.LoadTexture(@"images\basket.png");
            textureNumber= Utilities.LoadTexture(@"images\number.png");
            texturePause = Utilities.LoadTexture(@"images\pause.png");
            textureOperate = Utilities.LoadTexture(@"images\op.png");

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            for (int k = 0; k < 100; k++)
            {
                if(rand_y<0.92f)
                {
                    a[k] = new ball(rand_y);
                    rand_y = (element.NextDouble() * 2) - 1;
                    b[k] = 0;
                }
                else
                {
                    rand_y = (element.NextDouble() * 2) - 1;
                    k--;
                }

            }
   
            SetTimer_ball();

            for (int k = 0; k < 50; k++)
            {
               // ((rand_x_block > a[k].intit_position + 0.1f) || (rand_x_block + 0.22f < a[k].intit_position))
                if ( rand_x_block<0.8f)
                {
                    Block[k] = new block(rand_x_block);
                    rand_x_block = (element.NextDouble() * 2) - 1;
                    c_block[k] = 0;
                }
                else
                {
                    rand_x_block = (element.NextDouble() * 2) - 1;
                    k--;
                }
            }
            SetTimer_block();

            




        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if(theEnd!=1&&pause!=1&&win!=1)
            {
                if (caught_balls == 5)
                {
                    block_width = 0.15f;
                }
                else
                    if (caught_balls == 10&&enter==0)
                {
                    speed = 0.1f;
                    enter = 1;
                    Timer_ball.Dispose();
                    Timer_ball1.Elapsed += OnTimedEvent_ball;
                    Timer_ball1.AutoReset = true;
                    Timer_ball1.Enabled = true;
                }
                else
               if (caught_balls == 15)
                {
                    //Timer_ball.Interval = 4000;
                    block_width = 0.17f;
                }

                if (Keyboard[Key.Left])
                {
                    if (dx > -0.74f)
                        dx += -0.1f;

                }
                else if (Keyboard[Key.Right])
                {
                    if (dx < 0.74f)
                        dx += 0.1f;

                }
                else if (Keyboard[Key.Up])
                {
                    if (dy < 0.2f)
                        dy += 0.05f;

                }
                else if (Keyboard[Key.Down])
                {
                    if (dy > -0.05f)
                        dy -= 0.05f;

                }
                for (int k = 0; k < after_while_ball; k++)
                {
                    //   b[k] += 0.05f;
                          b[k] += speed;
                }

                for (int k = 0; k < after_while_block; k++)
                {
                    c_block[k] += 0.04f;
                }
               
           

            }
            if(Keyboard[Key.Space]&&theEnd==1)
            {
                Program myGame = new Program();
                myGame.Run(5, 5);
            }
            if(Keyboard[Key.Space]&&pause==1)
            {
                pause = 0;
                GL.Color3(Color.White);

            }
            else
            if(Keyboard[Key.Space]&&theEnd==0)
            {
                pause = 1;
            }

              }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);



         
            GL.LoadIdentity();

            // for sky background
        
            GL.BindTexture(TextureTarget.Texture2D, textureSky);
            GL.LoadIdentity();
            GL.Begin(BeginMode.Polygon);
            GL.TexCoord2(0, 0);
            GL.Vertex2(-1, 1);
            GL.TexCoord2(1, 0);
            GL.Vertex2(1, 1);
            GL.TexCoord2(1, 1);
            GL.Vertex2(1, -1);
            GL.TexCoord2(0, 1);
            GL.Vertex2(-1, -1);
            GL.End();
            GL.BindTexture(TextureTarget.Texture2D, 0);
            

            ///////
            GL.PushMatrix();
            GL.Translate(dx, dy, 0);
            GL.BindTexture(TextureTarget.Texture2D, textureBasket);
            GL.Begin(BeginMode.Polygon);
            GL.TexCoord2(0, 0);
            GL.Vertex2(-0.2, -0.5);
            GL.TexCoord2(1, 0);
            GL.Vertex2(0.2, -0.5);
            GL.TexCoord2(1, 1);
            GL.Vertex2(0.2, -0.9);
            GL.TexCoord2(0, 1);
            GL.Vertex2(-0.2, -0.9);
            GL.End();
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.PopMatrix();
            //////////operate/////////
            if(pause!=1)
            {
                GL.PushMatrix();
                GL.BindTexture(TextureTarget.Texture2D, textureOperate);
                GL.Begin(BeginMode.Polygon);
                GL.TexCoord2(0, 0);
                GL.Vertex2(-1, 1);
                GL.TexCoord2(1, 0);
                GL.Vertex2(-0.9, 1);
                GL.TexCoord2(1, 1);
                GL.Vertex2(-0.9, 0.9);
                GL.TexCoord2(0, 1);
                GL.Vertex2(-1, 0.9);
                GL.End();
                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.PopMatrix();
            }
            /////////////////
           
            for (int k = 0; k < after_while_ball; k++)
                {
                    y_basket = -0.5f + dy;
                    Xmin_basket = -0.2f + dx;
                    if (a[k].success != 1)
                    {
                        a[k].draw(b[k],speed, y_basket, Xmin_basket);
                    }
                }

                for (int k = 0; k < after_while_block; k++)
                {
                    y_basket = -0.5f + dy;
                    Xmin_basket = -0.2f + dx;
                    Block[k].draw(c_block[k],block_width, y_basket, Xmin_basket);
                }

               for (int k = 0; k < after_while_block; k++)
                {
                   if (Block[k].finish == 1)
                      theEnd = 1;
                }
              for (int k = 0; k < after_while_ball; k++)
                {
                    score += a[k].success;
                }
            //missed_ball = after_while_ball - score -1;
                for (int k = 0; k < after_while_ball-2; k++)
                {
                if (a[k].success == 0)
                    missed_ball++;
                }
                DisplayMissedBall(missed_ball);
            // check_missed_ball(missed_ball);
               if (missed_ball==5||theEnd==1)
               {
                   endGame();
               }
                DisplayScore(score);
                caught_balls = score;
                levelUp(caught_balls);
            score = 0;
                missed_ball = 0;

            if(pause==1)
            {
                GL.PushMatrix();
                GL.BindTexture(TextureTarget.Texture2D, texturePause);
                GL.Begin(BeginMode.Polygon);
                GL.TexCoord2(0, 0);
                GL.Vertex2(-1, 1);
                GL.TexCoord2(1, 0);
                GL.Vertex2(-0.9, 1);
                GL.TexCoord2(1, 1);
                GL.Vertex2(-0.9, 0.9);
                GL.TexCoord2(0, 1);
                GL.Vertex2(-1, 0.9);
                GL.End();
                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.PopMatrix();
                GL.Color3(Color.MistyRose);
                text.Begin();
                Font font = new Font(FontFamily.GenericSerif, 25.0f);
                text.Print("Continue ", font, Color.Black, new RectangleF(200, 300, 200, 80), OpenTK.Graphics.TextPrinterOptions.Default, OpenTK.Graphics.TextAlignment.Center);
                text.End();
            }

        SwapBuffers();
        }
        public void DisplayScore(int score)
        {
            text.Begin();
            Font font = new Font(FontFamily.GenericSerif, 18.0f);
            text.Print("score: " + score,font, Color.Tan, new RectangleF(200, 0, 200, 80), OpenTK.Graphics.TextPrinterOptions.Default, OpenTK.Graphics.TextAlignment.Center);
            text.End();

        }
        public void DisplayMissedBall(int missed_ball)
        {
            text.Begin();
            Font font = new Font(FontFamily.GenericSerif, 18.0f);
            text.Print("missed balls: " + missed_ball, font, Color.Tan, new RectangleF(280, 0, 280, 80), OpenTK.Graphics.TextPrinterOptions.Default, OpenTK.Graphics.TextAlignment.Far);
            text.End();

        }

        public void endGame()
        {
            theEnd = 1;
            GL.LoadIdentity();
            GL.Begin(BeginMode.Lines);
            GL.Color3(Color.Black);
            GL.Vertex2(-0.23, -0.03);
            GL.Vertex2(0.2, -0.03);
            GL.Vertex2(0.2, -0.2);
            GL.Vertex2(-0.23, -0.2);
            GL.Color3(Color.CadetBlue);
            GL.End();
            text.Begin();
            Font font = new Font(FontFamily.GenericSerif, 20.0f);
            text.Print("GAME OVER ", font, Color.Black, new RectangleF(200, 200, 200, 80), OpenTK.Graphics.TextPrinterOptions.Default, OpenTK.Graphics.TextAlignment.Center);
            text.Print("your score is " + score, font, Color.Black, new RectangleF(200, 250, 200, 80), OpenTK.Graphics.TextPrinterOptions.Default, OpenTK.Graphics.TextAlignment.Center);
            text.Print("play again ", font, Color.Black, new RectangleF(200, 320, 200, 80), OpenTK.Graphics.TextPrinterOptions.Default, OpenTK.Graphics.TextAlignment.Center);
            text.End();
        }
        public  void SetTimer_ball()
        {

            Timer_ball.Elapsed += OnTimedEvent_ball;
            Timer_ball.AutoReset = true;
            Timer_ball.Enabled = true;
        }

        public  void OnTimedEvent_ball(Object source, ElapsedEventArgs e)
        {
           if(theEnd!=1&&pause!=1)
            {
                after_while_ball++;
            }
        }


        public  void SetTimer_block()
        {

            Timer_block.Elapsed += OnTimedEvent_block;
            Timer_block.AutoReset = true;
            Timer_block.Enabled = true;
        }

        public  void OnTimedEvent_block(Object source, ElapsedEventArgs e)
        {
            if(theEnd!=1&&pause!=1)
            {
                after_while_block++;

            }
        }

        public void levelUp(int caught_balls)
        {
            if (caught_balls%5==0&&theEnd!=1&&caught_balls<=15)
            {
                int level = (caught_balls / 5)+1;
                text.Begin();
                Font font = new Font(FontFamily.GenericMonospace, 27.0f);
                text.Print("level "+level, font, Color.SteelBlue, new RectangleF(200, 300, 200, 80), OpenTK.Graphics.TextPrinterOptions.Default, OpenTK.Graphics.TextAlignment.Center);
                text.End();
            }
        }

        static void Main(string[] args)
        {
            Program myGameWin = new Program();
            myGameWin.Run(5, 5);

        }
    }
}
