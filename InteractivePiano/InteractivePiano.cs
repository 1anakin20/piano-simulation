using System;
using System.Collections.Generic;
using InteractivePiano.Audio;
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
        private PianoAudio _audio;
        private Piano _piano;
        private const int SampleRate = 44100;
        private const int Repetitions = 3;
        private const int playLength = SampleRate * Repetitions;
        private readonly Object PlayLock = new Object();
        private List<Keys> _keysPressed;

        public InteractivePiano()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _keysPressed = new List<Keys>();
            _piano = new Piano(Keys, SampleRate);
            _audio = new PianoAudio(_piano, SampleRate);
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
            foreach (var key in Enum.GetValues(typeof(Keys)))
            {
                if (keyboard.IsKeyDown((Keys)key))
                {
                    _audio.AddNote(key.ToString()!.ToLower()[0]);
                }
                else
                {
                    _audio.RemoveNote(key.ToString()!.ToLower()[0]);
                }
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
    }
}