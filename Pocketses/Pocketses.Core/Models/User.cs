using Microsoft.EntityFrameworkCore;
using Pocketses.Core.Models.Base;

namespace Pocketses.Core.Models
{
    [Index(nameof(GoogleSubject),IsUnique =true)]
    public sealed class User : AuditedEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string GoogleSubject { get; set; }
    }
}
