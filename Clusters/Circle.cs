using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Clusters;

internal class Circle
{
    public Vector2 Position { get; set; }
    public Color Color { get; set; }

    public Circle(Vector2 position, Color color)
    {
        Position = position;
        Color = color;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(Position, 2, 100, Color, 5); // 100 segments for smoothness, 2f thickness
    }
}
