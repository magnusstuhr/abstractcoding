using System;

namespace AbstractCoding.Extensions
{
    public static class ObjectValidationExtensions
    {
        public static void ValidateIsNotNull(this object obj, string paramName)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}