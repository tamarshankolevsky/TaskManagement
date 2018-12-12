using _00_DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace _01_BOL.validations
{
    class UniqeNameAttribute : ValidationAttribute
    {
        override protected ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty("Name");
            object propertyValue = property.GetValue(instance);
            string query = $"SELECT project_id FROM task_managment.projects WHERE name='{value}'";
            
            if (DBAccess.RunScalar(query)!= null)
                return new ValidationResult("the Name is allready exist");
            else return null;
        }
    }
}




