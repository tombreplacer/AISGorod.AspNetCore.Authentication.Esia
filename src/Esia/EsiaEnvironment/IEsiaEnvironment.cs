using System.Security.Cryptography.X509Certificates;

namespace AISGorod.AspNetCore.Authentication.Esia.EsiaEnvironment;

/// <summary>
/// Интерфейс настроек среды ЕСИА.
/// </summary>
public interface IEsiaEnvironment
{
    /// <summary>
    /// Сертификат среды ЕСИА.
    /// </summary>
    X509Certificate2 EsiaCertificate { get; }

    /// <summary>
    /// GOST Сертификат среды ЕСИА. (в виде base64 строки так как не хочется
    /// подключать cryptoPro в этот проект, а CpX509Certificate2 не наследуется
    /// от стандартного класса X509Certificate)
    /// </summary>
    string EsiaCertificateGOSTRaw { get; }

    /// <summary>
    /// Базовый URL для запросов.
    /// </summary>
    string Host { get; }

    /// <summary>
    /// Endpoint для получения авторизационного кода.
    /// </summary>
    string AuthorizationEndpoint { get; }

    /// <summary>
    /// Endpoint для получения маркера доступа и(или) маркера идентификации.
    /// </summary>
    string TokenEndpoint { get; }

    /// <summary>
    /// Endpoint для логаута.
    /// </summary>
    string LogoutEndpoint { get; }

    /// <summary>
    /// Базовый URL для REST-сервиса персональных данных.
    /// </summary>
    string RestPersonsEndpoint { get; }

    /// <summary>
    /// Issuer маркеров доступа.
    /// </summary>
    string Issuer { get; }
}