using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ValidationAttributes.Utilities.Attributes;

namespace ValidationAttributes.Utilities
{
	public static class Validator
	{
		public static bool IsValid(object obj)
		{
			Type objType = obj.GetType();

			PropertyInfo[] objProperties = objType
				.GetProperties()
				.Where(p => p.CustomAttributes
					.Any(ca => typeof(MyValidationAttribute).IsAssignableFrom(ca.AttributeType)))
				.ToArray();

			foreach (PropertyInfo property in objProperties)
			{
				object[] customAttributes = property
					.GetCustomAttributes()
					.Where(ca => typeof(MyValidationAttribute).IsAssignableFrom(ca.GetType()))
					.ToArray();

				object propertyValue = property.GetValue(obj);

				foreach (object customAttribute in customAttributes)
				{
					MethodInfo isValidMethod = customAttribute
						.GetType()
						.GetMethods(BindingFlags.Instance | BindingFlags.Public)
						.FirstOrDefault(m => m.Name == "IsValid");

					if (isValidMethod == null)
						throw new InvalidOperationException("IsValid method not found. Try implementing MyValidationAttribute!");

					bool result = (bool)isValidMethod.Invoke(customAttribute, new object[] { propertyValue });

					if (!result)
						return false;
				}
			}

			return true;
		}
	}
}
