using System;

namespace InteractivePiano.PianoInput
{
    public abstract class PianoInput
    {
        public event EventHandler<PianoInputEventArgs> PianoKeyPressed;
        public event EventHandler<PianoInputEventArgs> PianoKeyReleased;

        protected virtual void OnPianoKeyPressed(PianoInputEventArgs e)
        {
            PianoKeyPressed?.Invoke(this, e);
        }

        protected virtual void OnPianoKeyReleased(PianoInputEventArgs e)
        {
            PianoKeyReleased?.Invoke(this, e);
        }
    }

    public class PianoInputEventArgs : EventArgs
    {
        public int[] Keys { get; }

        public PianoInputEventArgs(int[] keys)
        {
            Keys = keys;
        }
    }
}