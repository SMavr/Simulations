using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;

namespace RomeVsOrcs.UIComponents;
public class Dialog
{
    private Texture2D backgroundTexture;
    private Rectangle backgroundRectangle;
    private BitmapFont font;
    private string text;
    private bool isVisible;
    private Vector2 textPosition;

    public bool IsVisible => isVisible;

    public Dialog(Texture2D backgroundTexture, BitmapFont font, string text, Rectangle backgroundRectangle)
    {
        this.backgroundTexture = backgroundTexture;
        this.font = font;
        this.text = text;
        this.backgroundRectangle = backgroundRectangle;
        isVisible = false;

        // Center the text within the dialog box
        var textSize = this.font.MeasureString(this.text);
        textPosition = new Vector2(
            this.backgroundRectangle.X + (this.backgroundRectangle.Width - textSize.Width) / 2,
            this.backgroundRectangle.Y + (this.backgroundRectangle.Height - textSize.Height) / 2
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
