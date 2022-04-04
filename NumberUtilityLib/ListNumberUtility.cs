using System.Collections.Generic;
using System.Linq;

namespace NumberUtilityLib
{
    public class ListNumberUtility
    {
        /// <summary>
        /// Move k lagest number to list 's head, remain list keep order as input   
        /// </summary>
        public static List<int> KLargestTop(List<int> list, int k)
        {
            if (k < 1)
            {
                // no need move  
                return list;
            }

            int n = list.Count;
            if (k >= n)
            {
                // no need move  
                return list;
            }

            // Creating priority list or give list  with only k elements
            var priorityList = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < k; i++)
            {
                priorityList.Add(new KeyValuePair<int, int>(i, list[i]));
            }

            KeyValuePair<int, int> minValueItem = new KeyValuePair<int, int>();
            bool hasValue;

            // Loop For each element in list after the kth element
            for (int i = k; i < n; i++)
            {
                // find priority list's minimum
                hasValue = false;
                foreach (var x in priorityList)
                {
                    if (hasValue)
                    {
                        if (x.Value < minValueItem.Value) minValueItem = x;
                    }
                    else
                    {
                        minValueItem = x;
                        hasValue = true;
                    }
                }

                // If current element is smaller than minimum, do nothing
                // and continue to next element
                if (list[i] <= minValueItem.Value)
                    continue;
                // Otherwise change minimum element to current element 
                else
                {
                    priorityList.Remove(minValueItem);
                    priorityList.Add(new KeyValuePair<int, int>(i, list[i]));
                }
            }

            var priorityIndex = priorityList.Select(x => x.Key).OrderByDescending(x => x).ToList();
            // maximmum priorityIndex smaller than k 
            if (priorityIndex[0] < k)
            {
                // No need move, k lasgest element at head 
                return list;
            }

            // remove priority items in list after kth position 
            for (int i = 0; i < k; i++)
            {

                var lastIndex = priorityIndex[i] + i;
                var firstIndex = i + 1 < k ? (priorityIndex[i + 1] + i) : k - 1;

                // remove item by move previous item to i+ 1 possiton 
                for (int j = lastIndex; j > firstIndex; j--)
                {
                    list[j] = list[j - i - 1];
                }
            }

            for (int i = 0; i < k; i++)
            {
                list[i] = priorityList[i].Value;
            }

            return list;
        }
    }
}
