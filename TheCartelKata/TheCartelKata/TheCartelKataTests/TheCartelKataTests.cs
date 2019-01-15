using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheCartelKataTests
{
    [TestClass]
    public class TheCartelKataTests
    {
        [TestMethod]
        public void LongAndVeryDescriptiveTestNameTheySaysExactlyWhatTheTestDoesTest()
        {
            // 1. Set up the expected value, called 'expected'
            var expected = 1;
            // 2. Get a result, called 'actual'
            var actual = 2;
            // 3. Assert something about 'expected' and 'actual'
            Assert.AreEqual(expected, actual);
        }

        /*
         * Create your unit tests here
         * Remember - Create the tests from the requirements, and then write the code
         */
    }
}
