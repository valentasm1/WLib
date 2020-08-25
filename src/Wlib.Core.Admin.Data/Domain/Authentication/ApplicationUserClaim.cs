using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Wlib.Core.Admin.Data.Domain.Authentication
{
    [Table("MyAccountsTable")]
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
    }
}
