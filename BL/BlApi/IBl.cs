
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
    public void CreateSchedule();
    public ProjectStatus CalcStatusProject();
    public  void InitializeDB();
    public  void ResetDB();

    #region Properties 
    DateTime Clock { get; }
    #endregion

    #region Methods
    void AdvanceTimeByYear(int years);
    void AdvanceTimeByMonth(int months);
    void AdvanceTimeByDay(int days);
    void AdvanceTimeByHour(int hours);
    void InitializeTime();

    #endregion
}





