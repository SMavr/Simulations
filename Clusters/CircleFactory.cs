﻿using Clusters.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace Clusters;
internal class CircleFactory
{
    Random random = new();
    List<Circle> circles = new List<Circle>();

    public void AddCircles(int circleNumber, Color color, int team, List<ForceParagon> forceParagons = null)
    {
        for (int i = 0; i < circleNumber; i++)
        {
            Circle circle = new Circle(new Vector2(random.Next(0, WindowConstants.Width), random.Next(0, WindowConstants.Width)), color, team);
            circle.AddForceParagons(forceParagons);
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
                Vector2 forceA = Vector2.Zero;
                Vector2 forceB = Vector2.Zero;
                if (circles[i].IsInVicinity(circles[j]))
                {
                    if (circles[i].IsOverlapping(circles[j]))
                    {
                        circles[i].ResolveOverlap(circles[j]);
                    }
                    else
                    {
                        forceA = circles[i].CalculateAttractiveForce(circles[j]);
                        forceB = circles[j].CalculateAttractiveForce(circles[i]);
                    }
                }

                // Apply force
                circles[i].Velocity += forceA * deltaTime;
                circles[j].Velocity += forceB * deltaTime;
            }

            circles[i].Move(deltaTime);
        }
    }
}
