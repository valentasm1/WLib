using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Wlib.Core.Admin.Data.Domain.Authentication
{
    [Table("Role")]
    public class ApplicationRole : IdentityRole
    {

    }
}
