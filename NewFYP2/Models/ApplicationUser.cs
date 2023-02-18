using Microsoft.AspNetCore.Identity;

namespace NewFYP2.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }
    }
}
