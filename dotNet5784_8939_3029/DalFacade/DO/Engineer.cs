
namespace DO;
/// <summary>
/// Engineer involved in projects with milestones
/// <see>Monday</see>
/// </summary>
/// <param name="ID"> int as identifier</param>
/// <param name="EMail">email of the worker</param>
/// <param name="Name">name of the worker</param>
/// <param name="Cost">"alibaba and 40 thieves</param>
/// <param name="Level">Experience level</param>
/// <param name="Active">ana aref</param>
public record Engineer
(
    int ID,
    string EMail,
    string Name,
    double Cost,
    EngineerExperience Level = EngineerExperience.Beginner,
    bool Active = true
)
{
    public Engineer() : this(0,"","",0) { }      //empty ctor for stage 1

    public int Id { get; set; }

    /// <summary>
    /// RegistrationDate - registration date of the current student record
    /// </summary>
    


    // } //get only
}

