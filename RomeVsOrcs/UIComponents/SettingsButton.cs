using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;

namespace RomeVsOrcs.UIComponents;
internal class SettingsButton(ContentManager content, GraphicsDeviceManager graphics, Desktop desktop)
{

    Dialog dialog;

    public void Load()
    {
        LoadButton();
        LoadDialog();
    }

    //public void Update(float elapsedTime)
    //{
    //    if (!dialog.IsPlaced && Mouse.GetState().LeftButton == ButtonState.Pressed)
    //    {
    //        var mousePos = Mouse.GetState().Position;

    //        //if (buttonRectangle.Contains(mousePos))
    //        //{
              
    //        //    dialog.ShowModal(desktop);
    //        //}
    //    }
    //}

    //public void Draw(SpriteBatch spriteBatch)
    //{
    //    spriteBatch.Draw(texture, buttonRectangle, Color.White);
    //}

    private void LoadDialog()
    {
        dialog = new Dialog
        {
            Title = "Hot Keys",
            Padding = new Thickness(20)
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

    private void LoadButton()
    {
        // Create the button and set its properties
        Button button = new Button
        {
            Content = new Image
            {
                Renderable = new TextureRegion(content.Load<Texture2D>("cog"))
            },
            Width = 40,
            Height = 40,
            Background = null,
            PressedBackground = null,
        };

        // Set the button position to the upper right corner
        button.Left = graphics.PreferredBackBufferWidth - button.Width.Value - 10;
        button.Top = 10;

        // Add an event handler for the button click
        button.Click += (s, e) =>
        {
            dialog.ShowModal(desktop);
        };

        // Add the button to the desktop
        desktop.Widgets.Add(button);
    }
}
