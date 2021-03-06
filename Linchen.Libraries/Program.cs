﻿using Linchen.Libraries.DAL;
using Linchen.Libraries.Factory;
using Linchen.Libraries.IDAL;
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
                IBaseDAL baseDAL = DALFactory.CreateInstance();
                //BaseDAL baseDAL = new BaseDAL();
                //Company company = baseDAL.Find<Company>(6);
                //string s1 = company.Name;
                //Console.WriteLine(s1);
                //List<Company> c1 = baseDAL.FindAll<Company>();
                //foreach (var item in c1)
                //{
                //    Console.WriteLine(item.Name);
                //}

                User user = baseDAL.Find<User>(1);
                List<User> list = baseDAL.FindAll<User>();

            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
