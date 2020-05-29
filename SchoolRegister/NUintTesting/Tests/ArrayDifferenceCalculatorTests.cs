using Display.Scripts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUintTesting.Tests
{
    [TestClass]
    public class ArrayDifferenceCalculatorTests
    {
        [TestMethod]
        public void ArrayDifferenceDictinaryTest()
        {
            int[] array1 = { 1, 1, 1, 2, 3, 5 };
            int[] array2 = { 1, 1, 2, 2, 2, 6 };
            Dictionary<int, int> expectedResult = new Dictionary<int, int>();
            expectedResult.Add(1, -1);
            expectedResult.Add(2, 2);
            expectedResult.Add(3, -1);
            expectedResult.Add(5, -1);
            expectedResult.Add(6, 1);

            Dictionary<int, int> result = ArrayDifferenceCalculator.GetDifferenceDictinary(array1, array2);

            try
            {
                foreach (int key in result.Keys)
                {
                    if (result[key] != expectedResult[key])
                    {
                        Assert.Fail();
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                Assert.Fail();
                throw;
            }
        }

        private void TestDictinary(Dictionary<int, int> expectedResult, Dictionary<int, int> result)
        {
            try
            {
                foreach (int key in result.Keys)
                {
                    if (result[key] != expectedResult[key])
                    {
                        Assert.Fail();
                    }
                }
            }
            catch (KeyNotFoundException)
            {
                Assert.Fail();
                throw;
            }
        }
    }
}
