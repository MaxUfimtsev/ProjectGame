using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MonoGame.GameStates;

public class Button
{
    public string Text;
    public Vector2 Location;
    public Texture2D Texture;
    private Rectangle _hitbox;
    public float Scale;

    public Button(string text, Vector2 location, Texture2D texture, float scale)
    {
        Text = text;
        Location = location;
        Texture = texture;
        Scale = scale;
        _hitbox = new Rectangle(Location.ToPoint(), 
            new Point((int)(texture.Width * scale), (int)(texture.Height * scale)));
    }

    public bool IsPressed(Point mouseLocation)
    {
        return _hitbox.Contains(mouseLocation) && Mouse.GetState().LeftButton == ButtonState.Pressed;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Location, null, 
            Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
    }
}