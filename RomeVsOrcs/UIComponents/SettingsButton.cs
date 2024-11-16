using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D;
using Myra.Graphics2D.UI;

namespace RomeVsOrcs.UIComponents;
internal class SettingsButton(ContentManager content, GraphicsDevice graphics, Desktop desktop)
{
    private Texture2D texture;

    private Rectangle buttonRectangle;

    Dialog dialog;

    public void Load()
    {
        texture = content.Load<Texture2D>("cog");

        // Set button size and position
        int buttonWidth = 25;
        int buttonHeight = 25;
        int screenWidth = graphics.Viewport.Width;

        // Place the button in the upper right corner
        buttonRectangle = new Rectangle(screenWidth - buttonWidth - 10, 10, buttonWidth, buttonHeight);

        LoadDialog();
    }

    public void Update(float elapsedTime)
    {
        if (!dialog.IsPlaced && Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            var mousePos = Mouse.GetState().Position;

            if (buttonRectangle.Contains(mousePos))
            {
              
                dialog.ShowModal(desktop);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, buttonRectangle, Color.White);
    }

    private void LoadDialog()
    {
        dialog = new Dialog
        {
            Title = "Hot Keys",
        };

        VerticalStackPanel stackPanel = new VerticalStackPanel();
        AddLabel("Move forward: W");
        AddLabel("Move back: S");
        AddLabel("Move left: A");
        AddLabel("Move right: D");
        AddLabel("Sprint: Shift");
        AddLabel("Full Screen: F11");
        AddLabel("Exit Game: ESC");

        dialog.Content = stackPanel;

        dialog.ButtonOk.Click += (s, e) =>
        {
            dialog.Close();
        };

        void AddLabel(string text)
        {
            var label = new Label
            {
                Text = text
            };
            stackPanel.Widgets.Add(label);
        }
    }
}
