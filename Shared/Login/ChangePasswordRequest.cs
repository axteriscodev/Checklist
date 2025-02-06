using System;

namespace Shared.Login;

public class ChangePasswordRequest : VerifyResetTokenRequest
{
        public string OldPassword { get; set; } = "";
        public string NewPassword { get; set; } = "";
}
