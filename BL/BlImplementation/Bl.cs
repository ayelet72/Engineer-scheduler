
using BlApi;
using BO;
using DalApi;
using DO;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;

namespace BlImplementation;

internal class Bl : IBl
{
    public Bl() { }
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public BlApi.IEngineer Engineer => new EngineerImplementation(this);
    public BlApi.ITask Task => new TaskImplementation(this);

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

    public void CreateManualSchedule()
    {
        var tasks = Task.ReadAll();
        if (tasks.Any(x => x.ScheduledDate == null)|| tasks.Count()==0)
            throw new BO.BlNotAllTasksAreScheduled($"Not all tasks have been entered into the schedule");

        _dal.StartProject = tasks.Min(x => x.ScheduledDate);
        _dal.EndProject = tasks.Max(x => x.ScheduledDate + x.RequiredEffortTime);

    }
    public  void CreateAutomateSchedule(DateTime? projectStartDate)
    {
        _dal.StartProject = projectStartDate;
        var tasks = _dal.Task.ReadAll().ToDictionary(t => t.Id);
        var dependecies = _dal.Dependency.ReadAll();
        foreach (var task in tasks.Values)
        {
            var depndencies1 = dependecies.Where(d => d.DependentTask == task.Id);
            var dependeciesTasks= depndencies1.Select(t => _dal.Task.Read(t.DependsOnTask!)).ToList();
            var maxDate = dependeciesTasks.Max(t => t!.ScheduledDate + t.RequiredEffortTime);
            var scheduledDate = maxDate is null ? _dal.StartProject : maxDate;
            _dal.Task.Update(task with { ScheduledDate = scheduledDate });

        }

    }
    public void IsSchedule()
    {
        if (StartProject == DateTime.MinValue)
            throw new BlIsScheduled($"The project has not yet been scheduled");
    }


    public ProjectStatus CalcStatusProject()
    {
        var tasks = Task.ReadAll();
        BO.Task? LastTask=tasks.MaxBy(x => x.ScheduledDate);
        if (StartProject == DateTime.MinValue)
            return ProjectStatus.Planning;
        else if (Clock >= EndProject)          //checking
            return ProjectStatus.Execution;

        return ProjectStatus.Scheduled;
    }
    public void  InitializeDB() => DalTest.Initialization.Do();
    public void ResetDB() => DalTest.Initialization.Reset();

    //Cloke defination: for stage 6 : 

    #region Private Fields
    private static DateTime s_Clock = DateTime.Now;
    #endregion

    #region Properties
    public DateTime Clock
    {
        get { return s_Clock; }
        private set { s_Clock = value; }
    }
    #endregion

    #region Methods

    public void AdvanceTimeByYear()
    {
       s_Clock= s_Clock.AddYears(1);
        Clock = s_Clock;
    }

    public void AdvanceTimeByMonth()
    {
        s_Clock = s_Clock.AddMonths(1);
        Clock = s_Clock;
    }

    public void AdvanceTimeByDay()
    {
        s_Clock= s_Clock.AddDays(1);
        Clock = s_Clock;
    }

    public void AdvanceTimeByHour( )
    {
        s_Clock= s_Clock.AddHours(1);
        Clock = s_Clock;
    }

    public void InitializeTime()
    {
        s_Clock = DateTime.Now;
    }

  

    #endregion
}
