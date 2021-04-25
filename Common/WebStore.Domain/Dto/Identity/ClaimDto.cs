using System.Collections.Generic;
using System.Security.Claims;

namespace WebStore.Domain.Dto.Identity
{
    public abstract class ClaimDto : UserDto
    {
        public IEnumerable<Claim> Claims { get; set; }
    }
}
