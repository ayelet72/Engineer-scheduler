
namespace BO;
/// <summary>
/// id - id of the task (run number)
/// Description- description of the task
/// Alias- it's a nick name for a task
/// CreateAtDate- date of the time of creating a task
/// Status- describe the progress of a task
/// Dependencies- a list of tasks that a task depends on
/// MileStone-
/// RequiredEffortTime- the time that requires to finish the task 
/// Startproject- the date that the engineer starts the task
/// ScheduledDate- the date that the project manager put a task in the schedule
/// DeadlineDate- the date that the project manager decides a task should be done 
/// CompleteDate- the date a task actually has been done
/// ForCastDate- estimated date for task completion (by the project manager)
/// Deliverables- results from the task
/// Remarks- notes during doing the task 
/// Engineer- the  responsible engineer of a task
/// Complexity- the level of the responsible engineer
/// 
/// </summary>

public class Task
{

    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public DateTime? CreateAtDate { get; init; }
    public Status Status { get; set; }
    public List<TaskInList>? Dependencies { get; set; }
    public TimeSpan? RequiredEffortTime { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ScheduledDate { get; set; }
   // public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public DateTime? ForCastDate { get; set; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public EngineerInTask? Engineer { get; set; }
    public BO.EngineerExperience Complexity { get; set; }
    public override string ToString() => this.ToStringProperty();
    
    
}
    





