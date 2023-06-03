using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Character;
using MonoGame.HUD;

namespace MonoGame.GameStates;

public class GameOn
{
    public static Texture2D BackGround;
    private List<Bullet> _bullets = new();
    private List<Enemy> _enemies = new();
    private ButtonState _previousButtonState;
    private readonly SpriteBatch _spriteBatch;
    private readonly Rectangle _window;
    private readonly Player _player;
    private readonly GameHUD _gameHud;

    public GameOn(GameWindow window, Player player, SpriteFont font, SpriteBatch spriteBatch)
    {
        _window = window.ClientBounds;
        _player = player;
        _spriteBatch = spriteBatch;
        _gameHud = new GameHUD(font, _player, _spriteBatch);
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
        
        _player.MovePlayer();
        _player.TurnPlayer();
        _player.UpdateHealth(_enemies);
        _player.UpdateScore(_enemies, _bullets);
        
        _enemies = _enemies.Where(x => !x.CheckCollisionWithPlayer(_player)).ToList();
        _enemies = _enemies.Where(x => !x.CheckCollisionWithBullet(_bullets)).ToList();
        _bullets = _bullets.Where(x => !x.CheckCollisionWithMeteor(_enemies)).ToList();

        return GameExtension.IsGameReplay(_player, _enemies, _bullets) ? State.Menu : State.Game;
    }
    
    public void DrawGame()
    {
        _spriteBatch.Draw(BackGround, new Vector2(-1, 0), Color.Purple);
        
        _gameHud.DrawHud();

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
            player.Coordinate = player.StartCoordinate;
            enemies.RemoveRange(0, enemies.Count);
            bullets.RemoveRange(0, bullets.Count);
            return true;
        }

        return false;
    }
}