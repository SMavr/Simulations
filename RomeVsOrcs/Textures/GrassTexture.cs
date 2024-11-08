using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RomeVsOrcs.Textures;
internal class GrassTexture(ContentManager content, Viewport viewport)
{
    private Texture2D grassTexture;

    public void Load()
    {
        grassTexture = content.Load<Texture2D>("grass");
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int windowWidth = viewport.Width;
        int windowHeight = viewport.Height;
        for (int x = 0; x < windowWidth; x += grassTexture.Width)
        {
            for (int y = 0; y < windowHeight; y += grassTexture.Height)
            {
                spriteBatch.Draw(grassTexture, new Vector2(x, y), Color.White);
            }
        }
    }
}
