using System;

namespace WebStore.Domain.Dto.Identity
{
    public class SetLockoutDto : UserDto
    {
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
