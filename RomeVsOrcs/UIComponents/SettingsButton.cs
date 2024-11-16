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
        //dialog.Draw(spriteBatch);
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

        //var label1 = new Label
        //{
        //    Padding = new Thickness(4),
        //    Text = "Name:"
        //};

        //var label2 = new Label
        //{
        //    Text = "Name:"
        //};
        //stackPanel.Widgets.Add(label1);
        //stackPanel.Widgets.Add(label2);

        //var textBox1 = new TextBox();
        //StackPanel.SetProportionType(textBox1, ProportionType.Fill);
        //stackPanel.Widgets.Add(textBox1);

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

        //AddText("Hotkeys", 1, spriteBatch);
        //AddText("Move forward: W", 2, spriteBatch);
        //AddText("Move back: S", 3, spriteBatch);
        //AddText("Move left: A", 4, spriteBatch);
        //AddText("Move right: D", 5, spriteBatch);
        //AddText("Sprint: Shift", 6, spriteBatch);
        //AddText("Full Screen: F11", 7, spriteBatch);
        //AddText("Exit Game: ESC", 8, spriteBatch);

        // "Ok" was clicked or Enter key pressed
        // ...
        //};

        //   dialog.Visible = false;

        //  dialog.ShowModal(desktop);
    }

   
}
