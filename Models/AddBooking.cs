using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dinolab.Models
{
    public class AddBooking
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int EqId { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        public int Time { get; set; }
    }
}