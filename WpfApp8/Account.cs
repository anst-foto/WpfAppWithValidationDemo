using System.ComponentModel.DataAnnotations;

namespace WpfApp8;

public class Account
{
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(20, 
        MinimumLength = 3, 
        ErrorMessage = "Логин должен быть не меньше 3 символов и не больше 20 символов.")]
    public string Login { get; set; }
    
    [Required]
    [StringLength(20, 
        MinimumLength = 8, 
        ErrorMessage = "Пароль должен быть не меньше 3 символов и не больше 20 символов.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])", 
        ErrorMessage = "Пароль должен содержать как минимум одну букву, одну цифру и один спецсимвол.")]
    public string Password { get; set; }
}