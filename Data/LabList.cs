using System.ComponentModel.DataAnnotations;

namespace Dinolab
{
    public class LabList
    {
        [Key] 
        public string LabId { get; set; }
        public string LabName { get; set; }
    }
}