

namespace DO;


/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="EngineerId"></param>
/// <param name="Complexity"></param>
/// <param name="Alias"></param>
/// <param name="Description"></param>
/// <param name="IsMilestone"></param>
/// <param name="CreateAtDate"></param>
/// <param name="StartDate"></param>
/// <param name="ScheduledDate"></param>
/// <param name="DeadlineDate"></param>
/// <param name="CompleteDate"></param>
/// <param name="RequiredEffortTime"></param>
/// <param name="Deliverables"></param>
/// <param name="Remarks"></param>

public record  Task
(
    int Id,
    int EngineerId,
    DO.EngineerExperience Complexity,
    string?Alias=null,
    string?Description=null,
    bool IsMilestone=false,
    DateTime? CreateAtDate=null,
    DateTime? StartDate=null,
    DateTime? ScheduledDate=null,
    DateTime?DeadlineDate=null,
    DateTime? CompleteDate=null,
    TimeSpan? RequiredEffortTime=null,
    string? Deliverables=null,
    string? Remarks=null

)
{
    public Task() : this(0, 0, 0) { } //empty ctor
    //public Task(DO.EngineerExperience complexity, string alias, string description, DateTime? createAtDate= DateTime.Now) : this(0,0,complexity,alias,description, createAtDate)
    //{
    //}
}

