using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dinolab
{
    public class BookingList
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("ItemList")]
        public int EqId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int Time { get; set; }
    }
}