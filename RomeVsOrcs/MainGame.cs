using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using Myra;
using RomeVsOrcs.Textures;
using RomeVsOrcs.UIComponents;

namespace RomeVsOrcs;

public class MainGame : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private SoldierTexture soldierTexture;
    private OrcFactory orcFactory;
    private StatsTexture statsTexture;
    private GrassTexture grassTexture;
    private SettingsButton settingsButton;


    public MainGame()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.GraphicsProfile = GraphicsProfile.HiDef;
        //graphics.IsFullScreen = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        Viewport viewport = graphics.GraphicsDevice.Viewport;

        statsTexture = new StatsTexture(Content);
        statsTexture.Load();
        settingsButton = new SettingsButton(Content, GraphicsDevice);
        settingsButton.Load();


        grassTexture = new GrassTexture(Content, viewport);
        grassTexture.Load();

        soldierTexture = new SoldierTexture(Content, graphics.GraphicsDevice.Viewport);
        Vector2 characterPos = new Vector2(viewport.Width / 2, viewport.Height / 2);
        soldierTexture.Load(characterPos);

        orcFactory = new OrcFactory(Content, viewport);
        orcFactory.Load(4);
        LoadDialog();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if (Keyboard.GetState().IsKeyDown(Keys.F11))
            ToggleFullScreen();

        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        soldierTexture.Update(elapsed, orcFactory.OrcTextures);
        orcFactory.Update(elapsed);
        settingsButton.Update(elapsed);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Green);
     
     
        spriteBatch.Begin();
        grassTexture.Draw(spriteBatch);
        orcFactory.Draw(spriteBatch);
        soldierTexture.Draw(spriteBatch);
        statsTexture.Draw(spriteBatch);
        settingsButton.Draw(spriteBatch);
        spriteBatch.End();
        _desktop.Render();

        base.Draw(gameTime);
    }

    private void ToggleFullScreen()
    {
        graphics.IsFullScreen = !graphics.IsFullScreen;
        graphics.ApplyChanges();
    }

    private Desktop _desktop;
    private Dialog _dialog;
    private Label _label;
    private void LoadDialog()
    {
        MyraEnvironment.Game = this;

        // Set up the Desktop environment
        _desktop = new Desktop();

        Dialog dialog = new Dialog
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

        dialog.Closed += (s, a) => {
            if (!dialog.Result)
            {
                // Dialog was either closed or "Cancel" clicked
                return;
            }

            // "Ok" was clicked or Enter key pressed
            // ...
        };

        dialog.ShowModal(_desktop);
    }
}
