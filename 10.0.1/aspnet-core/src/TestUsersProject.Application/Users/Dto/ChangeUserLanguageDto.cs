using System.ComponentModel.DataAnnotations;

namespace TestUsersProject.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}