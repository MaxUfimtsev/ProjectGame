﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Character;

public class Enemy
{
    public static Texture2D Texture;
    public Vector2 Coordinate;
    public Vector2 Velocity;
    public float Rotation;
    public Rectangle HitBox;
    public int HitsCounter;

    public Enemy(Rectangle window, Player player)
    {
        var random = new Random();
        var side = random.Next(1, 4);
        if (side == 1)
            Coordinate = new Vector2(-150, random.Next(0, window.Height));
        else if (side == 2)
            Coordinate = new Vector2(random.Next(0, window.Width), -150);
        else
            Coordinate = new Vector2(window.Width + 50, random.Next(0, window.Height));

        Rotation = new Random().Next(0, 360);
        
        BuildMeteorPath(player);
    }

    public bool CheckCollisionWithPlayer(Player player) =>
        HitBox.Intersects(player.HitBox);
    
    public bool CheckCollisionWithBullet(List<Bullet> bullets)
    {
        foreach (var bullet in bullets.Where(bullet => bullet.HitBox.Intersects(HitBox)))
            HitsCounter++;

        return HitsCounter == 2;
    }

    public void Move()
    {
        Coordinate -= Velocity;
        UpdateHitBox();
    }
    
    private void BuildMeteorPath(Player player)
    {
        var pointX = Coordinate.X - player.Coordinate.X;
        var pointY = Coordinate.Y - player.Coordinate.Y;

        var path = new Vector2(pointX, pointY);
        path.Normalize();
        Velocity = path * 7;
    }

    private void UpdateHitBox()
    {
        HitBox = new Rectangle((int)Coordinate.X, (int)Coordinate.Y,
            (int)(Texture.Width * 0.05), (int)(Texture.Height * 0.05));
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Coordinate, null, Color.White, Rotation,
            new Vector2(Texture.Width / 2, Texture.Height / 2), 0.08f, SpriteEffects.None, 0);
    }
}