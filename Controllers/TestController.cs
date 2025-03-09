using labITP.Data;
using labITP.DTO.CourseDto;
using labITP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace labITP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ApplDbContext db;

        public TestController(ApplDbContext _db)
        {
            db = _db;
        }

        // Get all courses
        [HttpGet ("get courses")]
        public IActionResult Get()
        {
           List<Course>crs = db.Courses.ToList();

            List<ReadCourseDto>crsDto= new List<ReadCourseDto>();
            foreach(var item in crs)
            {
                ReadCourseDto csDto = new ReadCourseDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Duration = item.Duration
                };
                crsDto.Add(csDto);
            }

            if (!crs.Any())
                return NotFound();

            return Ok(crsDto);
        } 

        // Get course by ID
        [HttpGet("{id} get by id")] 
        public IActionResult GetById(int id)
        {
            var course = db.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null) return NotFound();
            // d2
            ReadCourseDto csDto = new ReadCourseDto()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Duration = course.Duration


            };
            return Ok(csDto);
        }

        //deleteCourse(id) :

        [HttpDelete("{id} delete course")]
         public IActionResult DeleteCourse(int id)
          {
           var course = db.Courses.FirstOrDefault(c => c.Id == id);

             if (course == null)
                return NotFound();
           
             db.Courses.Remove(course);
             db.SaveChanges();  

           var courses = db.Courses.ToList();  
             return Ok(courses);  
         }

            // course by name :
            [HttpGet("{name :alpha}  get by name")]
        public IActionResult courseByName(string name)
        {
            Course crsName = db.Courses.Where(c => c.Name == name).FirstOrDefault(); 
            List<ReadCourseDto> crsDto = new List<ReadCourseDto>();
             
                ReadCourseDto csDto = new ReadCourseDto()
                {
                    Id = crsName.Id,
                    Name = crsName.Name,
                    Description = crsName.Description,
                    Duration = crsName.Duration
                };
                crsDto.Add(csDto);
            

            if (crsName == null)
                return NotFound();

             return Ok(crsName);
        }

            // add course :
        [HttpPost("add course")]
        public IActionResult add(AddCourseDto courseDTO)
        {
              if(courseDTO == null)
                  return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Course crss = new Course()
            {
                Name = courseDTO.Name,
                Description = courseDTO.Description,
                Duration = courseDTO.Duration
            };
               db.Courses.Add(crss);
               db.SaveChanges();
               return CreatedAtAction("GetById", new {id=crss.Id},courseDTO); 



        }

            // update course :
        [HttpPut("{id}  update course")]
            public IActionResult update(int id, Course course)
            {
                  if(course == null)
                     return BadRequest();
                   if(id!=course.Id)
                      return BadRequest();
                db.Entry(course).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                     return NoContent();

            }

    }
}
