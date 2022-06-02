using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.Bodies;
using System.Numerics;

namespace Game
{
    public abstract class Entity
    {
        protected Image sprite;
        protected RotateTransform rotate;
        protected World world;
        protected Body body;
        protected Canvas canvas;
        public static float PPM = 50;//Pixel Per Meter
        protected Vector2 size;
        public Entity(String imageSource, Canvas canvas, World world)
        {
            sprite = new Image();
            sprite.Source = new BitmapImage(new Uri(imageSource, UriKind.Relative));
            rotate = new RotateTransform();
            sprite.RenderTransform = rotate;
            sprite.RenderTransformOrigin = new Point(0.5, 0.5);
            canvas.Children.Add(sprite);
            this.world = world;
            this.canvas = canvas;
        }

        public virtual void update(float dt)
        {
            Canvas.SetLeft(sprite, body.Position.X * PPM);
            Canvas.SetBottom(sprite, body.Position.Y * PPM);
            rotate.Angle = body.GetAngle() * -180f / Math.PI;
        }
    }

}
