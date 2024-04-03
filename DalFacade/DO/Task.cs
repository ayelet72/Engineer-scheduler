

using System.Runtime.InteropServices;

namespace DO;

    public record  Task
(
    int Id,
    int? EngineerId=null,
    DO.EngineerExperience Complexity= EngineerExperience.None,
    string? Alias = null,
    string? Description = null,
    bool IsMilestone = false,
    DateTime? CreateAtDate = null,
    DateTime? StartDate = null,
    DateTime? ScheduledDate = null,
    DateTime? DeadlineDate = null,
    DateTime? CompleteDate = null,
    TimeSpan? RequiredEffortTime = null,
    string? Deliverables = null,
    string? Remarks = null

)
{
    public Task() : this(0, 0, 0) //empty ctor for stage 1
    {
        CreateAtDate = DateTime.Now; 
    }
   



}
