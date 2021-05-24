using System.ComponentModel.DataAnnotations;

namespace Dinolab.Models
{
    public class EditItem
    {
        [Required]
        public int itemId { get; set; }
        [Required]
        public string itemName { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int LabId { get; set; }
    }
}