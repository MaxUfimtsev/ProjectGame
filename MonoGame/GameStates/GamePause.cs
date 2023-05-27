using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.GameStates;

public class GamePause
{
    private KeyboardState _currentButton;
    private KeyboardState _prevButton;
    private SpriteBatch _spriteBatch;
    private Texture2D _texture;
    private Vector2 _coordinate;
    private float _scale;

    public GamePause(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Rectangle window)
    {
        _spriteBatch = spriteBatch;
        _texture = Texture2D.FromFile(graphicsDevice, "Images/Pause/Pause.png");
        _coordinate = new Vector2(window.Width * 0.465f, window.Height * 0.45f);
        _scale = 0.07f;
    }
    
    public bool IsGamePaused()
    {
        _prevButton = _currentButton;
        _currentButton = Keyboard.GetState();

        return _prevButton.IsKeyUp(Keys.Escape) && _currentButton.IsKeyDown(Keys.Escape);
    }

    public void DrawPause()
    {
        _spriteBatch.Draw(_texture, _coordinate, null, Color.White, 0, 
            Vector2.Zero, _scale, SpriteEffects.None, 0);
    }
}