namespace CoreApiPOC.Validations
{
    public class IsCompareRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public string Text { get; set; }

        public bool Check(T value)
        {
            bool IsValid = false;
            var str = value as string;

            IsValid = str == Text;

            return IsValid;
        }
    }
}
