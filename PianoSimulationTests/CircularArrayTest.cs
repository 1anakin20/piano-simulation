using Microsoft.VisualStudio.TestTools.UnitTesting;
using PianoSimulation;

namespace PianoSimulationTests
{
    [TestClass]
    public class CircularArrayTest
    {
        [TestMethod]
        public void TestCircularArrayShift()
        {
            /****************
             * First test
             * ****************/
            var circularArray1 = new CircularArray(3);
            circularArray1.Shift(1);
            circularArray1.Shift(2);
            circularArray1.Shift(3);
            
            Assert.AreEqual(1, circularArray1[0]);
            Assert.AreEqual(2, circularArray1[1]);
            Assert.AreEqual(3, circularArray1[2]);
            
            circularArray1.Shift(4);
            
            Assert.AreEqual(2, circularArray1[0]);
            Assert.AreEqual(3, circularArray1[1]);
            Assert.AreEqual(4, circularArray1[2]);

            /****************
             * Second test
             ****************/
            var circularArray2 = new CircularArray(5);
            circularArray2.Shift(3);
            circularArray2.Shift(9);
            circularArray2.Shift(27);
            circularArray2.Shift(42);
            circularArray2.Shift(0);
            
            Assert.AreEqual(3, circularArray2[0]);
            Assert.AreEqual(9, circularArray2[1]);
            Assert.AreEqual(27, circularArray2[2]);
            Assert.AreEqual(42, circularArray2[3]);
            Assert.AreEqual(0, circularArray2[4]);

            circularArray2.Shift(-0.6);
            circularArray2.Shift(3.9);
            
            Assert.AreEqual(27, circularArray2[0]);
            Assert.AreEqual(42, circularArray2[1]);
            Assert.AreEqual(0, circularArray2[2]);
            Assert.AreEqual(-0.6, circularArray2[3]);
            Assert.AreEqual(3.9, circularArray2[4]);

            circularArray2.Shift(0.256);
            circularArray2.Shift(0.69);
            
            Assert.AreEqual(0, circularArray2[0]);
            Assert.AreEqual(-0.6, circularArray2[1]);
            Assert.AreEqual(3.9, circularArray2[2]);
            Assert.AreEqual(0.256, circularArray2[3]);
            Assert.AreEqual(0.69, circularArray2[4]);

        }
    
        [TestMethod]
        public void TestCircularArraySFill() {
            var circularArray = new CircularArray(4);
            var array = new double[]{2, 3, 4, 6};
            circularArray.Fill(array);
            
            Assert.AreEqual(2, circularArray[0]);
            Assert.AreEqual(3, circularArray[1]);
            Assert.AreEqual(4, circularArray[2]);
            Assert.AreEqual(6, circularArray[3]);
        }
    }
}
