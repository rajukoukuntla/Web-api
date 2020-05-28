using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;
using System.Threading;
using System.Web.Http.Cors;

namespace Employeservice.Controllers
{
    [EnableCorsAttribute("*","*","*")]
    public class EmployeeController : ApiController
    {
        [BasicAuthentication]
        public HttpResponseMessage get(string gender = "All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (testapiEntities entites = new testapiEntities())
            {
                switch(username.ToLower())
                {
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, entites.employees.Where(e => e.gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entites.employees.Where(e => e.gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

        // get method with id

            // [httpget] attribute should metion if ur using custom method name insted of method name starts with get() or getsome()
        
            [HttpGet]
        public HttpResponseMessage loaddatabyid(int id)
        {
            using (testapiEntities entites = new testapiEntities())
            {
                var entity =  entites.employees.FirstOrDefault(e => e.id == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employee id is not found");
                }
            }
        }


        //post method
        public HttpResponseMessage post([FromBody]employee employee)
        {
            try {
                using (testapiEntities entites = new testapiEntities())
                {
                    entites.employees.Add(employee);
                    entites.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
      
        //  delete

        public HttpResponseMessage Delete(int id)
        {
            try
            {

                using (testapiEntities entities = new testapiEntities())
                {
                    var entity = entities.employees.FirstOrDefault(e => e.id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employrr is requested not found");
                    }
                    else
                    {
                        entities.employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // put is update to database

        public HttpResponseMessage put(int id,[FromBody]employee employee)
        {
            try
            {

                using (testapiEntities entities = new testapiEntities())
                {
                    var entity = entities.employees.FirstOrDefault(e => e.id == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employrr is requested not found");
                    }
                    else
                    {
                        entity.Firstname = employee.Firstname;
                        entity.Lastname = employee.Lastname;
                        entity.gender = employee.gender;
                        entity.age = employee.age;
                    entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
