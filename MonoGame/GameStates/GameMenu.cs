using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.GameStates;

public class GameMenu
{
    private SpriteBatch _spriteBatch;
    private Button _startButton;
    private Button _exitButton;
    private Texture2D _backGround;
    private const float Scale = 0.05f;

    public GameMenu(GraphicsDevice graphicsDevice, Rectangle window, SpriteBatch spriteBatch)
    {
        _startButton = new Button(null, 
            new Vector2(window.Width * 0.45f, window.Height * 0.45f), 
            Texture2D.FromFile(graphicsDevice, "Images/MenuTexture/StartButton.png"), Scale);
        
        _exitButton = new Button(null,
            new Vector2(window.Width * 0.45f, window.Height * 0.52f),
            Texture2D.FromFile(graphicsDevice, "Images/MenuTexture/ExitButton.png"), Scale);

        _spriteBatch = spriteBatch;
        _backGround = Texture2D.FromFile(graphicsDevice, "Images/Player/Screenshot_6.png");
    }
    
    public State UpdateMenu()
    {
        if (_startButton.IsPressed(Mouse.GetState().Position))
            return State.Game;
    
        if (_exitButton.IsPressed(Mouse.GetState().Position))
            return State.EndGame;

        return State.Menu;
    }
    
    public void DrawMenu()
    {
        _spriteBatch.Draw(_backGround, new Vector2(-1, 0), Color.Purple);
        _startButton.Draw(_spriteBatch);
        _exitButton.Draw(_spriteBatch);
    }
}