using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Character;

namespace MonoGame.Hood;

public class GameHood
{
    private SpriteFont _font;
    private Player _player;
    private SpriteBatch _spriteBatch;

    public GameHood(SpriteFont font, Player player, SpriteBatch spriteBatch)
    {
        _font = font;
        _player = player;
        _spriteBatch = spriteBatch;
    }
    
    public void DrawHood()
    {
        _spriteBatch.DrawString(_font, $"Health: {_player.Health.ToString()}", 
            new Vector2(20, 20), Color.Aquamarine);
    }
}