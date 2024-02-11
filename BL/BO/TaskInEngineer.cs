
namespace BO;
/// <summary>
///A class to describe a task that someone is responsible for:
/// Id : the id of the task
/// Alias: it's a nick name for a task
/// </summary>
public class TaskInEngineer
{
    public int Id { get; set; }
    public  string? Alias { get; set; }
    public override string ToString() => this.ToStringProperty();
}
