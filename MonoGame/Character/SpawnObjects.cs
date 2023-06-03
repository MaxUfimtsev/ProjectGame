using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Character;

public static class SpawnObjects
{
    public static Bullet SpawnBullet(Player player, ref ButtonState previousButtonState)
    {
        Bullet bullet = null;
        
        if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton != previousButtonState)
            bullet = new Bullet(player);
        
        previousButtonState = Mouse.GetState().LeftButton;

        return bullet;
    }
    
    public static Enemy SpawnMeteor(Rectangle window, Player player)
    {
        var random = new Random().Next(1, 50);
        Enemy meteor = null;
        
        if (random == 2)
            meteor = new Enemy(window, player);
        
        return meteor;
    }
}