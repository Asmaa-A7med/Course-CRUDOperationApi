using System.ComponentModel.DataAnnotations;

namespace labITP.DTO.CourseDto
{
    public class AddCourseDto
    {
        [Required]
        public string Name {  get; set; }

        public string Description { get; set; }

        public int Duration {  get; set; }
    }
}
