using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KeyboardPiano;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PianoSimulation;

namespace InteractivePiano
{
    public class InteractivePiano : Game
    {
        private const string Keys = "q2we4r5ty7u8i9op-[=zxdcfvgbnjmk,.;/' ";
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Audio _audio;
        private Piano _piano;
        private const int SampleRate = 44100;
        private const int Repetitions = 3;
        private const int playLength = SampleRate * Repetitions;
        private readonly Object PlayLock = new Object();

        public InteractivePiano()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _audio = Audio.Instance;
            _piano = new Piano(Keys);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            var keyboard = Keyboard.GetState();

            var pressedKeys = new List<char>();
            foreach (var key in keyboard.GetPressedKeys())
            {
                var charKey = key.ToString().ToLower()[0];
                if (Keys.Contains(charKey))
                {
                    pressedKeys.Add(charKey);
                }
            }

            if (pressedKeys.Count > 0)
            {
                var keysHandler = new Thread(() => HandleKeys(pressedKeys));
                keysHandler.Start();
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void HandleKeys(IEnumerable<char> keys)
        {
            lock (PlayLock)
            {
                foreach (var key in keys)
                {
                    _piano.StrikeKey(key);
                }

                for (var i = 0; i < playLength; i++)
                {
                    _audio.Play(_piano.Play());
                }
            }
        }
    }
}