using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;

namespace RomeVsOrcs.UIComponents;
internal class SettingsButton(ContentManager content, GraphicsDeviceManager graphics, Desktop desktop)
{
    Window menuDialog;

    public void Load()
    {
        LoadButton();
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

        var hotKeysItem = new MenuItem();
        hotKeysItem.Text = "Hotkeys";
        hotKeysItem.Selected += (s, a) =>
        {
            LoadHotKeysDialog();
            menuDialog.Close();
            menuDialog.ShowModal(desktop);
        };

        var ranksItem = new MenuItem();
        ranksItem.Text = "Ranks";
        ranksItem.Selected += (s, a) =>
        {
            LoadRanksDialog();
            menuDialog.Close();
            menuDialog.ShowModal(desktop);
        };

        var verticalMenu = new VerticalMenu();

        verticalMenu.Items.Add(hotKeysItem);
        verticalMenu.Items.Add(ranksItem);

        stackPanel.Widgets.Add(verticalMenu);

        menuDialog.Content = stackPanel;
    }

    private void LoadHotKeysDialog()
    {
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
                Text = textB,
            };
            horizontalStackPanel.Widgets.Add(labelB);
            stackPanel.Widgets.Add(horizontalStackPanel);
        }
    }


    private void LoadRanksDialog()
    {

        ScrollViewer scrollViewer = new ScrollViewer()
        {
            Width = 400,
            Height = 250,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };
        VerticalStackPanel stackPanel = new VerticalStackPanel();
        AddLabel("Velites (0)", "Light infantry skirmisher.");
        AddLabel("Miles (1)", "The basic soldier in the Roman army.");
        AddLabel("Equites (4)", "Cavalry unit.");
        AddLabel("Praetorian Guard (8)", "Elite unit tasked with protecting the Emperor.");
        AddLabel("Signifer (16)", "Standard bearer for a century.");
        AddLabel("Aquilifer (32)", "Standard bearer of the legion.");
        AddLabel("Optio (64)", "Second-in-command to a centurion.");
        AddLabel("Centurio (96)", "A centurion commands a century.");
        AddLabel("Tribunus (128)", "A junior officer rank.");
        AddLabel("Legatus (192)", "Senior officer, often in command of a legion.");

        scrollViewer.Content = stackPanel;
        menuDialog.Content = scrollViewer;

        void AddLabel(string textA, string textB)
        {
            VerticalStackPanel rankPanel = new VerticalStackPanel();
            var labelA = new Label
            {
                Width = 150,
                Text = textA,
                TextColor = Color.Aqua
            };
            rankPanel.Widgets.Add(labelA);

            var labelB = new Label
            {
                Width = 400,
                Text = textB
            };
            rankPanel.Widgets.Add(labelB);
            stackPanel.Widgets.Add(rankPanel);
        }
    }
}