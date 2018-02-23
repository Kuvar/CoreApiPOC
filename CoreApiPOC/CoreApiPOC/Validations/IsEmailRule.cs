namespace CoreApiPOC.Validations
{
    using System.Text.RegularExpressions;

    public class IsEmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value != null)
            {
                var str = value as string;

                Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                Match match = regex.Match(str);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
}
