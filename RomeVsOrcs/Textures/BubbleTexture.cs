using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RomeVsOrcs.Textures;
internal class BubbleTexture(ContentManager content)
{
    private Texture2D bubble;
    private float overlayTimer = 0f;
    private bool showBubble = true;

    public void Load(string asset)
    {
        bubble = content.Load<Texture2D>(asset);
    }

    public void Update(float elapsed)
    {
        if (showBubble)
        {
            overlayTimer += elapsed;

            // Hide overlay after the specified duration
            if (overlayTimer >= 5f)
            {
                showBubble = false;
            }
        }
    }

    public void DrawFrame(SpriteBatch spriteBatch, Vector2 characterPosition, Vector2 adjust = default)
    {
        if (showBubble)
        {
            spriteBatch.Draw(bubble, characterPosition + new Vector2(-32, -32) + adjust, null,
                Color.White, 0, Vector2.Zero, 0.3f, SpriteEffects.None, 1);
        }
    }
}
