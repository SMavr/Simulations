using Clusters.Constants;
using Clusters.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Clusters;

public class ClustersGame : Game
{
    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private CircleFactory circleFactory = new CircleFactory();

    public ClustersGame()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        graphics.SetDimensions(WindowConstants.Width, WindowConstants.Height);
        //circleFactory.AddTestCircles();

        circleFactory.AddCircles(40, Color.Red, 1, [new(3, 4)]);
        circleFactory.AddCircles(40, Color.Blue, 2, [new (1, -6), new (4, -2)]);
        circleFactory.AddCircles(40, Color.Yellow, 3, [new(1, -4)]);
        circleFactory.AddCircles(40, Color.Green, 4, [new (2, 3)]);
        circleFactory.AddCircles(40, Color.BurlyWood, 5, [new ( 3, 2)]);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        circleFactory.Move(deltaTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);


        spriteBatch.Begin();
        circleFactory.DrawCircles(spriteBatch);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}
