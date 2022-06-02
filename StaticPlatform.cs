using System.Windows.Controls;
using System.Windows.Media;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Fixtures;
using Box2D.NetStandard.Collision.Shapes;
using System.Numerics;

namespace Game
{
    public class StaticPlatform : Entity
    {
        public StaticPlatform(Vector2 position, Vector2 size, Canvas canvas, World world) : base((size.X / size.Y <= 18.5f ? ".\\Resource\\wide2.png" : ".\\Resource\\wide1.png"),
                                                                                                  canvas, world)
        {
            BodyDef boxDef = new BodyDef();
            boxDef.type = BodyType.Static;
            boxDef.position = position;
            body = world.CreateBody(boxDef);
            PolygonShape box = new PolygonShape();
            float width = size.X;
            float height = size.Y;
            Vector2[] vertices = new Vector2[4];
            vertices[0] = new Vector2(-width / 2, -height / 2); vertices[1] = new Vector2(width / 2, -height / 2); vertices[2] = new Vector2(width / 2, height / 2); vertices[3] = new Vector2(-width / 2, height / 2);
            box.Set(vertices);
            FixtureDef boxd = new FixtureDef();
            boxd.shape = box;
            boxd.friction = 0.6f;
            boxd.restitution = 0.1f;

            body.CreateFixture(boxd);
            sprite.Width = (width * PPM);
            sprite.Height = (height * PPM);
            sprite.Stretch = Stretch.Fill;
            Canvas.SetLeft(sprite, position.X * PPM - width * PPM / 2);
            Canvas.SetBottom(sprite, position.Y * PPM - height * PPM / 2);
            this.size = new Vector2(width, height);
        }

        public override void update(float dt)
        {

        }
    }
}
