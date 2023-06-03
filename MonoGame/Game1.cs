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
    private SpriteFont _font;
    private State _state = State.Menu;
    private GameOn _gameOn;
    private GameMenu _gameMenu;
    private GamePause _gamePause;

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
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("Font");
        
        _player = new Player(GraphicsDevice, Window.ClientBounds);
        _gameMenu = new GameMenu(GraphicsDevice, Window.ClientBounds, _spriteBatch);
        _gameOn = new GameOn(_graphics, GraphicsDevice, Window, _player, _font, _spriteBatch);
        _gamePause = new GamePause(_spriteBatch, GraphicsDevice, Window.ClientBounds);

        Enemy.Texture = Texture2D.FromFile(GraphicsDevice, "Images/Enemy/Meteor.png");
        Bullet.Texture = Texture2D.FromFile(GraphicsDevice, "Images/Shooting/1.png");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            Exit();

        if (_state == State.Menu)
            _state = _gameMenu.UpdateMenu();

        if (_state == State.Game)
            _state = _gameOn.UpdateGame();

        if (_state == State.Game && _gamePause.IsGamePaused())
            _state = State.Pause;

        if (_state == State.Pause && _gamePause.IsGamePaused())
            _state = State.Game;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        
        switch (_state)
        {
            case State.Menu:
                _gameMenu.DrawMenu();
                break;
            case State.Game:
                _gameOn.DrawGame();
                break;
            case State.EndGame:
                DrawEndGame();
                break;
            case State.Pause:
                _gamePause.DrawPause();
                break;
        }

        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    private void DrawEndGame() => Exit();
}