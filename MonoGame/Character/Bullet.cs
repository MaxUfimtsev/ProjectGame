using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Character;

public class Bullet
{
    public Texture2D Texture;
    public Vector2 SpawnPoint;
    public float Angle;
    public Vector2 Velocity;

    public Texture2D DrawTexture(GraphicsDevice graphicsDevice) =>
        Texture2D.FromFile(graphicsDevice, "Images/Shooting/1.png");
}