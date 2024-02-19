

using BlApi;
using BO;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;

namespace BlImplementation;

internal class Bl : IBl
{
    public Bl() { }
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public IEngineer Engineer => new EngineerImplementation();
    public ITask Task => new TaskImplementation();

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

    public ProjectStatus Status {
        get { return Status; }

        set { CalcStatusProject(); }
    }

    public void CreateSchedule()
    {

        var tasks = Task.ReadAll();
        if (tasks.Any(x => x.ScheduledDate == null)|| tasks.Count()==0)
            throw new BO.BlNotAllTasksAreScheduled($"Not all tasks have been entered into the schedule");

        _dal.StartProject = tasks.Min(x => x.ScheduledDate);
        _dal.EndProject = tasks.Max(x => x.ScheduledDate + x.RequiredEffortTime);



    }

    public ProjectStatus CalcStatusProject()
    {
        var tasks = Task.ReadAll();
        BO.Task? LastTask=tasks.MaxBy(x => x.ScheduledDate);
        if (StartProject == DateTime.MinValue)
            return ProjectStatus.Planning;
        else if (DateTime.Now >= EndProject)
            return ProjectStatus.Execution;

        return ProjectStatus.Scheduled;
            

    }
    public void  InitializeDB() => DalTest.Initialization.Do();
    public void ResetDB() => DalTest.Initialization.Reset();


}
