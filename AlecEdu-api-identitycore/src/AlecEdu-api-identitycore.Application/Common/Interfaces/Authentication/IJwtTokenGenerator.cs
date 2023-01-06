using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AlecEdu_api.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{

    Task<string> GenerateAccessToken(User user, DateTime? accessExpireSeconds);
    Task<string> GenerateRefreshToken(User user);
    Task<LoginResultDto> GenerateToken(User user);
}
