using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace GoplayasiaBlazor.DTOs.DTOOut;

public class LoginOTPDTO
{
    [Required]
    public string mobileNumber { get; set; } = "";
    public bool IsValid { get { return isValidMethod(); } }
    public bool isValidMethod()
    {
        string pattern = @"^09\d{9}$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(mobileNumber);
    }
}
