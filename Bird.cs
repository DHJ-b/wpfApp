using System;
using System.Windows.Controls;
using System.Windows.Input;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Fixtures;
using Box2D.NetStandard.Collision.Shapes;
using System.Numerics;

namespace Game
{
    public class Bird : Entity
    {
        //center is origin
        Vector2 mouseDownPosition;
        Vector2 mouseUpPosition;
        bool wasClicked = false;
        public Bird(Vector2 position, Canvas canvas, World world) : base(".\\Resource\\AngryBird2.png", canvas, world)
        {
            BodyDef bdef = new BodyDef();
            bdef.type = BodyType.Dynamic;
            bdef.position = new Vector2(position.X, position.Y);
            body = world.CreateBody(bdef);
            CircleShape circle = new CircleShape();
            FixtureDef fd = new FixtureDef();
            fd.shape = circle;
            circle.Set(new Vector2(0, 0), 0.5f);
            fd.density = 5f;
            fd.friction = 0.6f;
            fd.restitution = 0.33f;
            body.CreateFixture(fd);
            sprite.Width = (circle.Radius * 2.2) * PPM;
            sprite.Height = (circle.Radius * 2.2) * PPM;
            Canvas.SetLeft(sprite, position.X * PPM);
            Canvas.SetBottom(sprite, position.Y * PPM);
            size = new Vector2(circle.Radius * 2, circle.Radius * 2);
            sprite.MouseDown += OnMouseDown;
            sprite.MouseUp += OnMouseUp;
            canvas.MouseUp += OnMouseUp;
            body.SetEnabled(false);
        }

        private void OnMouseDown(Object sender, MouseButtonEventArgs e)
        {
            if (body.IsEnabled())
                return;
            wasClicked = true;
            var p = e.GetPosition(canvas);
            mouseDownPosition = new Vector2((float)p.X, (float)p.Y);

        }

        private void OnMouseUp(Object sender, MouseButtonEventArgs e)
        {
            if (body.IsEnabled() || !wasClicked)
                return;
            var p = e.GetPosition(canvas);
            mouseUpPosition = new Vector2((float)p.X, (float)p.Y);
            Vector2 diff = mouseUpPosition - mouseDownPosition;
            diff *= -1;
            float maxImpulse = 50f;
            if (diff.Length() > maxImpulse)
            {
                diff /= diff.Length() / maxImpulse;
            }
            diff.Y *= -1;
            body.SetEnabled(true);
            body.ApplyLinearImpulse(diff * 2, new Vector2(0, 0), true);
            wasClicked = false;
        }



        public override void update(float dt)
        {
            Canvas.SetBottom(sprite, (body.Position.Y - size.Y * 1.05f / 2) * PPM);
            Canvas.SetLeft(sprite, (body.Position.X - size.X * 1.05f / 2) * PPM);
            rotate.Angle = body.GetAngle() * -180f / Math.PI;
        }
    }
}
