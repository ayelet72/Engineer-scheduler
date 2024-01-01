using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
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
    );
}
