namespace SystemOrder.Attributes
{
    public class DescValidator : IValidator
    {
        public bool Validate(string input)
        {
            if (input.Length > 20)
                return false;

            return true;
        }
    }
}
