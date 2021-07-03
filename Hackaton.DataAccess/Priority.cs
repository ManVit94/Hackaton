using System.Text.Json.Serialization;

namespace Hackaton.DataAccess
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Priority
    {
        Low,
        Medium,
        High,
        Critical
    }
}
