using System.ComponentModel.DataAnnotations;

namespace Dinolab.Data
{
    public class ItemList
    {
        [Key]
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int Amount { get; set; }
        public int LabId { get; set; }
    }
}