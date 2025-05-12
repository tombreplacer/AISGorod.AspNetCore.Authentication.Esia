using System;
using System.Text;
using AISGorod.AspNetCore.Authentication.Esia.CryptoPro.Options;
using AISGorod.AspNetCore.Authentication.Esia.Options;
using CryptoPro.Security.Cryptography.X509Certificates;

namespace AISGorod.AspNetCore.Authentication.Esia.CryptoPro;

/// <summary>
/// Расширение для CryptoPro.
/// </summary>
public static class CryptoProExtensions
{
    /// <summary>
    /// Добавление подписи через bouncy castle.
    /// </summary>
    /// <remarks>
    /// Необходимо использовать только один обработчик для подписи.
    /// </remarks>
    /// <param name="options">Настройки.</param>
    /// <param name="configure">Конфигурация.</param>
    public static void UseCryptoPro(this EsiaOptions options, Action<CryptoProOptions> configure)
    {
        options.UseSigner(_ =>
        {
            var cpOptions = new CryptoProOptions();
            configure.Invoke(cpOptions);
            return new CryptoProEsiaSigner(cpOptions);
        });
    }

    /// <summary>
    /// Добавление ГОСТ Р 34.10-2012 валидатора сигнатуры JWT.
    /// </summary>
    /// <remarks>
    /// Необходимо использовать только один валидатор для подписи.
    /// </remarks>
    /// <param name="options">Настройки.</param>
    public static void UseGostTokenValidator(this EsiaOptions options)
    {
        var certificate = new CpX509Certificate2(Encoding.UTF8.GetBytes(options.EnvironmentInstance!.EsiaCertificateGOSTRaw));
        options.UseTokenValidatior(new JWSTokenSignatureValidatorGOST(certificate));
    }
}