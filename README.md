# Spotify-Jovem-Programador

## Configuracao da API

A API usa JWT. Antes de iniciar o Web API, configure uma chave secreta fora do
repositorio. No PowerShell, gere uma chave aleatoria com pelo menos 32 bytes:

```powershell
$env:JWTSettings__SecretKey = [Convert]::ToBase64String(
    [Security.Cryptography.RandomNumberGenerator]::GetBytes(32)
)
dotnet run --project SpotifyRecom.WebApi
```

O tempo de validade do token e configurado em
`SpotifyRecom.WebApi/appsettings.json` (`JWTSettings:ExpirationInMinutes`).
