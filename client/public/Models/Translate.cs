namespace HxH.Models
{
    public class Translate
    {
        public Translate(string key, string value, string isoCode)
        {
            Key = key;
            Value = value;
            IsoCode = isoCode;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
        public string IsoCode { get; private set; }
    }
}