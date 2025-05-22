using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq; // Required for .ToList() and .OrderBy()

namespace Linq
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void TestRangeFunction()
        {
            // Test case 1
            string input1 = "6,1-3,2-4";
            List<int> result1 = Program.Range(input1).ToList();
            List<int> expected1 = new List<int> { 1, 2, 3, 4, 6 };
            // Ensure both lists are sorted before comparison if Program.Range doesn't guarantee order
            // In this case, Program.Range already sorts and takes distinct values.
            CollectionAssert.AreEqual(expected1, result1);

            // Test case 2
            string input2 = "1-5,2-3,7";
            List<int> result2 = Program.Range(input2).ToList();
            List<int> expected2 = new List<int> { 1, 2, 3, 4, 5, 7 };
            // Ensure both lists are sorted
            CollectionAssert.AreEqual(expected2, result2);
        }
    }
}
