using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace Employeservice
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (testapiEntities entites = new testapiEntities())
            {
                return entites.users.Any(user => user.username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.password == password);

            }
        }
    }
}