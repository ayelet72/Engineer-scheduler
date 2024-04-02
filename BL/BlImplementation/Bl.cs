
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

    public IEngineer Engineer => new EngineerImplementation(this);
    public ITask Task => new TaskImplementation(this);

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

    public DateTime AdvanceTimeByYear(DateTime date)
    {
       return date.AddYears(1);
    }

    public DateTime AdvanceTimeByMonth(DateTime date)
    {
        return date.AddMonths(1);
    }

    public DateTime AdvanceTimeByDay(DateTime date)
    {
        return date.AddDays(1);
    }

    public DateTime AdvanceTimeByHour(DateTime date)
    {
        return date.AddHours(1);
    }

    public void InitializeTime()
    {
        s_Clock = DateTime.Now;
    }

    #endregion
}
