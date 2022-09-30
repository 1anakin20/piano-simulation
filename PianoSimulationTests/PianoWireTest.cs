using Microsoft.VisualStudio.TestTools.UnitTesting;
using PianoSimulation;

namespace PianoSimulationTests
{
    [TestClass]
    public class PianoWireTest
    {
        [TestMethod]
        public void TestPianoWire()
        {
            var pianoWire = new PianoWire(256, 44100);
            pianoWire.Strike();
            pianoWire.Sample();
        }
    }
}