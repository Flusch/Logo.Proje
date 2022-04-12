using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Logo.Proje.Domain.Entities
{
    public class CustomIdentityUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long identityNo { get; set; }
        public bool hasCar { get; set; }
        public string carPlate { get; set; }
    }
}
