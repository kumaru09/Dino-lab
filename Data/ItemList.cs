using System.ComponentModel.DataAnnotations;

namespace Dinolab.Data
{
    public class ItemList
    {
        [Key]
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int amount { get; set; }
        public int labId { get; set; }
        public string imgItemSrc { get; set; }
    }
}