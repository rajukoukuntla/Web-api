using Employeservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Employeservice.Controllers
{

    //route prefix

    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        static List<student> student = new List<student>()
        {

            new student() {id= 1 , Name="raj" },
            new student() {id= 2 , Name="raj1" },
            new student() {id= 3 , Name="raj2" }
        };

        //override of route prefix using (~) symbol
       [Route("~/api/teacher")]
        public IEnumerable<Teacher> getteacher()
        {
            List<Teacher> teacher = new List<Teacher>()
            {
                new Teacher() { id = 1, Name = "raj" },
                new Teacher() { id = 2, Name = "raj1" },
                new Teacher() { id = 3, Name = "raj2" }
            };
            return teacher;
        } 

        [Route("")]
        public IEnumerable<student> get()
        {
            return student;
        }

        //route with route prefix and constaints
        [Route("{id:int}")]
        public student get(int id)
        {
            return student.FirstOrDefault(s => s.id == id);
        }

        [Route("{name:alpha}")]
        public student get(string name)
        {
            return student.FirstOrDefault(s => s.Name.ToLower() == name.ToLower());
        }

        [Route("{id}/courses")]
        public IEnumerable<string> getstudentcources(int id)
        {
            if (id == 1)
                return new List<string>() { "c#", "Asp.net", "sql server" };
            else if (id == 2)
                return new List<string>() { "c#", "Asp.net mvc", "sql server 2012" };
            else
                return new List<string>() { "java scripts", "Asp.net web forms", "sql serve 2016r" };

        }
    }
}
