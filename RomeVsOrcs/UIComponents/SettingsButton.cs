using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RomeVsOrcs.UIComponents;
internal class SettingsButton(ContentManager content, GraphicsDevice graphics)
{
    private SettingsDialog_Old dialog;

    private Texture2D texture;

    private Rectangle buttonRectangle;

    public void Load()
    {
        texture = content.Load<Texture2D>("cog");

        // Set button size and position
        int buttonWidth = 25;
        int buttonHeight = 25;
        int screenWidth = graphics.Viewport.Width;

        // Place the button in the upper right corner
        buttonRectangle = new Rectangle(screenWidth - buttonWidth - 10, 10, buttonWidth, buttonHeight);

        dialog = new SettingsDialog_Old(content, graphics);
        dialog.Load();
    }

    public void Update(float elapsedTime)
    {
        if (dialog.IsVisible && Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            var mousePos = Mouse.GetState().Position;

            if (!buttonRectangle.Contains(mousePos))
            {
                dialog.Hide();
            }
        }

        else if (!dialog.IsVisible && Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            var mousePos = Mouse.GetState().Position;

            if (buttonRectangle.Contains(mousePos))
            {
                dialog.Show();
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, buttonRectangle, Color.White);
        dialog.Draw(spriteBatch);
    }
}
