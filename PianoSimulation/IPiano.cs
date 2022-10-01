using System.Collections.Generic;

namespace PianoSimulation
{
    public interface IPiano
    {
        /// <summary>
        /// Strikes the piano key (wire) corresponding to the specified character
        /// </summary>
        /// <param name="key">The charcter associated with a note</param>
        public int StrikeKey(int key);

        /// <summary>
        /// Raises the piano key (wire) corresponding to the specified character
        /// </summary>
        /// <param name="key">The character associated with the key</param>
        /// <returns></returns>
        public int RaiseKey(int key);

        /// <summary>
        /// Plays all of the vibrating keys (wires) at the current time step.
        /// </summary>
        /// <returns>Returns the combined harmonic result.</returns>
        public double Play();

        /// <summary>
        /// List containing a string descibring all the wires in the piano with their key and note frequency
        /// </summary>
        /// <returns></returns>
        public List<string> GetPianoKeys();
    }
}