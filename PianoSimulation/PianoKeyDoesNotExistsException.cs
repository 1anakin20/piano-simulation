using System;

namespace PianoSimulation
{
    public class PianoKeyDoesNotExistsException : Exception
    {
        public PianoKeyDoesNotExistsException(string message) : base(message)
        {
        }
    }
}