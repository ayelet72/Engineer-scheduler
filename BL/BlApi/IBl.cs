
using BO;
using DalApi;
using DO;
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
    public void  CreateAutomateSchedule(DateTime? projectStartDate);
    public ProjectStatus CalcStatusProject();
    public  void InitializeDB();
    public  void ResetDB();
    public void IsSchedule();

    #region Properties 
    DateTime Clock { get; }
    #endregion

    #region Methods
    public void AdvanceTimeByYear();
    public void AdvanceTimeByMonth();
    public void AdvanceTimeByDay();
    public void AdvanceTimeByHour();
    void InitializeTime();

    #endregion
}





