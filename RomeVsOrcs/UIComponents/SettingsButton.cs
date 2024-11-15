using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        //if (dialog.IsPlaced && Mouse.GetState().LeftButton == ButtonState.Pressed)
        //{
        //    var mousePos = Mouse.GetState().Position;

        //    if (buttonRectangle.Contains(mousePos))
        //    {
        //        dialog.Close();
        //       // dialog.Visible = false;
        //    }
        //}

        if (!dialog.IsPlaced && Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            var mousePos = Mouse.GetState().Position;

            if (buttonRectangle.Contains(mousePos))
            {
              
                //dialog.Visible = true;
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
            Title = "Enter Your Name"
        };

        var stackPanel = new HorizontalStackPanel
        {
            Spacing = 8
        };

        var label1 = new Label
        {
            Text = "Name:"
        };
        stackPanel.Widgets.Add(label1);

        var textBox1 = new TextBox();
        StackPanel.SetProportionType(textBox1, ProportionType.Fill);
        stackPanel.Widgets.Add(textBox1);

        dialog.Content = stackPanel;

        dialog.ButtonOk.Click += (s, e) =>
        {
            dialog.Close();
        };
        //dialog.CloseButton += (s, a) =>
        //{
        //};

        //dialog.Closed += (s, a) => {
        //    if (!dialog.Result)
        //    {
        //        // Dialog was either closed or "Cancel" clicked
        //        dialog.Close();
        //        return;
        //    }



        // "Ok" was clicked or Enter key pressed
        // ...
        //};

        //   dialog.Visible = false;

        //  dialog.ShowModal(desktop);
    }
}
