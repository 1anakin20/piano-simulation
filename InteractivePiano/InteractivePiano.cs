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
        private readonly PianoAudio _audio;
        private const int SampleRate = 44100;
        private const int Repetitions = 3;
        private List<Keys> _pressedKeys;
        private readonly KeysEvents _keysEvents;
        private readonly KeyboardPiano _keyboardPiano;

        public InteractivePiano()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _pressedKeys = new List<Keys>();
            var piano = new Piano(Keys.Length, SampleRate);
            _audio = new PianoAudio(piano, SampleRate);
            _keyboardPiano = new KeyboardPiano(_audio, Keys);
            _keysEvents = new KeysEvents();
            _keysEvents.KeyPressed += OnKeyPressed;
            _keysEvents.KeyReleased += OnKeyReleased;
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

            _keysEvents.Update();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void OnKeyPressed(object sender, KeysEventArgs e)
        {
            foreach (var key in e.Keys)
            {
                _keyboardPiano.StrikeKey(char.ToLower((char)key));
            }
        }

        private void OnKeyReleased(object sender, KeysEventArgs e)
        {
            foreach (var key in e.Keys)
            {
                _keyboardPiano.RaiseKey(char.ToLower((char)key));
            }
        }
    }
}