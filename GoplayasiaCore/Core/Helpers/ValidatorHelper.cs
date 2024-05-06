using System.Text.RegularExpressions;

namespace GoplayasiaBlazor.Core.Helpers
{
    public sealed class ValidatorHelper
    {
        public static async Task<bool> ValidateEmail(string emailAddress)
        {
            try
            {
                if (string.IsNullOrEmpty(emailAddress))
                    return false;
                if (emailAddress.Contains(" "))
                    return false;
                if (emailAddress.Trim().EndsWith('.') || !emailAddress.Contains('@'))
                    return false;
                var splitEmail = emailAddress.Split('@');
                if (splitEmail == null || splitEmail.Length < 2)
                    return false;
                if (string.IsNullOrEmpty(splitEmail.LastOrDefault()) || !splitEmail.LastOrDefault().Contains('.'))
                    return false;
                var splitDomain = splitEmail.LastOrDefault().Split('.');
                if (splitDomain == null || splitDomain.Length < 2)
                    return false;
                if (string.IsNullOrEmpty(splitDomain.LastOrDefault()) || splitDomain.LastOrDefault().Length < 2)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<int> ValidatePassword(string password, string confirmPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || password != confirmPassword)
                    return 1;
                if ( password.Length < 8)
                    return 2;
                if (password.Contains(" ") || confirmPassword.Contains(" "))
                    return 3;
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static async Task<bool> ValidateDate(int month, int day, int year)
        {
            try
            {
                if (month < 1 || month > 12)
                    return false;
                if (day < 1 || day > 31)
                    return false;
                int yearLimiter = DateTime.UtcNow.Year;
                if (year < (yearLimiter - 100) || year > yearLimiter)
                    return false;
                DateTime dateCreator = new DateTime(year, month, day);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> ValidateAge(int limiter, DateTime dateOfBirth)
        {
            try
            {
                DateTime dateNow = DateTime.UtcNow;
                int age = dateNow.Year - dateOfBirth.Year;
                if (dateNow.Month < dateOfBirth.Month || (dateNow.Month == dateOfBirth.Month && dateNow.Day < dateOfBirth.Day))
                    age--;
                if (age < limiter)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> ValidateIssueId(string IdentificationNumber)
        {
            var isValid = Regex.IsMatch(IdentificationNumber, "^[0-9a-zA-Z-]+$");
            if (isValid)
            {
                return true;
            }
            return false;
        }
    }
}
