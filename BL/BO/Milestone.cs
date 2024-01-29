
//צריך לכתוב תיעוד
namespace BO;

public class Milestone
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public DateTime? CreateAtDate { get; init; }
    public Status Status { get; set; }
    public DateTime? ForCastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public double CompletionPercentage { get; set; }
    public string? Remarks { get; set; }
    public List<TaskInList>? Dependencies { get; set; }
    public override string ToString() => this.ToStringProperty();


}
