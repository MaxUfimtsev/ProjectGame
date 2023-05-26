using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace MonoGame.GameStates;

public class GameMenu
{
    private Rectangle _playHitBox;
    private Texture2D _texture;
    private Vector2 _textureCoordinate;

    public GameMenu(GraphicsDevice graphicsDevice,GameWindow window)
    {
        _texture = Texture2D.FromFile(graphicsDevice, "Images/MenuTexture/abc.png");
        _textureCoordinate = new Vector2(window.ClientBounds.Width * 0.485f, window.ClientBounds.Height * 0.45f);
        _playHitBox = new Rectangle((int)_textureCoordinate.X, (int)_textureCoordinate.Y, 
            _texture.Width, _texture.Height);
    }
    
    public State IsStartPressed()
    {
        var mouseLocation = new Point(Mouse.GetState().X, Mouse.GetState().Y);
        var mouseSize = new Point(1, 1);
        var mouseRectangle = new Rectangle(mouseLocation, mouseSize);
        
        if (_playHitBox.Intersects(mouseRectangle) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            return State.Game;

        return State.Menu;
    }

    public void DrawTexture(SpriteBatch spriteBatch, GameWindow window)
    {
        spriteBatch.Draw(_texture, _textureCoordinate, null, Color.Aquamarine);
    }
}