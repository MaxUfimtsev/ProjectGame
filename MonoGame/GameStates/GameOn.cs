using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Character;
using MonoGame.Hood;

namespace MonoGame.GameStates;

public class GameOn
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backGround;
    private Rectangle _window;
    private Player _player;
    private List<Bullet> _bullets = new();
    private List<Enemy> _enemies = new();
    private ButtonState _previousButtonState;
    private SpriteFont _font;
    private GameHUD _gameHud;

    public GameOn(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, GameWindow window, Player player,
        SpriteFont font, SpriteBatch spriteBatch)
    {
        _graphics = graphics;
        _window = window.ClientBounds;
        _player = player;
        _font = font;
        _spriteBatch = spriteBatch;
        _backGround = Texture2D.FromFile(graphicsDevice, "Images/Player/Screenshot_6.png");
        _gameHud = new GameHUD(_font, _player, _spriteBatch);
    }
    
    public State UpdateGame()
    {
        var newBullet =
            SpawnObjects.SpawnBullet(_player, ref _previousButtonState);
        
        if (newBullet != null)
            _bullets.Add(newBullet);
        
        foreach (var bullet in _bullets)
            bullet.Move();
        
        _bullets = _bullets.Where(x => x.IsInField(_window)).ToList();

        
        var newEnemy = SpawnObjects.SpawnMeteor(_window, _player);
        
        if (newEnemy != null)
            _enemies.Add(newEnemy);

        foreach (var enemy in _enemies)
            enemy.Move();
        
        _player.MovePlayer(_graphics);
        _player.TurnPlayer();
        _player.UpdateHealth(_enemies);
        _player.UpdateScore(_enemies, _bullets);
        
        _enemies = _enemies.Where(x => !x.CheckCollisionWithPlayer(_player)).ToList();
        _enemies = _enemies.Where(x => !x.CheckCollisionWithBullet(_bullets)).ToList();
        _bullets = _bullets.Where(x => !_enemies.Any(x.CheckCollisionWithMeteor)).ToList();

        return GameExtension.IsGameReplay(_player, _enemies, _bullets) ? State.Menu : State.Game;
    }
    
    public void DrawGame()
    {
        _spriteBatch.Draw(_backGround, new Vector2(-1, 0), Color.Purple);
        
        _gameHud.DrawHood();

        foreach (var bullet in _bullets)
            bullet.Draw(_spriteBatch);

        foreach (var enemy in _enemies)
            enemy.Draw(_spriteBatch);
        
        _player.Draw(_spriteBatch);
    }
}

public static class GameExtension
{
    public static bool IsGameReplay(Player player, List<Enemy> enemies, List<Bullet> bullets)
    {
        if (player.Health == 0)
        {
            player.Health = 3;
            player.Score = 0;
            enemies.RemoveRange(0, enemies.Count);
            bullets.RemoveRange(0, bullets.Count);
            return true;
        }

        return false;
    }
}