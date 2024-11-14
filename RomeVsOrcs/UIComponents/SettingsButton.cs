using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomeVsOrcs.UIComponents;
internal class SettingsButton(ContentManager content, Viewport viewport)
{

    private Texture2D texture;

    private Rectangle buttonRectangle;

    public void Load()
    {
        texture = content.Load<Texture2D>("cog");

        // Set button size and position
        int buttonWidth = 25;
        int buttonHeight = 25;
        int screenWidth = viewport.Width;

        // Place the button in the upper right corner
        buttonRectangle = new Rectangle(screenWidth - buttonWidth - 10, 10, buttonWidth, buttonHeight);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, buttonRectangle, Color.White);
    }
}
