namespace SystemOrder.Attributes
{
    public class NameValidator : IValidator
    {
        public bool Validate(string input)
        {
            if (input.Length > 5) return false;

            return true;
        }
    }
}
