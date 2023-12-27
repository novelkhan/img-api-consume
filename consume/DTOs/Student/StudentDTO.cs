using System.ComponentModel.DataAnnotations.Schema;

namespace consume.DTOs.Student
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Roll { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
