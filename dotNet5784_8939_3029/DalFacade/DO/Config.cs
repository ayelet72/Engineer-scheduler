
namespace DO;
 
/// </summary>
/// <param name="Stautus"> which in level the project is in</param>
/// <param name="StartDate">a date for the starting of the project</param>
/// <param name="EndDate">a date for the ending of the project</param>
public record Config
(

    ProjectStatus Stautus = ProjectStatus.Unscheduled,
    DateTime? StartDate =null,
    DateTime? EndDate = null
)
{
    public Config() : this(0) { }             //empty ctor for stage 3

}

