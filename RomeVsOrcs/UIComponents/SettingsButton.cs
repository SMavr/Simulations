using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using System.ComponentModel;

namespace RomeVsOrcs.UIComponents;
internal class SettingsButton(ContentManager content, GraphicsDeviceManager graphics, Desktop desktop)
{
    Dialog hotkeysDialog;
    Dialog menuDialog;

    public void Load()
    {
        LoadButton();
        LoadSettingsDialog();
        LoadMenuDialog();
    }

    private void LoadSettingsDialog()
    {
        hotkeysDialog = new Dialog
        {
            Title = "Hot Keys",
            Padding = new Thickness(20)
        };


        VerticalStackPanel stackPanel = new VerticalStackPanel();
        AddLabel("Move forward:","W");
        AddLabel("Move back:","S");
        AddLabel("Move left:","A");
        AddLabel("Move right:","D");
        AddLabel("Sprint:","Shift");
        AddLabel("Full Screen:","F11");
        AddLabel("Exit Game:","ESC");

        hotkeysDialog.Content = stackPanel;

        hotkeysDialog.ButtonOk.Click += (s, e) =>
        {
            hotkeysDialog.Close();
        };

        void AddLabel(string textA, string textB)
        {
            HorizontalStackPanel horizontalStackPanel = new HorizontalStackPanel();
            var labelA = new Label
            {
                Width = 150,
                Text = textA
            };
            horizontalStackPanel.Widgets.Add(labelA);

            var labelB = new Label
            {
                Width = 50,
                Text = textB
            };
            horizontalStackPanel.Widgets.Add(labelB);
            stackPanel.Widgets.Add(horizontalStackPanel);
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
            menuDialog.ShowModal(desktop);
        };

        // Add the button to the desktop
        desktop.Widgets.Add(button);
    }

    private void LoadMenuDialog()
    {
        menuDialog = new Dialog
        {
            Title = "Menu",
            Padding = new Thickness(20)
        };

        var stackPanel = new VerticalStackPanel
        {
            Spacing = 4
        };

        var titleContainer = new Panel
        {
        };

        var titleLabel = new Label
        {
            Text = "Choose Option",
            HorizontalAlignment = HorizontalAlignment.Center
        };

        titleContainer.Widgets.Add(titleLabel);
        stackPanel.Widgets.Add(titleContainer);

        var menuItem1 = new MenuItem();
        menuItem1.Text = "Start New Game";
        menuItem1.Selected += (s, a) =>
        {
            // "Start New Game" selected
        };

        var menuItem2 = new MenuItem();
        menuItem2.Text = "Options";

        var menuItem3 = new MenuItem();
        menuItem3.Text = "Quit";

        var verticalMenu = new VerticalMenu();

        verticalMenu.Items.Add(menuItem1);
        verticalMenu.Items.Add(menuItem2);
        verticalMenu.Items.Add(menuItem3);

        stackPanel.Widgets.Add(verticalMenu);

        menuDialog.Content = stackPanel;
        //desktop.Widgets.Add(stackPanel);
    }
}
