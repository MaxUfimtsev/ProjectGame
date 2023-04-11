using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Character;
using SharpDX.Direct2D1.Effects;

namespace MonoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private Background _backGround;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _player = new Player();
        _backGround = new Background();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
        
        _player.Texture = Texture2D.FromFile(GraphicsDevice,"Images/Player/ship-1.png");
        _backGround.Texture = Texture2D.FromFile(GraphicsDevice, "Images/Player/Screenshot_6.png");
        

        _player.Coordinate =
            new Vector2(
                Window.ClientBounds.Width * 0.46f,
                Window.ClientBounds.Height * 0.48f);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Keyboard.GetState().IsKeyDown(Keys.W))
            _player.Coordinate.Y -= 6;
        if (Keyboard.GetState().IsKeyDown(Keys.S))
            _player.Coordinate.Y += 6;
        if (Keyboard.GetState().IsKeyDown(Keys.A))
            _player.Coordinate.X -= 6;
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            _player.Coordinate.X += 6;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_backGround.Texture, new Vector2(-1, 0), Color.Purple);
        _spriteBatch.Draw(_player.Texture, _player.Coordinate, null, 
            Color.GhostWhite, 0, Vector2.Zero, 1.8f, SpriteEffects.None, 0);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}