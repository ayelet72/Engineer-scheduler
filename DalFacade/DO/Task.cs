

using System.Runtime.InteropServices;

namespace DO;

    public record  Task
(
    int Id,
    int EngineerId,
    DO.EngineerExperience Complexity,
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
    public Task() : this(0, 0, 0) 
    {
        CreateAtDate = DateTime.Now; 
    }
    public int id { get; set; }
    public int ENgineerId { get; set;}



}
