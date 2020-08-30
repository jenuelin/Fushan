using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Configration.Enums
{
    public enum SexEnum
    {
        [EnumMember(Value = "女")]
        Women = 0,
        [EnumMember(Value = "男")]
        Female = 1
    }
}
