using System.ComponentModel.DataAnnotations;

namespace Dinolab
{
    public class BookingList
    {
        [Key] 
        public string BookId { get; set; }
        public string UserId { get; set; }
        public string EqId { get; set; }
        public int Amount { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}