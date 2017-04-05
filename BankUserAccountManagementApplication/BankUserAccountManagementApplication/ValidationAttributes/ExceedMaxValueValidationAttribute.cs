using BankUserAccountManagmentDAL.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BankUserAccountManagementApplication.ValidationAttributes
{
    public class ExceedMaxValueValidationAttribute : ValidationAttribute
    {

        public ExceedMaxValueValidationAttribute(string otherProperty)
            : base("{0} must be less than or equal to  {1}")
        {
            OtherProperty = otherProperty;
        }

        public string OtherProperty { get; set; }

        public string FormatErrorMessage(string name, string otherName)
        {
            return string.Format(ErrorMessageString, name, otherName);
        }

        protected override ValidationResult
            IsValid(object firstValue, ValidationContext validationContext)
        {
            var firstDecimalValue = firstValue as decimal?;
            var secondDecimalValue = GetSecondAmount(validationContext);

            if (firstDecimalValue != null && secondDecimalValue != null)
            {
                if ((firstDecimalValue + secondDecimalValue) > (decimal)AmountConstants.MaxAmount)
                {
                    object obj = validationContext.ObjectInstance;
                    var thing = obj.GetType().GetProperty(OtherProperty);
                    var displayName = thing.GetCustomAttribute<DisplayAttribute>(true);
                    string errorMessage = validationContext.DisplayName + " and " + displayName;

                    return new ValidationResult(
                        FormatErrorMessage(errorMessage, AmountConstants.MaxAmount.ToString()));
                }
            }

            return ValidationResult.Success;
        }

        protected decimal? GetSecondAmount(
            ValidationContext validationContext)
        {
            var propertyInfo = validationContext
                                  .ObjectType
                                  .GetProperty(OtherProperty);
            if (propertyInfo != null)
            {
                var secondValue = propertyInfo.GetValue(
                    validationContext.ObjectInstance, null);
                return secondValue as decimal?;
            }
            return null;
        }
    }
}
