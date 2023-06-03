using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Character;

public class Player
{
    public Vector2 StartCoordinate;
    public Vector2 Coordinate;
    public static Texture2D Texture;
    public float Angle;
    public Rectangle HitBox;
    public int Health = 3;
    public int Score;
    private const int Speed = 6;
    private GraphicsDeviceManager _graphics;

    public Player(Rectangle window, GraphicsDeviceManager graphics)
    {
        _graphics = graphics;
        StartCoordinate = new Vector2(window.Width * 0.5f, window.Height * 0.5f);
        Coordinate = StartCoordinate;
    }

    public void MovePlayer()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.W) && Coordinate.Y > Texture.Height)
            Coordinate.Y -= Speed;
        if (Keyboard.GetState().IsKeyDown(Keys.S) 
            && Coordinate.Y < _graphics.PreferredBackBufferHeight - Texture.Height)
            Coordinate.Y += Speed;
        if (Keyboard.GetState().IsKeyDown(Keys.A) && Coordinate.X > Texture.Width)
            Coordinate.X -= Speed;
        if (Keyboard.GetState().IsKeyDown(Keys.D) 
            && Coordinate.X < _graphics.PreferredBackBufferWidth - Texture.Width)
            Coordinate.X += Speed;
        
        UpdateHitBox();
    }

    public void UpdateHealth(List<Enemy> enemies)
    {
        foreach (var enemy in enemies.Where(enemy => HitBox.Intersects(enemy.HitBox)))
            Health--;
    }

    public void UpdateScore(List<Enemy> enemies, List<Bullet> bullets)
    {
        foreach (var enemy in enemies)
        foreach (var bullet in bullets)
            if (bullet.HitBox.Intersects(enemy.HitBox) && enemy.HitsCounter == 1)
                Score += 5;
    }

    public void TurnPlayer()
    {
        var line1 = Coordinate.X - Mouse.GetState().X;
        var line2 = Coordinate.Y - Mouse.GetState().Y;

        var tg = line1 / line2;

        Angle = -(float)Math.Atan(tg) + (line2 < 0 ? 135 : 0);
    }
    
    private void UpdateHitBox()
    {
        HitBox = new Rectangle((int)Coordinate.X, (int)Coordinate.Y, 
            Texture.Width * 2, Texture.Height * 2);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Coordinate, null, Color.GhostWhite, Angle, 
            new Vector2(Texture.Width / 2, Texture.Height / 2), 1.8f, SpriteEffects.None, 0);
    }
}