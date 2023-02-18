using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NewFYP2.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Venue { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerEmail { get; set; }
        public string OrganizerPhoneNo { get; set; }
        public string EventDescription { get; set; }
    }
}
