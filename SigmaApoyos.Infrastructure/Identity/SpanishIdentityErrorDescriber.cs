using Microsoft.AspNetCore.Identity;

namespace SigmaApoyos.Infrastructure.Identity;

public sealed class SpanishIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => Error(nameof(DefaultError), "Ocurrió un error inesperado.");

    public override IdentityError ConcurrencyFailure() => Error(nameof(ConcurrencyFailure), "La información fue modificada por otro proceso. Intenta nuevamente.");

    public override IdentityError PasswordMismatch() => Error(nameof(PasswordMismatch), "La contraseña actual es incorrecta.");

    public override IdentityError InvalidToken() => Error(nameof(InvalidToken), "El código de seguridad no es válido.");

    public override IdentityError LoginAlreadyAssociated() => Error(nameof(LoginAlreadyAssociated), "Este acceso externo ya está asociado a otra cuenta.");

    public override IdentityError InvalidUserName(string? userName) => Error(nameof(InvalidUserName), $"El usuario '{userName}' no es válido.");

    public override IdentityError InvalidEmail(string? email) => Error(nameof(InvalidEmail), $"El correo electrónico '{email}' no es válido.");

    public override IdentityError DuplicateUserName(string userName) => Error(nameof(DuplicateUserName), $"Ya existe una cuenta con el usuario '{userName}'.");

    public override IdentityError DuplicateEmail(string email) => Error(nameof(DuplicateEmail), $"Ya existe una cuenta con el correo electrónico '{email}'.");

    public override IdentityError InvalidRoleName(string? role) => Error(nameof(InvalidRoleName), $"El rol '{role}' no es válido.");

    public override IdentityError DuplicateRoleName(string role) => Error(nameof(DuplicateRoleName), $"El rol '{role}' ya existe.");

    public override IdentityError UserAlreadyHasPassword() => Error(nameof(UserAlreadyHasPassword), "La cuenta ya tiene una contraseña.");

    public override IdentityError UserLockoutNotEnabled() => Error(nameof(UserLockoutNotEnabled), "El bloqueo no está habilitado para esta cuenta.");

    public override IdentityError UserAlreadyInRole(string role) => Error(nameof(UserAlreadyInRole), $"La cuenta ya tiene asignado el rol '{role}'.");

    public override IdentityError UserNotInRole(string role) => Error(nameof(UserNotInRole), $"La cuenta no tiene asignado el rol '{role}'.");

    public override IdentityError PasswordTooShort(int length) => Error(nameof(PasswordTooShort), $"La contraseña debe tener al menos {length} caracteres.");

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) => Error(nameof(PasswordRequiresUniqueChars), $"La contraseña debe contener al menos {uniqueChars} caracteres diferentes.");

    public override IdentityError PasswordRequiresNonAlphanumeric() => Error(nameof(PasswordRequiresNonAlphanumeric), "La contraseña debe contener al menos un carácter especial.");

    public override IdentityError PasswordRequiresDigit() => Error(nameof(PasswordRequiresDigit), "La contraseña debe contener al menos un número.");

    public override IdentityError PasswordRequiresLower() => Error(nameof(PasswordRequiresLower), "La contraseña debe contener al menos una letra minúscula.");

    public override IdentityError PasswordRequiresUpper() => Error(nameof(PasswordRequiresUpper), "La contraseña debe contener al menos una letra mayúscula.");

    public override IdentityError RecoveryCodeRedemptionFailed() => Error(nameof(RecoveryCodeRedemptionFailed), "El código de recuperación no es válido.");

    private static IdentityError Error(string code, string description)
    {
        return new IdentityError { Code = code, Description = description };
    }
}
