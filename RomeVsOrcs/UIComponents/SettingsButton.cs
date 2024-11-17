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
                Text = textB
            };
            horizontalStackPanel.Widgets.Add(labelB);
            stackPanel.Widgets.Add(horizontalStackPanel);
        }
    }


    private void LoadRanksDialog()
    {
        
        VerticalStackPanel stackPanel = new VerticalStackPanel();
        AddLabel("Velites", "Light infantry skirmisher.");
        AddLabel("Miles", "The basic soldier in the Roman army.");
        AddLabel("Equites", "Cavalry unit.");
        AddLabel("Praetorian Guard", "Elite unit tasked with protecting the Emperor.");
        AddLabel("Signifer", "Standard bearer for a century.");
        AddLabel("Aquilifer", "Standard bearer of the legion.");
        AddLabel("Optio", "Second-in-command to a centurion.");

       
        menuDialog.Content = stackPanel;

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

//Legatus

//Senior officer, often in command of a legion or a large military unit.
//Appointed by the Roman Senate or the Emperor, typically from the senatorial class.
//Responsible for strategic decisions, military operations, and diplomatic relations.
//Acts as a liaison between the military and political authorities.

//Tribunus

//A junior officer rank, often serving as a staff officer or in command of a cohort.
//Typically comprised of six tribunes per legion, often from the equestrian class.
//Responsible for training soldiers, maintaining discipline, and leading troops in battle.
//Often used as a stepping stone for future political careers.

//Centurio

//A centurion commands a century, which is about 80-100 soldiers.
//Key figure in maintaining discipline and training within the ranks.
//Promoted based on merit and experience, often from the ranks of the miles.
//Plays a crucial role in battlefield tactics and troop movements.

//Optio

//Second-in-command to a centurion, assisting in leadership and training.
//Often responsible for administrative tasks and maintaining order within the century.
//Selected for their experience and leadership potential, often promoted from the ranks.
//Acts as a key communicator between the centurion and the soldiers.

//Aquilifer

//Standard bearer of the legion, responsible for carrying the eagle standard (aquila).
//Symbolizes the honor and spirit of the legion, crucial for morale.
//Holds a prestigious position, often selected for bravery and loyalty.
//Plays a vital role in maintaining formation and rallying troops during battle.

//Signifer

//Standard bearer for a century, carrying the unit's standard (signum).
//Responsible for communication and signaling during battles.
//Often serves as a paymaster, managing the distribution of wages to soldiers.
//Holds a respected position within the ranks, often chosen for reliability.

//Miles

//The basic soldier in the Roman army, typically a heavy infantryman.
//Fought in formations, primarily the manipular system, and later the cohort system.
//Required to be a Roman citizen, often equipped with armor and weapons.
//Played a crucial role in the expansion and defense of the Roman Empire.

//Velites

//Light infantry skirmishers, often used for harassment and reconnaissance.
//Typically younger soldiers, not yet fully equipped as heavy infantry.
//Armed with javelins and lighter armor, providing flexibility on the battlefield.
//Played a key role in the early phases of battle, disrupting enemy formations.

//Equites

//Cavalry units, often composed of wealthier citizens who could afford horses.
//Served as scouts, flanking forces, and in pursuit of fleeing enemies.
//Played a significant role in battles, providing mobility and speed.
//Often held a social status above infantry soldiers, with many becoming officers.

//Praetorian Guard

//Elite unit tasked with protecting the Emperor and high-ranking officials.
//Comprised of highly trained soldiers, often selected for their loyalty and skill.
//Played a significant role in political power dynamics, sometimes influencing succession.
//Served as both a military and police force within Rome, maintaining order.