using Microsoft.Xna.Framework;

namespace Clusters.Extensions;

internal static class GraphicsDeviceManagerExtensions
{
    public static void SetDimensions(this GraphicsDeviceManager graphics, int width, int height)
    {
        graphics.PreferredBackBufferWidth = width;
        graphics.PreferredBackBufferHeight = height;
        graphics.ApplyChanges();
    }
}
