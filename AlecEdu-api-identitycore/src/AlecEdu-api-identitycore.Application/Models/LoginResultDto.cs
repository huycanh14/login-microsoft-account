using AlecEdu_api.Domain.Enums;

namespace AlecEdu_api.Application.Models;

public class LoginResultDto
{
    public DateTime Expires { get; set; }
    public int ExpiresIn { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public EUserStatus LoaiTaiKhoan { get; set; }
}
