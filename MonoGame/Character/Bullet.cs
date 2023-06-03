using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Character;

public class Bullet
{
    public static Texture2D Texture;
    public Rectangle HitBox;
    private Vector2 _coordinate;
    private Vector2 _velocity;
    private readonly float _angle;

    public Bullet(Player player)
    {
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

    public bool CheckCollisionWithMeteor(List<Enemy> enemies)
    {
        foreach (var enemy in enemies)
            if (HitBox.Intersects(enemy.HitBox))
                return true;

        return false;
    }

    public void Move()
    {
        _coordinate -= _velocity;
        UpdateHitBox();
    }
    
    private void BuildBulletPath()
    {
        var mousePointX = _coordinate.X - Mouse.GetState().X;
        var mousePointY = _coordinate.Y - Mouse.GetState().Y;

        var mousePoint = new Vector2(mousePointX, mousePointY);
        mousePoint.Normalize();
        _velocity = mousePoint * 35;
    }
    
    private void UpdateHitBox()
    {
        HitBox = new Rectangle((int)_coordinate.X, (int)_coordinate.Y, 
            (int)(Texture.Width * 1.8), (int)(Texture.Height * 1.8));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, _coordinate, null, Color.White, 
            _angle, Vector2.Zero, 1.8f, SpriteEffects.None, 0);
    }
}