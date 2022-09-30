using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PianoSimulation;

namespace PianoSimulationTests
{
    [TestClass]
    public class PianoTest
    {
        [TestMethod]
        public void TestPiano()
        {
            var pianoSimulation = new Piano();
        }

        [TestMethod]
        public void TestPianoPlay()
        {
            var pianoSimulation = new Piano();
            pianoSimulation.StrikeKey('q');
            pianoSimulation.StrikeKey('w');
            pianoSimulation.StrikeKey('e');
            pianoSimulation.StrikeKey('r');
            var sum = pianoSimulation.Play();
        }
    }
}