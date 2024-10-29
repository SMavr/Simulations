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

        circleFactory.AddCircles(10, Color.Red);
        circleFactory.AddCircles(10, Color.Blue);
        circleFactory.AddCircles(10, Color.Yellow);
        circleFactory.AddCircles(10, Color.Green);

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
