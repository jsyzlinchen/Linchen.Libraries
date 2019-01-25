using Linchen.Libraries.DAL;
using Linchen.Libraries.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linchen.Libraries.Project
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello");
                BaseDAL baseDAL = new BaseDAL();
                Company company = baseDAL.Find<Company>(6);
                string s1 = company.Name;
                Console.WriteLine(s1);
                List<Company> c1 = baseDAL.FindAll<Company>();
                foreach (var item in c1)
                {
                    Console.WriteLine(item.Name);
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
