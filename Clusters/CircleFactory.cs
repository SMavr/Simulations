using Clusters.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Clusters;
internal class CircleFactory
{
    Random random = new();
    List<Circle> circles = new List<Circle>();

    public void AddCircles(int circleNumber, Color color)
    {
        for (int i = 0; i < circleNumber; i++)
        {
            Circle circle = new Circle(new Vector2(random.Next(0, WindowConstants.Width), random.Next(0, WindowConstants.Width)), color);
            circles.Add(circle);
        }
    }

    public void AddTestCircles()
    {
        Circle circle = new Circle(new Vector2(150, 150), Color.Red);
        Circle circle2 = new Circle(new Vector2(200, 150), Color.Red);
        Circle circle3 = new Circle(new Vector2(150, 200), Color.Red);
        Circle circle4 = new Circle(new Vector2(200, 200), Color.Red);
        circles.AddRange([circle, circle2, circle3, circle4]);
    }

    public void DrawCircles(SpriteBatch spriteBatch)
    {
        foreach (var circle in circles) 
        {   
            circle.Draw(spriteBatch);
        }
    }

    public void Move(float deltaTime)
    {
        for (int i = 0; i < circles.Count; i++)
        {
           for(int j = i + 1; j < circles.Count; j++)
           {
                Vector2 force = Vector2.Zero;
                if (circles[i].IsInVicinity(circles[j]))
                {
                    if (circles[i].IsOverlapping(circles[j]))
                    {
                        circles[i].ResolveOverlap(circles[j]);
                    }
                    else
                    {
                        force = circles[i].CalculateAttractiveForce(circles[j]);
                    }


                    //if (circles[i].IsOverlapping(circles[j]))
                    //{
                    //    //force += circles[i].CalculateRepulsiveForce(circles[j]);

                    //    //circles[i].Velocity = Vector2.Zero;
                    //    //circles[j].Velocity = Vector2.Zero;
                    //    //force = Vector2.Zero;
                    //}
                }

                // Apply equal and opposite force to each circle
                circles[i].Velocity += force * deltaTime;
                circles[j].Velocity -= force * deltaTime;
           }
        }

        // Update each circle's position based on velocity
        foreach (var circle in circles)
        {
            circle.Move(deltaTime);
        }
    }
}
