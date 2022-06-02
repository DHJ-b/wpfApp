using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Fixtures;
using Box2D.NetStandard.Collision.Shapes;
using System.Numerics;

namespace Game
{
    /// World width = 25.6
    /// World height = 14.4

    public partial class MainWindow : Window
    {
        private Canvas canvas;
        public static int boxesBroken = 0;
        private Button pause, exit, restart;
        List<Entity> entities = new List<Entity>();
        World world;
        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        private float timeScale = 0.7f;
        private static int screenWidth = 1280;
        private static int screenHeight = 720;
        private static float worldWidth = (float)(screenWidth) / Entity.PPM;
        private static float worldHeight = (float)(screenHeight) / Entity.PPM;
        private bool onPause = false;
        private StackPanel pausePicture, resumePicture;
        public MainWindow()
        {
            init();
            System.Windows.Media.CompositionTarget.Rendering += UpdateScene;
        }

        public void InitGround()
        {
            BodyDef bd = new BodyDef();
            Body ground = world.CreateBody(bd);
            
            EdgeShape shape = new EdgeShape();

            FixtureDef fd = new FixtureDef();
            fd.shape = shape;
            fd.density = 0.8f;
            fd.friction = 1f;

            shape.SetTwoSided(new Vector2(0,0), new Vector2(worldWidth,0));
            ground.CreateFixture(fd);
            shape.SetTwoSided(new Vector2(worldWidth,0), new Vector2(worldWidth, worldHeight));
            ground.CreateFixture(fd);
            shape.SetTwoSided(new Vector2(worldWidth, worldHeight), new Vector2(0, worldHeight));
            ground.CreateFixture(fd);
            shape.SetTwoSided(new Vector2(0, worldHeight), new Vector2(0,0));
            ground.CreateFixture(fd);
        }

        

        private void init()
        {
            world = new World(new Vector2( 0f, -9.81f));
            world.SetContactListener(new MyContactListener());
            InitGround();
            this.Title = "AB";
            this.ResizeMode = ResizeMode.NoResize;
            this.Width = screenWidth + 60;
            this.Height = screenHeight + 60;
            canvas = new Canvas();
            canvas.Name = "root";
            canvas.Width = screenWidth;
            canvas.Height = screenHeight;
            System.Windows.Media.Color backgr = new System.Windows.Media.Color();
            backgr.R = 65;
            backgr.G = 197;
            backgr.B = 247;
            backgr.A = 255;
            canvas.Background = new SolidColorBrush(backgr);
            Content = canvas;
            pause = new Button();
            exit = new Button();
            restart = new Button();
            pausePicture = makeImageForButton(".\\Resource\\pause-button.png");
            resumePicture = makeImageForButton(".\\Resource\\play-button.png");
            pause.Content = pausePicture;
            exit.Content = makeImageForButton(".\\Resource\\close.png");
            restart.Content = makeImageForButton(".\\Resource\\replay-arrow.png");
            Canvas.SetTop(pause, 10);
            Canvas.SetLeft(pause, 40);
            Canvas.SetTop(restart, 10);
            Canvas.SetLeft(restart, 80);
            Canvas.SetTop(exit, 10);
            Canvas.SetRight(exit, 40);
            canvas.Children.Add(exit    );
            canvas.Children.Add(restart);
            canvas.Children.Add(pause);
            exit.Click += OnExit;
            restart.Click += OnRestart;
            pause.Click += OnPause;
            
            Bird bird = new Bird(new Vector2(3f, 5f), canvas, world);
            entities.Add(bird);
            Box box = new Box(new Vector2(worldWidth * 0.75f, worldHeight * 0.3f + 0.75f), canvas, world);
            Box box2 = new Box(new Vector2(worldWidth * 0.75f, worldHeight * 0.3f + 2.75f), canvas, world);
            Box box3 = new Box(new Vector2(worldWidth * 0.75f - 0.5f, worldHeight * 0.3f + 1.75f), canvas, world);
            Box box4 = new Box(new Vector2(worldWidth * 0.75f + 0.5f, worldHeight * 0.3f + 1.75f), canvas, world);
            entities.Add(box);
            entities.Add(box2);
            entities.Add(box3);
            entities.Add(box4);
            StaticPlatform platform = new StaticPlatform(new Vector2(worldWidth * 0.75f, worldHeight * 0.3f), new Vector2(3f, 0.5f), canvas, world);
            entities.Add(platform);
        }

        private StackPanel makeImageForButton(String source)
        {
            Image img = new Image();
            img.Width = 30;
            img.Height = 30;
            img.Source = new BitmapImage(new Uri(source, UriKind.Relative));
            StackPanel result = new StackPanel();
            result.Orientation = Orientation.Horizontal;
            result.Children.Add(img);

            return result;
        }

        private void UpdateScene(object sender, EventArgs a)
        {
            sw.Stop();
            float dt = 0;
            if (!onPause)
            {
                dt = sw.ElapsedMilliseconds * timeScale / 1000f;
                world.Step(dt, 8, 8);
            }
            sw.Reset();
            sw.Start();
            foreach (var entity in entities)
                entity.update(dt);
        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void OnRestart(object sender, RoutedEventArgs e)
        {
            init();
        }

        private void OnPause(object sender, RoutedEventArgs e)
        {
            onPause = !onPause;
            if (onPause)
            {
                pause.Content = resumePicture;
            }
            else
            {
                pause.Content = pausePicture;
            }

        }
    }

	
}
