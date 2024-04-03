using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL;
internal class ExperienceLevelCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> s_enums =
(Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

internal class StatusCollection : IEnumerable
{
    static readonly IEnumerable<BO.Status> s_enums =
    (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
internal class EngineersCollection : IEnumerable
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static readonly IEnumerable<BO.Engineer> s_engineers = s_bl.Engineer.ReadAll();
    public IEnumerator GetEnumerator() => s_engineers.GetEnumerator();
}



