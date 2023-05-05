using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Character;

public class Bullet
{
    private readonly Texture2D _texture;
    private Vector2 _spawnPoint;
    private readonly float _angle;
    private Vector2 _velocity;

    public Bullet(GraphicsDevice graphics, Player player, Rectangle window)
    {
        _texture = Texture2D.FromFile(graphics, "Images/Shooting/1.png");
        _spawnPoint = new Vector2(player.Coordinate.X, player.Coordinate.Y);
        _angle = player.Angle + (float)Math.PI / 2;
        BuildBulletPath();
    }

    public bool IsInField(Rectangle window)
    {
        return _spawnPoint.Y >= 0
               && _spawnPoint.Y <= window.Height
               && _spawnPoint.X >= 0
               && _spawnPoint.X <= window.Width;
    }

    private void BuildBulletPath()
    {
        var mousePointX = _spawnPoint.X - Mouse.GetState().X;
        var mousePointY = _spawnPoint.Y - Mouse.GetState().Y;

        var mousePoint = new Vector2(mousePointX, mousePointY);
        mousePoint.Normalize();
        _velocity = mousePoint * 15;
    }

    public void Move()
    {
        _spawnPoint -= _velocity;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _spawnPoint, null, Color.White, 
            _angle, Vector2.Zero, 1.8f, SpriteEffects.None, 0);
    }
}