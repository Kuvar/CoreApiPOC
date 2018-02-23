namespace CoreApiPOC.Validations
{
    using System;

    public class IsNumberRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value != null)
            {
                try
                {
                    var str = value as string;

                    Int64 num;
                    if (Int64.TryParse(str, out num))
                        return true;
                    else
                        return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
