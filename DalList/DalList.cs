
namespace Dal;
using DalApi;
using DO;

sealed internal class DalList : IDal
{

    public DateTime? StartProject
    {
        set => DataSource.Config.Startproject = value;
        get => DataSource.Config.Startproject;
    }
    public DateTime? EndProject
    {
        set => DataSource.Config.EndProject = value;
        get => DataSource.Config.EndProject;
    }

    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    //public static void Reset()
    //{
    //    DataSource.Dependencies.Clear();
    //    DataSource.Tasks.Clear();
    //    DataSource.Engineers.Clear();
    //    DataSource.Config.ResetDependencyID();
    //    DataSource.Config.ResetTaskID();
    //    DataSource.Config.ResetDate();

    //}
}

