using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Character;

namespace MonoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private Texture2D _backGround;
    private List<Bullet> _bullets = new();
    private ButtonState _previousButtonState;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
        
        _player = new Player(GraphicsDevice, Window.ClientBounds);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backGround = Texture2D.FromFile(GraphicsDevice, "Images/Player/Screenshot_6.png");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _player.MovePlayer(_graphics);
        _player.TurnPlayer();

        if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton != _previousButtonState)
        {
            var bullet = new Bullet(GraphicsDevice, _player);

            bullet.BuildBulletPath();
            
            _bullets.Add(bullet);
        }

        _previousButtonState = Mouse.GetState().LeftButton;

        foreach (var bullet in _bullets)
            bullet.GetBulletVelocity();
        
        _bullets = _bullets.Where(x => x.SpawnPoint.Y >= 0).ToList();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_backGround, new Vector2(-1, 0), Color.Purple);
        
        if (_bullets != null)
            foreach (var bullet in _bullets)
                bullet.Draw(_spriteBatch);
        
        _player.Draw(_spriteBatch);

        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}