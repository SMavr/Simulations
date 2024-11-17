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
    Window hotkeysDialog;
    Window menuDialog;

    public void Load()
    {
        LoadButton();
        //LoadSettingsDialog();
        //LoadMenuDialog();
    }

    private void LoadSettingsDialog()
    {
        hotkeysDialog = new Window
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

        menuDialog.Content = stackPanel;

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
            LoadMenuDialog();
            menuDialog.Close();
            menuDialog.ShowModal(desktop);
        };

        // Add the button to the desktop
        desktop.Widgets.Add(button);
    }

    private void LoadMenuDialog()
    {
        menuDialog = new Window
        {
            Title = "Menu",
            Padding = new Thickness(20)
        };

        var stackPanel = new VerticalStackPanel
        {
            Spacing = 20
        };

        var titleContainer = new Panel();

        var titleLabel = new Label
        {
            Text = "Choose Option",
            HorizontalAlignment = HorizontalAlignment.Center
        };

        titleContainer.Widgets.Add(titleLabel);
        stackPanel.Widgets.Add(titleContainer);

        var menuItem1 = new MenuItem();
        menuItem1.Text = "Hotkeys";
        menuItem1.Selected += (s, a) =>
        {
            LoadSettingsDialog();
            menuDialog.Close();
            menuDialog.ShowModal(desktop);
        };

        var menuItem2 = new MenuItem();
        menuItem2.Text = "Toggle Fullscreen";

        var menuItem3 = new MenuItem();
        menuItem3.Text = "Ranks";

        var verticalMenu = new VerticalMenu();

        verticalMenu.Items.Add(menuItem1);
        verticalMenu.Items.Add(menuItem2);
        verticalMenu.Items.Add(menuItem3);

        stackPanel.Widgets.Add(verticalMenu);

        menuDialog.Content = stackPanel;
        //desktop.Widgets.Add(stackPanel);
    }
}
