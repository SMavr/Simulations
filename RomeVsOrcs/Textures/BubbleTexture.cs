using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RomeVsOrcs.Textures;
internal class BubbleTexture
{
    private Texture2D bubble;
    private float overlayTimer = 0f;
    private bool showBubble = true;
    private readonly ContentManager content;
    private readonly bool enableLoop;

    public BubbleTexture(ContentManager content, bool enableLoop = false)
    {
        this.content = content;
        this.enableLoop = enableLoop;
        if (enableLoop)
        {
            Random random = new Random();
            overlayTimer = random.Next(0, 32);
            showBubble = false;
        }
        else
        {
            showBubble = true;
        }
    }

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
            if (overlayTimer >= 4f)
            {
                showBubble = false;
                overlayTimer = 0f;
            }
        }
        else if(enableLoop)
        {
            overlayTimer += elapsed;

            if (overlayTimer >= 10f)
            {
                showBubble = true;
                overlayTimer = 0f;
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
