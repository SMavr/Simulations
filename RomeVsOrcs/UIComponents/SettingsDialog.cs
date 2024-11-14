using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;

//using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Content;

namespace RomeVsOrcs.UIComponents;
public class SettingsDialog(ContentManager content, GraphicsDevice graphicsDevice)
{
    private Texture2D backgroundTexture;
    private Rectangle backgroundRectangle;
    private SpriteFont font;
    private string text;
    private bool isVisible;
    private Vector2 textPosition;

    public bool IsVisible => isVisible;

    public void Load()
    {
        // Load the background texture (a simple solid color texture can be used)
        backgroundTexture = new Texture2D(graphicsDevice, 1, 1);
        backgroundTexture.SetData(new[] { Color.Gray });

        // Create the dialog instance
        backgroundRectangle = new Rectangle(100, 100, 400, 200);

        this.font = content.Load<SpriteFont>("Stats");
        this.text = "Test";
        isVisible = false;

        // Center the text within the dialog box
        var textSize = this.font.MeasureString(this.text);
        textPosition = new Vector2(
            backgroundRectangle.X + (backgroundRectangle.Width - 10) / 2,
            backgroundRectangle.Y + (backgroundRectangle.Height - 10) / 2
        );
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
            spriteBatch.DrawString(font, text, textPosition, Color.Black);
        }
    }
}
