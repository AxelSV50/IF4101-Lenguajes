using Microsoft.AspNetCore.Mvc;
using SandboxProf.Models.Domain;

namespace SandboxProf.Controllers
{
    public class StudentController : ControllerBase
    {
        // GET: api/<StudentsController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            IEnumerable<Student> students = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7192/api/Student/");
                    var responseTask = client.GetAsync("GetStudents");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        /*
                        var readTask = result.Content.ReadAsAsync<IList<Student>>();
                        readTask.Wait();
                        //lee los estudiantes provenientes de la API
                        students = readTask.Result;
                        */
                    }
                    else
                    {
                        students = Enumerable.Empty<Student>();
                    }
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact an administrator");
            }

            return students;
        }

        [HttpGet]
        public Student GetById(int id)
        {
            Student student = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7192/api/Student/GetStudent/" + id);
                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    /*
                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();
                    //lee el estudiante provenientes de la API
                    student = readTask.Result;
                    */

                    }
                }

            return student;

        }


        // POST api/<StudentsController>
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7192/api/Student/");

                var postTask = client.PostAsJsonAsync("PostStudent", student);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return new JsonResult(result);
                    // TODO: return new JsonResult(student);
                }
                else
                {
                    // TODO should be customized to meet the client's needs
                    return new JsonResult(result);
                }
            }
        }


        // PUT api/<StudentsController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Student student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7192/api/Student/");
                var putTask = client.PutAsJsonAsync("PutStudent/" + student.Id, student);
                putTask.Wait();

                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return new JsonResult(result);
                    // TODO: return new JsonResult(student);
                }
                else
                {
                    // TODO should be customized to meet the client's needs
                    return new JsonResult(result);
                }
            }
        }



        // DELETE api/<StudentsController>/5
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7192/api/Student/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("DeleteStudent/" + id.ToString());
                //deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return new JsonResult(result);
                }
                else
                {
                    //camino del error
                    return new JsonResult(result);

                }
            }


        }


    }
}
