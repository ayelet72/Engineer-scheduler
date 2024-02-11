
namespace BO;
/// <summary>
/// A class to desribe an Engineer whoi is responsible for a task
/// Id: the id of the engineer
/// Name: the name of the Engineer
/// </summary>
public class EngineerInTask
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public override string ToString() => this.ToStringProperty();
}
