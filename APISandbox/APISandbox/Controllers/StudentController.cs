using APISandbox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APISandbox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly If4101B97683Context _context;

        public StudentController(If4101B97683Context context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            //return await _context.Students.ToListAsync();

            //Include es como especie de JOIN
            return await _context.Students.Include(s => s.Nationality).Select(studentItem => new Student
            {
                Id = studentItem.Id,
                Name = studentItem.Name,
                Email = studentItem.Email,
                Nationality = studentItem.Nationality

            }).ToListAsync();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            //  var student = await _context.Students.FindAsync(id);

            //  var studentFound =  _context.Students.Where(e => e.Id == id).Include(e => e.Nationality);

            var student = await _context.Students.Include(s => s.Nationality)
            .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }



        // PUT: api/Student/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("[action]/{id}")]

        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Student
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {

            //TODO reconstruir el objeto estudiante y agregar los atributos faltantes. 
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Student/5
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }


        //----------------------------------------------\\

        [Route("[action]")]
        [HttpGet]
        public IActionResult GetAllStudentsSP()
        {
            var students = _context.Students
                             .FromSqlRaw($"GetAllStudentsSP")
                             .AsEnumerable();

            return Ok(students);
        }

        [Route("[action]/{email}")]
        [HttpGet]
        public IActionResult GetStudentSP(string email)
        {

            var studentEmail = new SqlParameter("@Email", email);
            var student = _context.Students
                           .FromSqlRaw($"GetStudentByEmailSP @Email", studentEmail)
                           .AsEnumerable().Single();

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }



        [Route("[action]")]
        [HttpPut]
        public IActionResult PutStudentSP(Student student)
        {

            var result = _context.Database.ExecuteSqlRaw("UpdateStudentSP {0}, {1}, {2}, {3}, {4}",
                                student.Id,
                                student.Name,
                                student.Email,
                                student.Password,
                                student.NationalityId);
            if (result == 0)
            {
                return null;
            }

            return Ok(result);
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult PostStudentSP(Student student)
        {

            var result = _context.Database.ExecuteSqlRaw("InsertStudentSP {0}, {1}, {2}, {3}, {4}",
                                student.Name,
                                student.Email,
                                student.Password,
                                student.NationalityId);
            if (result == 0)
            {
                return null;
            }

            return Ok(result);

        }

        [Route("[action]/{email}")]
        [HttpDelete]

        public IActionResult DeleteStudentSP(string email)
        {
            var result = _context.Database.ExecuteSqlRaw("DeleteStudentSP {0}", email);
            if (result == 0)
            {
                return null;
            }

            return Ok(result);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudentsView()
        {
            var allStudents = await _context.GetAllStudentsViews.ToListAsync();

            return Ok(allStudents);
        }
    }


}
