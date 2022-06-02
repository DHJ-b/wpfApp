using System;
using System.Numerics;
using Box2D.NetStandard.Collision;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Contacts;
using Box2D.NetStandard.Dynamics.Fixtures;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.World.Callbacks;


namespace Game
{
    public class MyContactListener : ContactListener
    {
        private Vector2 preIm1, preIm2, postIm1, postIm2;
        public override void BeginContact(in Contact contact)
        {

        }

        public override void EndContact(in Contact contact)
        {

        }


        public override void PostSolve(in Contact contact, in ContactImpulse impulse)
        {

            Fixture a = contact.GetFixtureA();
            Fixture b = contact.GetFixtureB();

            if (a == null || b == null)
            {
                return;
            }
            Body aa = a.Body;
            Body bb = b.Body;
            float crashimp = 4f;


            Body box1 = null;
            Body box2 = null;
            Body staticbody = null;
            if (aa.GetUserData<Box.BoxData>() != null && bb.Type() == BodyType.Static)
            {
                box1 = aa;
                staticbody = bb;
            }
            if (bb.GetUserData<Box.BoxData>() != null && aa.Type() == BodyType.Static)
            {
                box1 = bb;
                staticbody = aa;
            }
            if (aa.GetUserData<Box.BoxData>() != null &&
                bb.GetUserData<Box.BoxData>() != null)
            {
                box1 = aa;
                box2 = bb;
            }

            if (box1 != null && box2 == null)
            {
                postIm1 = new Vector2(box1.GetLinearVelocity().X * box1.GetMass(), box1.GetLinearVelocity().Y * box1.GetMass());
                if ((postIm1 - preIm1).Length() >= crashimp)
                {
                    box1.GetUserData<Box.BoxData>().box.onCrush();
                }
            }

            if (box2 != null)
            {
                postIm1 = new Vector2(box1.GetLinearVelocity().X * box1.GetMass(), box1.GetLinearVelocity().Y * box1.GetMass());
                postIm2 = new Vector2(box2.GetLinearVelocity().X * box2.GetMass(), box2.GetLinearVelocity().Y * box2.GetMass());
                if ((postIm1 - preIm1).Length() >= crashimp)
                {
                    box1.GetUserData<Box.BoxData>().box.onCrush();
                }
                if ((postIm2 - preIm2).Length() >= crashimp)
                {
                    box2.GetUserData<Box.BoxData>().box.onCrush();
                }

            }

        }

        public override void PreSolve(in Contact contact, in Manifold oldManifold)
        {
            Fixture a = contact.GetFixtureA();
            Fixture b = contact.GetFixtureB();

            if (a == null || b == null)
            {
                return;
            }
            Body aa = a.Body;
            Body bb = b.Body;
            float crashimp = 10;


            Body box1 = null;
            Body box2 = null;
            Body staticbody = null;
            if (aa.GetUserData<Box.BoxData>() != null && bb.Type() == BodyType.Static)
            {
                box1 = aa;
                staticbody = bb;
            }
            if (bb.GetUserData<Box.BoxData>() != null && aa.Type() == BodyType.Static)
            {
                box1 = bb;
                staticbody = aa;
            }
            if (aa.GetUserData<Box.BoxData>() != null &&
                bb.GetUserData<Box.BoxData>() != null)
            {
                box1 = aa;
                box2 = bb;
            }

            if (box1 != null && box2 == null)
            {
                preIm1 = new Vector2(box1.GetLinearVelocity().X * box1.GetMass(), box1.GetLinearVelocity().Y * box1.GetMass());
            }

            if (box2 != null)
            {
                preIm1 = new Vector2(box1.GetLinearVelocity().X * box1.GetMass(), box1.GetLinearVelocity().Y * box1.GetMass());
                preIm2 = new Vector2(box2.GetLinearVelocity().X * box2.GetMass(), box2.GetLinearVelocity().Y * box2.GetMass());

            }

        }
    }
}
