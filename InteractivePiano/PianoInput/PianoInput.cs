using System;

namespace InteractivePiano.PianoInput
{
    /// <summary>
    /// Base class to create a new way to play the piano
    /// This class has two events: <see cref="PianoKeyPressed"/> and <see cref="PianoKeyReleased"/>. Press piano keys and one to release pressed keys.
    /// </summary>
    public abstract class PianoInput
    {
        /// <summary>
        /// Event that notifies only newly pressed keys
        /// </summary>
        public event EventHandler<PianoInputEventArgs> PianoKeyPressed;

        /// <summary>
        /// Event that notifies only newly released keys
        /// </summary>
        public event EventHandler<PianoInputEventArgs> PianoKeyReleased;

        /// <summary>
        /// Invokes the <see cref="PianoKeyPressed"/> event
        /// </summary>
        /// <param name="e"><see cref="PianoInputEventArgs"/> Event arguments</param>
        protected void OnPianoKeyPressed(PianoInputEventArgs e)
        {
            PianoKeyPressed?.Invoke(this, e);
        }

        /// <summary>
        /// Invokes the <see cref="PianoKeyReleased"/> event
        /// </summary>
        /// <param name="e"><see cref="PianoInputEventArgs"/> Event arguments</param>
        protected void OnPianoKeyReleased(PianoInputEventArgs e)
        {
            PianoKeyReleased?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Arguments for the <see cref="PianoInput"/> events
    /// </summary>
    public class PianoInputEventArgs
    {
        /// <summary>
        /// Corresponding piano key numbers of newly pressed or released keys
        /// Note numbers start from 0
        /// </summary>
        public int[] Keys { get; }

        /// <summary>
        ///  Constructs a new <see cref="PianoInputEventArgs"/> instance
        /// </summary>
        /// <param name="keys">Array of newly pressed or released keys</param>
        public PianoInputEventArgs(int[] keys)
        {
            Keys = keys;
        }
    }
}