using System.Text;
using CryptoPro.Security.Cryptography;
using CryptoPro.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;


namespace AISGorod.AspNetCore.Authentication.Esia.CryptoPro;

internal class JWSTokenSignatureValidatorGOST : IEsiaTokenSignatureValidator
{
    private CpX509Certificate2 _certificate;
    private Gost3410_2012_256CryptoServiceProvider _algorithm;

    public JWSTokenSignatureValidatorGOST(CpX509Certificate2 certificate)
    {
        _certificate = certificate;
        if (_certificate?.PublicKey?.Key != null)
        {
            _algorithm = (_certificate.PublicKey.Key as Gost3410_2012_256CryptoServiceProvider)!;
        }
        else
        {
            throw new SecurityTokenValidationException("[JWSTokenSignatureValidatorGOST] Сертификат не содержит публичный ключ для проверки подписей");
        }
    }

    public bool Validate(string token)
    {
        string[] parts = token.Split('.');
        if (parts.Length != 3)
        {
            throw new SecurityTokenValidationException("Невалидный JWT формат");
        }

        string plainJWT = parts[0] + "." + parts[1];
        byte[] signature = Base64UrlEncoder.DecodeBytes(parts[2]);

        bool isValid = _algorithm.VerifyData(Encoding.UTF8.GetBytes(plainJWT), signature);

        if (!isValid)
        {
            throw new SecurityTokenValidationException("GOST подпись не прошла проверку");
        }
        return isValid;
    }
}