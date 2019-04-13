using MyPersonalProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace MyPersonalProject.TravelApp
{
    public class DataAccess
    {
        public List<LoginDomain> GetLogins(string email)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SampleDB")))
            {
               return connection.Query<LoginDomain>("select * from Login where Email = '{ email }'").ToList();
            }
        }
    }
}
