using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Character;

namespace MonoGame.GameStates;

public class GameOn
{
    private GraphicsDevice _graphicsDevice;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _backGround;
    private Rectangle _window;
    private Player _player;
    private List<Bullet> _bullets = new();
    private List<Enemy> _enemies = new();
    private ButtonState _previousButtonState;
    private int _score;
    private SpriteFont _font;

    public GameOn(GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, GameWindow window, Player player, 
        SpriteFont font, SpriteBatch spriteBatch)
    {
        _graphicsDevice = graphicsDevice;
        _graphics = graphics;
        _window = window.ClientBounds;
        _player = player;
        _font = font;
        _spriteBatch = spriteBatch;
        _backGround = Texture2D.FromFile(graphicsDevice, "Images/Player/Screenshot_6.png");
    }
    
    public State UpdateGame()
    {
        var newBullet =
            SpawnObjects.SpawnBullet(_graphicsDevice, _player, ref _previousButtonState);
        
        if (newBullet != null)
            _bullets.Add(newBullet);
        
        foreach (var bullet in _bullets)
            bullet.Move();
        
        _bullets = _bullets.Where(x => x.IsInField(_window)).ToList();

        
        var newEnemy = SpawnObjects.SpawnMeteor(_graphicsDevice, _window, _player);
        
        if (newEnemy != null)
            _enemies.Add(newEnemy);

        foreach (var enemy in _enemies)
            enemy.Move();
        
        _player.MovePlayer(_graphics);
        _player.TurnPlayer();
        _player.UpdateHealth(_enemies);

        var hittedMeteor = _enemies.Where(x => _bullets.Any(x.CheckCollisionWithBullet)).ToList();
        _enemies = _enemies.Where(x => !x.CheckCollisionWithPlayer(_player)).ToList();
        _bullets = _bullets.Where(x => !_enemies.Any(x.CheckCollisionWithMeteor)).ToList();
        _score += _enemies.RemoveAll(enemy => hittedMeteor.Contains(enemy));

        return _player.Health == 0 ? State.Menu : State.Game;
    }
    
    public void DrawGame()
    {
        _spriteBatch.Draw(_backGround, new Vector2(-1, 0), Color.Purple);
        
        _spriteBatch.DrawString(_font, $"Health: {_player.Health.ToString()}", 
            new Vector2(20, 20), Color.Aquamarine);
        
        _spriteBatch.DrawString(_font, $"Score: {_score}", new Vector2(90, 20), Color.Aquamarine);

        foreach (var bullet in _bullets)
            bullet.Draw(_spriteBatch);

        foreach (var enemy in _enemies)
            enemy.Draw(_spriteBatch);
        
        _player.Draw(_spriteBatch);
    }
}