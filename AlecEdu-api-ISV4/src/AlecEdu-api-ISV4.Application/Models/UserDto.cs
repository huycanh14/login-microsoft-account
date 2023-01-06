using System;
using System.ComponentModel.DataAnnotations;
using AlecEdu_api.Application.Mappings;
using AlecEdu_api.Domain.Entities;
using Newtonsoft.Json;

namespace AlecEdu_api.Application.Models;

public class UserDto: IMapFrom<User>
{
    [Display(Name = "Code")]
    //[JsonProperty(PropertyName = "Code")]
    public int Id { get; set; }

    [Display(Name = "Họ và tên")]
    //[JsonProperty(PropertyName = "Họ và tên")]
    public string UserName { get; set; }

    [Display(Name = "Mật khẩu")]
    [JsonIgnore]
    public string Password { get; set; }
}
