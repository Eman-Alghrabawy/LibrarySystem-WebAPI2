namespace LibrarySystem.Dtos.AccountDtos
{
    // FIX (from the example): FullName is part of the request and is saved on
    // the ApplicationUser. The example had the property on the user but
    // Register never set it.
    public record UserRegisterRequest(string FullName, string Email, string Password);
}
