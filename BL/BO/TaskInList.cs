
namespace BO;
/// <summary>
/// A class that is describing a task in a list of dependent tasks:
/// Id : the id of the task
/// Description: short description fo the task
/// Alias: a nicK name for the task
/// Status: the status of the progress of the task
/// 
/// </summary>
public class TaskInList
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public Status Status { get; set; }
    public override string ToString() => this.ToStringProperty();
}
