using _00_DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace _01_BOL.validations
{
    class UniqeUserNameAttribute : ValidationAttribute
    {
        override protected ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty("Id");
            object propertyValue = property.GetValue(instance);
            if ((int)propertyValue != 0)
                return null;
            string query = $"SELECT worker_id FROM task_managment.workers WHERE user_name='{value}'";
            
            if (DBAccess.RunScalar(query)!=null)
                return new ValidationResult("the UserName is exist");
            else return null;
        }
    }
}




