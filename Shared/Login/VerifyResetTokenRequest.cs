using System;

namespace Shared.Login;

public class VerifyResetTokenRequest : EmailRequest
{
    public string ResetToken { get; set; } = "";
}
