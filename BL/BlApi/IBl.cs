
using BO;
using DalApi;
using System.Security.Cryptography.X509Certificates;

namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public ProjectStatus Status { set; get; }
    
    public DateTime? StartProject { set; get; }
    public DateTime? EndProject { set; get; }
    public void CreateManualSchedule(); //not in use
    public void CreateAutomateSchedule(IEnumerable<BO.Task> Tasks, DateTime projectStartDate);
    public ProjectStatus CalcStatusProject();
    public  void InitializeDB();
    public  void ResetDB();

    #region Properties 
    DateTime Clock { get; }
    #endregion

    #region Methods
    DateTime AdvanceTimeByYear(DateTime date);
    DateTime AdvanceTimeByMonth(DateTime date);
    DateTime AdvanceTimeByDay(DateTime date);
    DateTime AdvanceTimeByHour(DateTime date);
    void InitializeTime();

    #endregion
}





