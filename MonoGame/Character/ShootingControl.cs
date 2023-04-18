using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Character;

public class ShootingControl
{
    public Texture2D Texture;
    public Vector2 BulletPath;

    public Texture2D DrawTexture(GraphicsDevice graphicsDevice) =>
        Texture2D.FromFile(graphicsDevice, "Images/Shooting/1.png");
    
    
    
}