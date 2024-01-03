using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//////hii
namespace DO;                   
xfxdfd
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
    public Engineer() : this(0,"","",0) { } //empty ctor for stage 3

    /// <summary>
    /// RegistrationDate - registration date of the current student record
    /// </summary>
    //public Engineer(int id, string email, string name, double cost):this(id,email,name,cost)
   // {
        //this.ID = id;
        //this.EMail = email;
        //this.Name = name;
        //this.Cost = cost;

    
   // } //get only
}

