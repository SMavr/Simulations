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

    public void DrawCircles(SpriteBatch spriteBatch)
    {
        foreach (var circle in circles) 
        {   
            circle.Draw(spriteBatch);
        }
    }

    public void Move(float deltaTime)
    {
        foreach (var circle in circles)
        {
            circle.Move(deltaTime);
        }
    }
}
