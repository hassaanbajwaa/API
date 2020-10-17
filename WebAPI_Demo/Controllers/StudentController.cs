using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_Demo.Models;

namespace WebAPI_Demo.Controllers
{
    public class StudentController : ApiController
    {
        public IEnumerable<Student> GetEmployees()
        {
            using (StudentEntities db = new StudentEntities())
            {
                return db.Students.ToList();
            }
        }
        public HttpResponseMessage GetStudentById(int id)
        {
            using (StudentEntities db = new StudentEntities())
            {
                var entity = db.Students.FirstOrDefault(e => e.ID == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student with id=" + id.ToString() + " not found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
        }
        public HttpResponseMessage Post([FromBody] Student employee)
        {
            try
            {
                using (StudentEntities db = new StudentEntities())
                {
                    db.Students.Add(employee);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            using (StudentEntities db = new StudentEntities())
            {
                var entity = db.Students.FirstOrDefault(e => e.ID == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student with id=" + id.ToString() + "not found to delete");
                }
                else
                {
                    db.Students.Remove(entity);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
        public HttpResponseMessage Put(int id, [FromBody] Student employee)
        {
            using (StudentEntities db = new StudentEntities())
            {
                try
                {
                    var entity = db.Students.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student with id=" + id.ToString() + "not found to update");
                    }
                    else
                    {
                        entity.Name = employee.Name;
                       
                        entity.Gender = employee.Gender;
                        entity.Age = employee.Age;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
}
