namespace CLRDGAddress.Abstractions
{
    public class Country
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public override string ToString()
        {
            return CountryName;
        }
    }
}
