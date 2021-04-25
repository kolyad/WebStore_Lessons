using System.Security.Claims;

namespace WebStore.Domain.Dto.Identity
{
    public class ReplaceClaimDto : UserDto 
    {
        public Claim Claim { get; set; }
        
        public Claim NewClaim { get; set; }
    }
}
