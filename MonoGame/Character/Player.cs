﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Character;

public class Player
{
    public Vector2 Coordinate;
    public readonly Texture2D Texture;
    public float Angle;

    public Player(GraphicsDevice graphics, Rectangle window)
    {
        Texture = Texture2D.FromFile(graphics, "Images/Player/ship-1.png");
        Coordinate = new Vector2(window.Width * 0.46f, window.Height * 0.48f);
    }

    public void MovePlayer(GraphicsDeviceManager graphics)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.W) && Coordinate.Y > Texture.Height)
            Coordinate.Y -= 6;
        if (Keyboard.GetState().IsKeyDown(Keys.S) 
            && Coordinate.Y < graphics.PreferredBackBufferHeight - Texture.Height)
            Coordinate.Y += 6;
        if (Keyboard.GetState().IsKeyDown(Keys.A) && Coordinate.X > Texture.Width)
            Coordinate.X -= 6;
        if (Keyboard.GetState().IsKeyDown(Keys.D) 
            && Coordinate.X < graphics.PreferredBackBufferWidth - Texture.Width)
            Coordinate.X += 6;
    }

    public void TurnPlayer()
    {
        var line1 = Coordinate.X - Mouse.GetState().X;
        var line2 = Coordinate.Y - Mouse.GetState().Y;

        var tg = line1 / line2;

        Angle = -(float)Math.Atan(tg) + (line2 < 0 ? 135 : 0);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Coordinate, null, Color.GhostWhite, Angle, 
            new Vector2(Texture.Width / 2, Texture.Height / 2), 1.8f, SpriteEffects.None, 0);
    }
}