using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Character;

public static class SpawnObjects
{
    public static Bullet SpawnBullet(GraphicsDevice graphicsDevice, Player player, ref ButtonState previousButtonState)
    {
        Bullet bullet = null;
        
        if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton != previousButtonState)
            bullet = new Bullet(graphicsDevice, player);
        
        previousButtonState = Mouse.GetState().LeftButton;

        return bullet;
    }
    
    public static Enemy SpawnMeteor(GraphicsDevice graphicsDevice, Rectangle window, Player player)
    {
        var random = new Random().Next(1, 50);
        Enemy meteor = null;
        
        if (random == 2)
            meteor = new Enemy(graphicsDevice, window, player);
        
        return meteor;
    }
}