namespace TestClassLibrary
{
    public class SimpleContainer<T>
    {
        public T Instance { get; set; }
    }

    public class DeepContainer<T>:SimpleContainer<DeepContainer<SimpleContainer<T>>>
    {
    }

    public class Class1
    {
        private readonly string _test;

        public Class1()
        {
            _test = "";
            Hello.TestOverload("one","two");
        }
    }

    public class Hello
    {

        public static void Main(string[] args)
        {
            var obj = new DeepContainer<string>();
            //Animal a = Activator.CreateInstance<Animal>();

            var z = new Class1();
            Console.WriteLine("Hit enter to allocate");
            Console.ReadKey();
            byte[] data = new byte[1024 * 1024]; //Memory still 7.1 MB
            Console.WriteLine("Hit Enter to Fill with zeros!");
            Console.ReadKey();
            Array.Clear(data, 0, data.Length); //Memory now 8.1 MB
            Console.WriteLine("1MB filled, hit enter to exit");
            Console.ReadKey();
        }

        public static void TestOverload(string s1)
        {
        }
        public static void TestOverload(string s1, string s2)
        {
        }
        public static void TestOverload(string s1, string s2, string s3)
        {
        }
        public static void TestOverload(string s1, string s2, string s3, string s4)
        {
        }
    }
}