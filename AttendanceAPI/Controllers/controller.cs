using AttendanceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AttendanceAPI : ControllerBase
    {

        /// <summary>
        /// GET request to get all students with admin or instructor key
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>list of StudentDTO objects</returns>
        // https://localhost:7001/api/v1/AttendanceAPI/all/{key}
        // possibly write this as a post and take guid  from body
        [HttpGet("all/{key}")]
        public ActionResult<List<StudentDTO?>> GetStudents(string guid)
        {
            try
            {
                DataLayer dl = new();

                UserLevelDTO? userDTO = dl.GetUserLevelByKey(guid);
                if (userDTO == null)
                {
                    return Unauthorized($"{guid} is unauthorized to access the databasee");
                }
                if (userDTO.UserLevel is not "admin" and not "instructor")
                {
                    return Unauthorized($"{guid} is unauthorized to access the database");
                }

                List<StudentDTO> students = dl.GetStudents();
                if (students == null)
                {
                    return NotFound("No students found");
                }

                return Ok(students);

            }
            catch (Exception error)
            {
                Console.WriteLine($"Error: {error}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); //StatusCode(500);
            }

        }

        /// <summary>
        /// Gets student info by guid 
        /// </summary>
        /// <param name="guid"></param>
        [HttpGet("{key}")]
        public void GetStudentByGuid(string guid)
        {
            // implement here
            
        }

        /// <summary>
        /// POST request to add a single student with admin or instructor key
        /// </summary>
        /// <param name="studentDTO"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost("{key}")]
        public ActionResult<StudentDTO> PostAStudent([FromBody] StudentDTO studentDTO, string key)
        {
            try
            {
                DataLayer dl = new();
                UserLevelDTO? userDTO = dl.GetUserLevelByKey(key);
                if (userDTO == null)
                {
                    return Unauthorized($"{key} is unauthorized to access the database"); //status 401;
                }
                if (userDTO.UserLevel is not "admin" and not "user")
                {
                    return Unauthorized($"{key} is unauthorized to access the database"); //status 401;
                }
                StudentDTO tempStudent = new StudentDTO()
                {
                    Guid = studentDTO.Guid,
                    UserName = studentDTO.UserName
                };
                //return the article or null
                StudentDTO? student = dl.InsertStudent(tempStudent);
                if (student == null)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError); //StatusCode(500);
                }
                return Ok(student);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("The user key can not be null."); //StatusCode(400)
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
