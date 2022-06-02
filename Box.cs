using System;
using System.Windows.Controls;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Fixtures;
using Box2D.NetStandard.Collision.Shapes;
using System.Numerics;

namespace Game
{
    public class Box : Entity
    {
        public class BoxData
        {
            public Box box;
            public BoxData(Box box)
            {
                this.box = box;
            }
        }
        private bool crashed = false, destroyed = false;
        private float timer = 0.1f;
        public Box(Vector2 position, Canvas canvas, World world) : base(".\\Resource\\TNTBox.png", canvas, world)
        {
            BodyDef boxDef = new BodyDef();
            boxDef.type = BodyType.Dynamic;
            boxDef.position = position;
            body = world.CreateBody(boxDef);
            PolygonShape box = new PolygonShape();
            float width = 1;
            float height = 1;
            Vector2[] vertices = new Vector2[4];
            vertices[0] = new Vector2(-width / 2, -height / 2); vertices[1] = new Vector2(width / 2, -height / 2); vertices[2] = new Vector2(width / 2, height / 2); vertices[3] = new Vector2(-width / 2, height / 2);
            box.Set(vertices);
            FixtureDef boxd = new FixtureDef();
            boxd.shape = box;
            boxd.density = 0.5f;
            boxd.friction = 0.6f;
            boxd.restitution = 0.1f;
            body.SetUserData(new BoxData(this));
            body.CreateFixture(boxd);
            sprite.Width = (width * PPM);
            sprite.Height = (height * PPM);
            Canvas.SetLeft(canvas, position.X * PPM + width / 2 * PPM);
            Canvas.SetBottom(canvas, position.Y * PPM + height / 2 * PPM);
            size = new Vector2(width, height);
        }
        public override void update(float dt)
        {
            if (destroyed)
                return;
            if (crashed)
            {
                timer -= dt;
                if (timer < 0)
                {
                    MainWindow.boxesBroken += 1;
                    canvas.Children.Remove(sprite);
                    body.SetAwake(false);
                    body.SetEnabled(false);
                    world.DestroyBody(body);
                    destroyed = true;
                }
            }
            Canvas.SetBottom(sprite, (body.Position.Y - size.Y / 2) * PPM);
            Canvas.SetLeft(sprite, (body.Position.X - size.X / 2) * PPM);
            rotate.Angle = body.GetAngle() * -180f / Math.PI;
        }

        public void onCrush()
        {
            crashed = true;
        }
    }
}
