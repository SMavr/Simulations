﻿using Clusters.Constants;
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
                //if(j != i + 1 && circles[i].IsInVicinity(circles[j]))
                //{
                    Vector2 force = circles[i].CalculateAttractiveForce(circles[j]);

                    // Apply equal and opposite force to each circle
                    circles[i].Velocity += force * deltaTime;
                    circles[j].Velocity -= force * deltaTime;
                //}
           }
        }

        // Update each circle's position based on velocity
        foreach (var circle in circles)
        {
            circle.Position += circle.Velocity * deltaTime;
        }
    }
}
