

using BlApi;
using BO;

namespace BlImplementation;

internal class Bl : IBl
{
    public Bl() { }
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public ProjectStatus ProjectStatus => throw new NotImplementedException();

    public DateTime? StartProject
    {
        get { return _dal.StartProject; }
        set
        {
            
            _dal.StartProject = value;

          
        }
    }
    public DateTime? EndProject
    {
        get { return _dal.EndProject; }
        set
        {
            _dal.EndProject = value;
           
        }
    }
    public void CreateSchedule()
    {

        var tasks = Task.ReadAll();
        if (tasks.Any(x => x.ScheduledDate == null)|| tasks.Count()==0)
            throw new BO.BlNotAllTasksAreScheduled($"Not all tasks have been entered into the schedule");

        _dal.StartProject = tasks.Min(x => x.ScheduledDate);
        _dal.EndProject = tasks.Max(x => (x.ScheduledDate + x.RequiredEffortTime));



    }
}
