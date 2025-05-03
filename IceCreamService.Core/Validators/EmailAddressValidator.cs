using System.Net.Mail;

namespace IceCreamService.Core.Validators
{
    public class EmailAddressValidator
    {
        public bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
