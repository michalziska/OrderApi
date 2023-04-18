namespace SystemOrder.Attributes
{
    public interface IValidator
    {
        bool Validate(string input);
    }
}
