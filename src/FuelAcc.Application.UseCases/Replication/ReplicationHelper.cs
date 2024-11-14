using FuelAcc.Application.Dto.Replication;
using FuelAcc.Application.Interface.Exceptions;
using FuelAcc.Application.Interface.Replication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO.Compression;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FuelAcc.Application.UseCases.Replication
{
    public class ReplicationHelper : IReplicationHelper
    {
        // todo: move to config file
        public class AuthOptions
        {
            private const string KEY = "mysupersecret_secretkey!12345678";   // encryption key. MUST be 256 bit!

            public static SymmetricSecurityKey GetSymmetricSecurityKey()
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
            }
        }

        public byte[] Compress(string serialized)
        {
            using (var zipStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
                {
                    var entry = archive.CreateEntry("packet.jws");
                    using (var writer = new StreamWriter(entry.Open()))
                    {
                        writer.Write(serialized);
                    }
                }
                return zipStream.ToArray();
            }
        }

        public string ConstructFileName(ReplictionPacketDto packet)
        {
            var dest = packet.BranchId.ToString("N");
            var ts = packet.Date.ToString("yyyyMMdd_HHmmss_fff");

            var result = $"to_{dest}_{ts}.zip";

            return result;
        }

        public string Decompress(byte[] data)
        {
            using (var zipStream = new MemoryStream(data))
            {
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
                {
                    var entry = archive.GetEntry("packet.jws");
                    if (entry is null)
                    {
                        throw new DomainException("Can't find packet.jws in zip file");
                    }
                    using (var reader = new StreamReader(entry.Open()))
                    {
                        var text = reader.ReadToEnd();
                        return text;
                    }
                }
            }
        }

        public ReplictionPacketDto Deserialize(string data)
        {
            var serialized = ExtractClaim(data);

            if (serialized == null)
            {
                throw new DomainException("Packet claim data not found");
            }

            var pkt = JsonSerializer.Deserialize<ReplictionPacketDto>(serialized);

            return pkt;

            static string? ExtractClaim(string data)
            {
                try
                {
                    var jwtHandler = new JwtSecurityTokenHandler();

                    var authSigningKey = AuthOptions.GetSymmetricSecurityKey();

                    // todo: use async?
                    var claimsPrincipal = jwtHandler.ValidateToken(data, new TokenValidationParameters()
                    {
                        RequireExpirationTime = false,
                        ValidateLifetime = false,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = authSigningKey,
                    }, out var token);

                    var serialized = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "data")?.Value;
                    return serialized;
                }
                catch (Exception ex)
                {
                    throw new DomainException("Can't decode token", ex);
                }
            }
        }

        public string SerializeAndSign(ReplictionPacketDto packet)
        {
            // use JWT as signed container for transfer replication data
            var serialized = JsonSerializer.Serialize(packet);

            var issuer = packet.SourceBranchId.ToString();
            var audience = packet.BranchId.ToString();
            var claims = new[] {
                new Claim("data", serialized),
            };

            var authSigningKey = AuthOptions.GetSymmetricSecurityKey();

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenText = jwtHandler.WriteToken(token);

            return tokenText;
        }
    }
}