using InteractivePiano.Audio;
using InteractivePiano.PianoInput;
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
        private readonly KeyboardKeysEvents _keyboardKeysEvents;
        private readonly PianoInput.PianoInput _pianoInput;

        public InteractivePiano()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            var piano = new Piano(Keys.Length, SampleRate);
            _audio = new PianoAudio(piano, SampleRate);
            _keyboardKeysEvents = new KeyboardKeysEvents();
            _pianoInput = new PianoInput.KeyboardPiano(Keys, _keyboardKeysEvents);
            _pianoInput.PianoKeyPressed += OnPianoInputOnPianoInputKeyPressed;
            _pianoInput.PianoKeyReleased += OnPianoInputOnPianoInputKeyReleased;
        }

        private void OnPianoInputOnPianoInputKeyReleased(object sender, PianoInputEventArgs args)
        {
            foreach (var key in args.Keys)
            {
                _audio.RemoveNote(key);
            }
        }

        private void OnPianoInputOnPianoInputKeyPressed(object sender, PianoInputEventArgs args)
        {
            foreach (var key in args.Keys)
            {
                _audio.AddNote(key);
            }
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

            // It is only necessary to get the keyboard inputs if we are playing the piano with a computer keyboard
            if (_pianoInput is PianoInput.KeyboardPiano)
            {
                _keyboardKeysEvents.Update();
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