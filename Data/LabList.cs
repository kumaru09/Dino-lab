using System.ComponentModel.DataAnnotations;

namespace Dinolab
{
    public class LabList
    {
        [Key]
        [Required]
        public int LabId { get; set; }
        public string LabName { get; set; }
    }
}