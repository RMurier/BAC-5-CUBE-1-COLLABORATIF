using System.ComponentModel.DataAnnotations;

namespace GoodFood.Auth.Api.Models.DTOs;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
