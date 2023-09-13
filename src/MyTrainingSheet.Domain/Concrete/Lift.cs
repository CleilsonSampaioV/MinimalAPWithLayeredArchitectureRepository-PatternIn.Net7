using System.Text.Json.Serialization;

namespace MyTrainingSheet.Domain;
public class Lift : IEntity
{
    public int Id { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LiftName Name { get; set; }
    public int? Weight { get; set; }
    public int Reps { get; set; }
}