using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberUtilityLib;

namespace UnitTest
{
    [TestClass]
    public class ListNumberUtilityTest
    {
        public const int K_FACTOR = 10;// numer of elememnt need move to list 's head  
        public const int NORMAL_SIZE = 200;
        public const int LARGE_SIZE = 10000;

        [TestMethod]
        public void KSmallerThanOneTest()
        {
            List<int> list = RandomList(NORMAL_SIZE);
            List<int> expectedList = new List<int> { };
            expectedList.AddRange(list);

            list = ListNumberUtility.KLargestTop(list, 0);

            CollectionAssert.AreEqual(expectedList, list);
        }

        [TestMethod]
        public void KLargerOrEqualsListSizeTest()
        {
            List<int> list = RandomList(NORMAL_SIZE);
            List<int> expectedList = new List<int> { };
            expectedList.AddRange(list);

            list = ListNumberUtility.KLargestTop(list, NORMAL_SIZE + 1);
            CollectionAssert.AreEqual(expectedList, list);

            list = ListNumberUtility.KLargestTop(list, NORMAL_SIZE);
            CollectionAssert.AreEqual(expectedList, list);
        }


        [TestMethod]
        public void SmallListTest()
        {
            List<int> list = new List<int> { 3, 2, 15, 4, 5, 6, 7, 8, 9, 10, 11, 11, 12, 13, 14 };
            list = ListNumberUtility.KLargestTop(list, K_FACTOR);
            List<int> expectedList = new List<int> { 15, 7, 8, 9, 10, 11, 11, 12, 13, 14, 3, 2, 4, 5, 6 };
            CollectionAssert.AreEqual(expectedList, list);

            list =  new List<int> {  4, 5, 3 , 2 ,  6, 7, 8, 9, 10, 15 , 11, 11, 12, 13, 14 };
            list = ListNumberUtility.KLargestTop(list, K_FACTOR);
            expectedList = new List<int> { 7, 8, 9, 10, 15, 11, 11, 12, 13, 14, 4, 5, 3 , 2 , 6 };
            CollectionAssert.AreEqual(expectedList, list);

            list = new List<int> { 11, 12, 13, 14 , 4, 5, 3, 2, 6, 7, 8, 9, 10, 15, 11 };
            list = ListNumberUtility.KLargestTop(list, K_FACTOR);
            expectedList = new List<int> { 11, 12, 13, 14,  7, 8, 9, 10, 15, 11, 4, 5, 3, 2, 6 };
            CollectionAssert.AreEqual(expectedList, list);

        }

        [TestMethod]
        public void NormalListTest()
        {
            NotOrderListTest(NORMAL_SIZE);
        }

        [TestMethod]
        public void LargeListTest()
        {
            NotOrderListTest(LARGE_SIZE);
        }

        [TestMethod]
        public void VeryLargeListTest()
        {
            NotOrderListTest(LARGE_SIZE * LARGE_SIZE);
        }


        [TestMethod]
        public void NormalOrderListTest()
        {
            OrderListTest(NORMAL_SIZE);
        }

        [TestMethod]
        public void LargeOrderListTest()
        {
            OrderListTest(LARGE_SIZE);
        }

        [TestMethod]
        public void VeryLargeOrderListTest()
        {
            OrderListTest(LARGE_SIZE * LARGE_SIZE);
        }

        [TestMethod]
        public void NormalOrderDescListTest()
        {
            OrderDescListTest(NORMAL_SIZE);
        }

        [TestMethod]
        public void LargeOrderDescListTest()
        {
            OrderDescListTest(LARGE_SIZE);
        }

        [TestMethod]
        public void VeryLargeOrderDescListTest()
        {
            OrderDescListTest(LARGE_SIZE * LARGE_SIZE);
        }


        private List<int> RandomList(int size, int maxValue = 1000)
        {
            var rand = new Random();
            var rtnlist = new List<int>();

            for (int i = 0; i < size; i++)
            {
                rtnlist.Add(rand.Next(maxValue));
            }
            return rtnlist;
        }


        private void NotOrderListTest(int size)
        {
            var rand = new Random();

            int baseLargestNumber = rand.Next(1000);
            int initSize = size - K_FACTOR;
            List<int> list = RandomList(initSize, baseLargestNumber);

            List<int> expectedList = new List<int> { };
            expectedList.AddRange(list);

            List<int> expectedHeadList = new List<int> { };
            // Add K_FACTOR elememnt larger than baseLargestNumber to list
            for (int i = 0; i < K_FACTOR; i++)
            {
                var value = baseLargestNumber + rand.Next();
                // Insert element larger than baseLargestNumber to list,
                // this element expect move to list's head 
                list.Insert(initSize / K_FACTOR * i, value);

                expectedHeadList.Add(value);

            }
            expectedList.InsertRange(0, expectedHeadList);

            list = ListNumberUtility.KLargestTop(list, K_FACTOR);

            CollectionAssert.AreEqual(expectedList, list);
        }


        private static void OrderListTest(int size)
        {
            Random rnd = new Random();
            int start = rnd.Next(50);

            List<int> list = Enumerable.Range(start, size).ToList();
            list = ListNumberUtility.KLargestTop(list, K_FACTOR);

            List<int> expectedList = Enumerable.Range(start + size - K_FACTOR, K_FACTOR).ToList();//K_FACTOR largest element 
            expectedList.AddRange(Enumerable.Range(start, size - K_FACTOR));
            CollectionAssert.AreEqual(expectedList, list);
        }

        private static void OrderDescListTest(int size)
        {
            Random rnd = new Random();
            int start = rnd.Next(50);
            List<int> list = Enumerable.Range(start, size).OrderByDescending(x => x).ToList();
            list = ListNumberUtility.KLargestTop(list, K_FACTOR);

            //expectedList is same input list 
            List<int> expectedList = new List<int> { };
            expectedList.AddRange(list);

            CollectionAssert.AreEqual(expectedList, list);
        }


    }
}
