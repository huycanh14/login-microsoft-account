using System.ComponentModel.DataAnnotations;
using AlecEdu_api.Application.Mappings;
using Microsoft.AspNetCore.Identity;

namespace AlecEdu_api.Application.Models;

public class RoleDto: IMapFrom<IdentityRole<int>>
{
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Display(Name = "Tên")]
    public string Name { get; set; }
    
    [Display(Name = "Tên chuẩn hóa")]
    public string NormalizedName { get; set; }
}
