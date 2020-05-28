using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace Employeservice.Controllers
{
    [Authorize]
    public class EmployeedbController : ApiController
    {
        public IEnumerable<employee> get()
        {
            using(testapiEntities entity = new testapiEntities())
            {
                return entity.employees.ToList();
            }
        }
    }
}
