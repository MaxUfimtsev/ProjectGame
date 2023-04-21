using System;
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
    private List<Bullet> _bullets = new ();
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

        _player.PlayerControls(_graphics);

        if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton != _previousButtonState)
        {
            var shootingControl = new Bullet();
            shootingControl.Texture = shootingControl.DrawTexture(GraphicsDevice);
            shootingControl.BulletPath = 
                new Vector2(_player.Coordinate.X + _player.Texture.Width / 2 * 1.8f, _player.Coordinate.Y);
            _bullets.Add(shootingControl);
        }

        _previousButtonState = Mouse.GetState().LeftButton;

        foreach (var bullet in _bullets)
            bullet.BulletPath.Y -= 15;
        
        _bullets = _bullets.Where(x => x.BulletPath.Y >= 0).ToList();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_backGround, new Vector2(-1, 0), Color.Purple);
        
        _spriteBatch.Draw(_player.Texture, _player.Coordinate, null, 
            Color.GhostWhite, 0, Vector2.Zero, 1.8f, SpriteEffects.None, 0);

        if (_bullets != null)
        {
            foreach (var bullet in _bullets)
            {
                _spriteBatch.Draw(bullet.Texture, bullet.BulletPath, null,
                    Color.White, (float)Math.PI / 2, Vector2.Zero, 1.8f, SpriteEffects.None, 0);
            }
        }

        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}