using Ardalis.SmartEnum;

namespace Raditap.DataObjects.Raditap
{
    public class RaditapGenderTypes : SmartEnum<RaditapGenderTypes, string>
    {
        public RaditapGenderTypes(string value, string name) : base(name, value) { }

        public static readonly RaditapGenderTypes Male = new RaditapGenderTypes("M", "Male");
        public static readonly RaditapGenderTypes Female = new RaditapGenderTypes("F", "Female");
    }
}
