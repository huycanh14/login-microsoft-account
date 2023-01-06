using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AlecEdu_api.Infrastructure.Persistence.Seeds;

public static class UserSeed
{
    public static IEnumerable<User> getUsers()
    {
        var _pass = "123321";
        var admin = new User()
        {
            Id = 1,
            PasswordOrigin = _pass,
            HoTen = "Admin dev",
            NgaySinh = DateTime.Parse("14-07-1998"),
            GioTinh = EGioiTinh.Male,
            DiaChi = "ABC",
            TinhId = 1,
            QuanHuyenId = 1,
            TrangThai = EUserStatus.Active,
            LoaiTaiKhoan = ETypeUser.Admin,
            UserName = "dev",
            NormalizedUserName = "DEV",
            Email = "dev@gmail.com",
            NormalizedEmail = "DEV@GMAIL.COM",
            SecurityStamp = "JWKXHWH4JQ7XMPIWWHLW43VI4LWDWJAJ"
        };
        
        var dev = new User()
        {
            Id = 2,
            PasswordOrigin = _pass,
            HoTen = "Admin dev",
            NgaySinh = DateTime.Parse("14-07-1998"),
            GioTinh = EGioiTinh.Male,
            DiaChi = "ABC",
            TinhId = 1,
            QuanHuyenId = 1,
            TrangThai = EUserStatus.Active,
            LoaiTaiKhoan = ETypeUser.Develop,
            UserName = "dev1",
            NormalizedUserName = "DEV1",
            Email = "dev1@gmail.com",
            NormalizedEmail = "DEV1@GMAIL.COM",
            SecurityStamp = "JWKXHWH4JQ7XMPIWWHLW43VI4LWDWJAJ"
        };
        admin.PasswordHash = PassGenerate(admin, _pass);
        dev.PasswordHash = PassGenerate(dev, _pass);
        return new List<User>
        {
            admin,
            dev
        };
    }
    
    private static string PassGenerate(User user, string password)
    {
        var passHash = new PasswordHasher<User>();
        return passHash.HashPassword(user, password);
    }
}
