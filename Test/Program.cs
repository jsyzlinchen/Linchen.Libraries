using System;

namespace Test
{
    #region $ @ format
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Hello World!");
    //        var k = "a";
    //        var a0 = "User";
    //        var a1 = "Id";
    //        var a2 = 5;
    //        var cc1 = string.Format("select * from {0} where {1} = {2}", a0, a1, a2);
    //        var cc2 = $"select * from {a0} where {a1}={a2}";
    //        var cc3 = @"select * from {a0} where {a1}={a2}";
    //        Console.WriteLine(cc1);
    //        Console.WriteLine(cc2);
    //        Console.WriteLine(cc3);
    //        //总结  $就是string.Format    功能一样 可使用占位符
    //        //@ 就是死的东西
    //    }
    //}
    #endregion

    #region   多播委托
    //class Program
    //{
    //    public delegate void TestAB(int i);
    //    static void Iwork(TestAB a)
    //    {
    //        if (a != null)
    //            for (int i = 0; i <= 10; i++)
    //            {
    //                a(i * 10);
    //                System.Threading.Thread.Sleep(100);
    //            }
    //    }
    //    static void Main(string[] args)
    //    {
    //        TestAB T1 = TestA;
    //        T1 += TestB;
    //        Iwork(T1);
    //        Console.WriteLine("Done.");
    //    }
    //    static void TestA(int a)
    //    {
    //        System.IO.File.AppendAllText("1.txt", a + "%" + "\r\n");
    //    }
    //    static void TestB(int b)
    //    {
    //        Console.WriteLine(b + "%");
    //    }
    //}
    #endregion

    #region  泛型委托
    //class B 
    //{
    //    public delegate T work<T>(T t);

    //    public static int Workint(int i)
    //    {
    //        i = i * 2;
    //        return i;
    //    }
    //    public static string Workstring(string s)
    //    {
    //        s = s + "1";
    //        return s;
    //    }
    //    static void Main(string[] args)
    //    {
    //        int[] number = { 2, 4, 6, 8 };
    //        Change(number, Workint);
    //        foreach (int i in number)
    //            Console.Write(i + " ");

    //        string[] s = { "a", "b", "c", "d" };
    //        Change(s, Workstring);
    //        for (int i = 0; i < s.Length; i++)
    //        {
    //            Console.Write(s[i]+ " ");
    //        }
    //        Console.ReadKey();
    //    }
    //    public static void Change<T>(T[] number, work<T> t)
    //    {
    //        for (int i = 0; i < number.Length; i++)
    //            number[i] = t(number[i]);
    //    }
    //}
    #endregion

    #region action func 官方泛型
    //class C {
    //    public static int Workint(int i)
    //    {
    //        i = i * 2;
    //        return i;
    //    }
    //    public static string Workstring(string s)
    //    {
    //        s = s + "1";
    //        return s;
    //    }
    //    static void Main(string[] args)
    //    {
    //        int[] number = { 2, 4, 6, 8 };
    //        Change(number, Workint);
    //        foreach (int i in number)
    //            Console.Write(i + " ");

    //        string[] s = { "a", "b", "c", "d" };
    //        Change(s, Workstring);
    //        for (int i = 0; i < s.Length; i++)
    //        {
    //            Console.Write(s[i] + " ");
    //        }
    //        Console.ReadKey();
    //    }
    //    public static void Change<T>(T[] number, Func<T,T> t)
    //    {
    //        for (int i = 0; i < number.Length; i++)
    //            number[i] = t(number[i]);
    //    }
    //}
    #endregion


    public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);
    public class IPhone6
    {
        public event PriceChangedHandler PriceChanged;
    }
}
