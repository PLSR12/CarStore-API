using CarStore.Exceptions;
using FluentValidation;
using FluentValidation.Validators;

namespace CarStore.Application.SharedValidator
{
    public class PasswordValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "PasswordValidator";

        protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";


        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessagesException.PASSWORD_INVALID);

                return false;
            }

            if (value.Length < 6)
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", ResourceMessagesException.PASSWORD_LENGTH);

                return false;
            }
            return true;
        }
    }
}
