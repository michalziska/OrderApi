using System.Reflection;

namespace SystemOrder.Attributes
{
    public static class ValidationHelper
    {
        public static IValidator GetValidator<T>(string property)
        {
            var modelType = typeof(T);

            var validatorAttr = modelType
                .GetProperty(property)
                .GetCustomAttribute(typeof(ValidateAttribute<>))
                .GetType()
                .GenericTypeArguments.First();

            return Activator.CreateInstance(validatorAttr) as IValidator;
        }

    }
}
