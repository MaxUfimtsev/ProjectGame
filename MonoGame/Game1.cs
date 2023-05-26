using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Character;
using MonoGame.GameStates;

namespace MonoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private List<Enemy> _enemies = new();
    private Texture2D _backGround;
    private List<Bullet> _bullets = new();
    private ButtonState _previousButtonState;
    private SpriteFont _font;
    private State _state = State.Menu;
    private GameMenu _gameMenu;

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

        _gameMenu = new GameMenu(GraphicsDevice, Window);
        _player = new Player(GraphicsDevice, Window.ClientBounds);
        _font = Content.Load<SpriteFont>("Font");

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
        
        if (_state == State.Menu)
            UpdateMenu();

        if (_state == State.Game)
            UpdateGame();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        
        if (_state == State.Menu)
            DrawMenu();

        if (_state == State.Game)
            DrawGame();
        
        if (_state == State.EndGame)
            DrawEndGame();
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void UpdateGame()
    {
        var newBullet =
            SpawnObjects.SpawnBullet(GraphicsDevice, _player, Window.ClientBounds, ref _previousButtonState);
        
        if (newBullet != null)
            _bullets.Add(newBullet);
        
        foreach (var bullet in _bullets)
            bullet.Move();
        
        _bullets = _bullets.Where(x => x.IsInField(Window.ClientBounds)).ToList();

        
        var newEnemy = SpawnObjects.SpawnMeteor(GraphicsDevice, Window.ClientBounds, _player);
        
        if (newEnemy != null)
            _enemies.Add(newEnemy);

        foreach (var enemy in _enemies)
            enemy.Move();
        
        _player.MovePlayer(_graphics);
        _player.TurnPlayer();
        _player.UpdateHealth(_enemies);

        _enemies = _enemies.Where(x => !x.CheckCollisionWithPlayer(_player) 
                                       && _bullets.All(b => !x.CheckCollisionWithBullet(b))).ToList();

        if (_player.Health == 0)
            _state = State.EndGame;
    }

    private void DrawGame()
    {
        _spriteBatch.Draw(_backGround, new Vector2(-1, 0), Color.Purple);
        
        _spriteBatch.DrawString(_font, _player.Health.ToString(), new Vector2(20, 20), Color.Aquamarine);

        foreach (var bullet in _bullets)
            bullet.Draw(_spriteBatch);

        foreach (var enemy in _enemies)
            enemy.Draw(_spriteBatch);
        
        _player.Draw(_spriteBatch);
    }

    private void UpdateMenu()
    {
        _state = _gameMenu.IsStartPressed();
    }
    
    private void DrawMenu()
    {
        _spriteBatch.Draw(_backGround, new Vector2(-1, 0), Color.Purple);

        _spriteBatch.DrawString(_font, "PLAY", 
            new Vector2(Window.ClientBounds.Width * 0.485f, Window.ClientBounds.Height * 0.45f), 
            Color.Aquamarine);
        
        _spriteBatch.DrawString(_font, "EXIT",
            new Vector2(Window.ClientBounds.Width * 0.488f, Window.ClientBounds.Height * 0.49f),
            Color.Aquamarine);
    }

    private void DrawEndGame()
    {
        _spriteBatch.Draw(_backGround, new Vector2(-1, 0), Color.Purple);
    }
}