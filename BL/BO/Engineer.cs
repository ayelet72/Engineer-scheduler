

namespace BO;
/// <summary>
/// 
/// id - id of the Engineer (run number)
/// name-  the name of the engineer
/// Email- the Email of the engineer
/// Level- the level of the engineer
/// Cost- the amount of the payment for this engineer
/// task - the task this engineer is responsible for.
/// </summary>

public class Engineer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public BO.EngineerExperience Level { get; set; }
    public double Cost { get; set; }
    public TaskInEngineer? Task { get; set; }
    public override string ToString() => this.ToStringProperty();

}
