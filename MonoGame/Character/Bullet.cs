using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Character;

public class Bullet
{
    public readonly Texture2D Texture;
    public Vector2 SpawnPoint;
    public float Angle;
    public Vector2 Velocity;

    public Bullet(GraphicsDevice graphics, Player player)
    {
        Texture = Texture2D.FromFile(graphics, "Images/Shooting/1.png");
        SpawnPoint = new Vector2(player.Coordinate.X, player.Coordinate.Y);
        Angle = player.Angle + (float)Math.PI / 2;
    }

    public void BuildBulletPath()
    {
        var mousePointX = SpawnPoint.X - Mouse.GetState().X;
        var mousePointY = SpawnPoint.Y - Mouse.GetState().Y;

        var mousePoint = new Vector2(mousePointX, mousePointY);
        mousePoint.Normalize();
        Velocity = mousePoint * 10;
    }

    public void GetBulletVelocity() =>
        SpawnPoint -= Velocity;

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, SpawnPoint, null, Color.White, 
            Angle, Vector2.Zero, 1.8f, SpriteEffects.None, 0);
    }
}