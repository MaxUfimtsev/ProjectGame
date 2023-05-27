using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Character;

public class Bullet
{
    private readonly Texture2D _texture;
    private Vector2 _coordinate;
    private readonly float _angle;
    private Vector2 _velocity;
    public Rectangle HitBox;

    public Bullet(GraphicsDevice graphics, Player player)
    {
        _texture = Texture2D.FromFile(graphics, "Images/Shooting/1.png");
        _coordinate = new Vector2(player.Coordinate.X, player.Coordinate.Y);
        _angle = player.Angle + (float)Math.PI / 2;
        BuildBulletPath();
    }

    public bool IsInField(Rectangle window)
    {
        return _coordinate.Y >= 0
               && _coordinate.Y <= window.Height
               && _coordinate.X >= 0
               && _coordinate.X <= window.Width;
    }

    public bool CheckCollisionWithMeteor(Enemy enemy)
    {
        return HitBox.Intersects(enemy.HitBox);
    }

    private void BuildBulletPath()
    {
        var mousePointX = _coordinate.X - Mouse.GetState().X;
        var mousePointY = _coordinate.Y - Mouse.GetState().Y;

        var mousePoint = new Vector2(mousePointX, mousePointY);
        mousePoint.Normalize();
        _velocity = mousePoint * 35;
    }

    public void Move()
    {
        _coordinate -= _velocity;
        
        UpdateHitBox();
    }
    
    private void UpdateHitBox()
    {
        HitBox = new Rectangle((int)_coordinate.X, (int)_coordinate.Y, 
            (int)(_texture.Width * 1.8), (int)(_texture.Height * 1.8));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _coordinate, null, Color.White, 
            _angle, Vector2.Zero, 1.8f, SpriteEffects.None, 0);
    }
}