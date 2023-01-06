using AlecEdu_api.Application.Mappings;
using AlecEdu_api.Domain.Entities;
using IdentityModel.Client;

namespace AlecEdu_api.Application.Models;

public class TokenResponseDto: IMapFrom<TokenResponse>
{
    public string AccessToken { get; set; }
    public string IdentityToken { get; set; }
    public string IssuedTokenType { get; set; }
    public string TokenType { get; set; }
    public string RefreshToken { get; set; }
    public string ErrorDescription { get; set; }
    public string ExpiresIn { get; set; }
    public DateTime ExpireDateTime { get; set; }
}
