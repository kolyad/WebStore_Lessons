using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Dto.Identity
{
    public class AddLoginDto : UserDto
    {
        public UserLoginInfo UserLoginInfo { get; set; }
    }


}
