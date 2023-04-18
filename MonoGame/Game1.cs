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
    private Background _backGround;
    private List<ShootingControl> _bullets = new ();
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
        
        _player = new Player();
        _backGround = new Background();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

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

        if (Keyboard.GetState().IsKeyDown(Keys.W) && _player.Coordinate.Y > 0)
            _player.Coordinate.Y -= 6;
        if (Keyboard.GetState().IsKeyDown(Keys.S) 
            && _player.Coordinate.Y < _graphics.PreferredBackBufferHeight - _player.Texture.Height * 2)
            _player.Coordinate.Y += 6;
        if (Keyboard.GetState().IsKeyDown(Keys.A) && _player.Coordinate.X > 0)
            _player.Coordinate.X -= 6;
        if (Keyboard.GetState().IsKeyDown(Keys.D) 
            && _player.Coordinate.X < _graphics.PreferredBackBufferWidth - _player.Texture.Width * 2)
            _player.Coordinate.X += 6;
        
        
        if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton != _previousButtonState)
        {
            var shootingControl = new ShootingControl();
            shootingControl.Texture = shootingControl.DrawTexture(GraphicsDevice);
            shootingControl.BulletPath = 
                new Vector2(_player.Coordinate.X + _player.Texture.Width / 2 * 1.8f, _player.Coordinate.Y);
            _bullets.Add(shootingControl);
        }

        _previousButtonState = Mouse.GetState().LeftButton;

        foreach (var bullet in _bullets)
        {
            bullet.BulletPath.Y -= 15;
        }

        _bullets = _bullets.Where(x => x.BulletPath.Y >= 0).ToList();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_backGround.Texture, new Vector2(-1, 0), Color.Purple);
        
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