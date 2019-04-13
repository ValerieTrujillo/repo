using Dapper;
using MyPersonalProject.Models.Domain;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace MyPersonalProject.TravelApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = Helper.CnnVal("DefaultConnection");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Successfully opened a SQL connection!!!!");
                var list = conn.Query<LoginDomain>("select * from Login where Email = '{ email }'").ToList();
                if (list.Count > 0)
                {
                     Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Oops!!! Something went Wrong!!");
            }
            conn.Close();
            conn.Dispose();
            Console.Read();
        }
    }
}
