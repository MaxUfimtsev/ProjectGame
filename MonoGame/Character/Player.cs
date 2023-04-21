using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Character;

public class Player
{
    public Vector2 Coordinate;
    public Texture2D Texture;

    public Player(GraphicsDevice graphics, Rectangle window)
    {
        Texture = Texture2D.FromFile(graphics, "Images/Player/ship-1.png");
        Coordinate = new Vector2(window.Width * 0.46f, window.Height * 0.48f);
    }

    public void PlayerControls(GraphicsDeviceManager graphics)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.W) && Coordinate.Y > 0)
            Coordinate.Y -= 6;
        if (Keyboard.GetState().IsKeyDown(Keys.S) 
            && Coordinate.Y < graphics.PreferredBackBufferHeight - Texture.Height * 2)
            Coordinate.Y += 6;
        if (Keyboard.GetState().IsKeyDown(Keys.A) && Coordinate.X > 0)
            Coordinate.X -= 6;
        if (Keyboard.GetState().IsKeyDown(Keys.D) 
            && Coordinate.X < graphics.PreferredBackBufferWidth - Texture.Width * 2)
            Coordinate.X += 6;
    }
}