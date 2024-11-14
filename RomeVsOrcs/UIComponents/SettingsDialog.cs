using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RomeVsOrcs.UIComponents;
public class SettingsDialog(ContentManager content, GraphicsDevice graphicsDevice)
{
    private Texture2D backgroundTexture;
    private Rectangle backgroundRectangle;
    private SpriteFont font;
    //private string text;
    private bool isVisible;
    //private Vector2 textPosition;

    public bool IsVisible => isVisible;

    public void Load()
    {
        // Load the background texture (a simple solid color texture can be used)
        backgroundTexture = new Texture2D(graphicsDevice, 1, 1);
        backgroundTexture.SetData(new[] { Color.LightGray });

        // Create the dialog instance
        backgroundRectangle = new Rectangle(150, 50, 600, 400);

        this.font = content.Load<SpriteFont>("Stats");
        isVisible = false;
    }

    public void Show()
    {
        isVisible = true;
    }

    public void Hide()
    {
        isVisible = false;
    }

    public void Update(MouseState mouseState, MouseState previousMouseState)
    {
        if (isVisible)
        {
            // Close dialog when left mouse button is clicked
            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                Hide();
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (isVisible)
        {
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White * 0.8f);
            AddText("Hotkeys", 1, spriteBatch);
            AddText("Move forward: W", 2 , spriteBatch);
            AddText("Move back: S", 3, spriteBatch);
            AddText("Move left: A", 4, spriteBatch);
            AddText("Move right: D", 5, spriteBatch);
            AddText("Sprint: Shift", 6, spriteBatch);
            AddText("Full Screen: F11", 7, spriteBatch);
            AddText("Exit Game: ESC", 8, spriteBatch);
        }
    }

    public void AddText(string text, int line, SpriteBatch spriteBatch)
    {
        var textPosition = new Vector2(
            backgroundRectangle.X + 30,
            backgroundRectangle.Y + line * 25
        );
        spriteBatch.DrawString(font, text, textPosition, Color.Black);
    }
}
