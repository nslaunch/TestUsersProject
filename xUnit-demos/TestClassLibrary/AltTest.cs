using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestClassLibrary
{
    public abstract class Animal
    {
        public Animal()
        {
            
        }

        public abstract void Walk();
    }

    public class Item
    {
        public string Name { get; set; }
        public int Size { get; set; }


        public void ItemMethod()
        {
            // Code Smell
            string result = "";
            for (int i = 0; i < 1000; i++)
            {
                result += i.ToString();
            }
        }

    } 

    public class ItemTest
    {
        public void Test()
        {
            Item item = new Item(){ Name = "Test1", Size = 12};
            Item item2 = new Item() { Name = "Test2", Size = 20 };
        }

        public int SwitchExpressionMethod(string name)
        {
            switch (name)
            {
                case "first":
                    return 1;
                case "second":
                    return 2;
                case "third":
                    return 3;
                case "fourth":
                    return 4;
                default:
                    return 0;
            }
        } 

        public void ProcessNumbers()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int c = 1;
            for(int i = 0; i < numbers.Length; i++)
            {
                c = c * numbers[i];
            }
        }
    }
}
