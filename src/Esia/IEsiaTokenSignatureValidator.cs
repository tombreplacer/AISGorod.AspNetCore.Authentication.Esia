namespace AISGorod.AspNetCore.Authentication.Esia;

/// <summary>
/// Интерфейс для проверки подписи токенов.
/// </summary>
public interface IEsiaTokenSignatureValidator
{
    /// <summary>
    /// Проверить цифровую подпись.
    /// </summary>
    /// <param name="signature">Цифровая подпись, которую необходимо подписать.</param>
    /// <returns>true/false валидна или не валидна подпись.</returns>
    bool Validate(string signature);
}