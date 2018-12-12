using System;
using System.ComponentModel.DataAnnotations;

namespace _01_BOL.validations
{
    class RangeDateAttribute : ValidationAttribute
    {
       
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int id = (validationContext.ObjectInstance as Project).Id;
            string valueFieldName = validationContext.DisplayName;
            DateTime laterDate = (DateTime)value;
            DateTime earlierDate;
            string validationMessage;
          
                earlierDate = (validationContext.ObjectInstance as Project).StartDate;
                if (laterDate >= earlierDate)
                {
                    return ValidationResult.Success;
                }
                validationMessage = "'end date' must be greater than 'start date'";

            return new ValidationResult(validationMessage);

        }
    }
}

     

