using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InteractivePiano.GameObject
{
    public class Piano
    {
        private readonly PianoKeyColor[] _keyColorPattern =
        {
            PianoKeyColor.White,
            PianoKeyColor.Black,
            PianoKeyColor.White,
            PianoKeyColor.Black,
            PianoKeyColor.White,
            PianoKeyColor.White,
            PianoKeyColor.Black,
            PianoKeyColor.White,
            PianoKeyColor.Black,
            PianoKeyColor.White,
            PianoKeyColor.Black,
            PianoKeyColor.White
        };

        PianoKey[] _keys;

        public Piano(SpriteBatch spriteBatch, Texture2D whiteKeyTexture, Texture2D blackKeyTexture,
            Vector2 startingPosition, int numberKeys, int startingKey, Color pressedColor)
        {
            // White keys need to be drawn first, so black keys can go on top of them
            _keys = new PianoKey[numberKeys];
            Vector2 currentWhiteKeyPosition = startingPosition;

            for (int i = 0; i < numberKeys; i++)
            {
                var keyColor = _keyColorPattern[(i + startingKey) % _keyColorPattern.Length];
                if (keyColor == PianoKeyColor.White)
                {
                    _keys[i] = new PianoKey(spriteBatch, whiteKeyTexture, currentWhiteKeyPosition, pressedColor);
                    currentWhiteKeyPosition += new Vector2(whiteKeyTexture.Width, 0);
                }
            }

            for (int i = 0; i < numberKeys; i++)
            {
                if (_keys[i] == null)
                {
                    var position = _keys[i - 1].Position +
                                   new Vector2(
                                       (int)Math.Ceiling(whiteKeyTexture.Width / 2d) + blackKeyTexture.Width / 2, 0);
                    _keys[i] = new PianoKey(spriteBatch, blackKeyTexture, position, pressedColor);
                }
            }
        }

        public Piano(SpriteBatch spriteBatch, Texture2D whiteKeyTexture, Texture2D blackKeyTexture,
            Vector2 startingPosition, int numberKeys, int startingKey) : this(spriteBatch, whiteKeyTexture,
            blackKeyTexture, startingPosition, numberKeys, startingKey, Color.Red)
        {
        }

        public void DrawKeys()
        {
            foreach (var key in _keys)
            {
                key.Draw();
            }
        }

        public void PressKey(int key)
        {
            _keys[key].PressKey();
        }

        public void ReleaseKey(int key)
        {
            _keys[key].ReleaseKey();
        }
    }
}