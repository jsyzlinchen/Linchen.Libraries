using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var k = "a";
            var a0 = "User";
            var a1 = "Id";
            var a2 = 5;
            var cc1 = string.Format("select * from {0} where {1} = {2}", a0, a1, a2);
            var cc2 = $"select * from {a0} where {a1}={a2}";
            var cc3 = @"select * from {a0} where {a1}={a2}";
            Console.WriteLine(cc1);
            Console.WriteLine(cc2);
            Console.WriteLine(cc3);
            //总结  $就是string.Format    功能一样 可使用占位符
            //@ 就是死的东西

        }
    }
}
