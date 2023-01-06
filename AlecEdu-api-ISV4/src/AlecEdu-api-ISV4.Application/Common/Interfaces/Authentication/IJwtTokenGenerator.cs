using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Entities;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;

namespace AlecEdu_api.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{

    Task<string> GenerateAccessToken(User user, DateTime? expireDays);
    Task<string> GenerateRefreshToken(User user);
    Task<LoginResultDto> GenerateToken(User user);
    Task<TokenResponseDto> GenerateTokenByIdentityServer4(User user, string password);
    Task<TokenResponseDto> GenerateTokenByIdentityServer4(User user);
    Task SaveToken(TokenResponseDto tokenDto, string? refreshTokenOld);
}
