using System.ComponentModel.DataAnnotations;

namespace Demo_BKAV
{
    public class Student
    {
        [Key]
        public int idStudent { get; set; } 
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
