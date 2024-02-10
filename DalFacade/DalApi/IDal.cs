using System;
namespace DalApi;

public interface IDal
{
    //DateTime? StartDate { set; get; }
    //DateTime? EndDate { set; get; }

    IEngineer Engineer { get; }
    ITask Task { get; }
    IDependency Dependency { get; }
    DateTime? StartProject { set;get; }
    DateTime? EndProject { set; get; }
    
    void Reset();
}
