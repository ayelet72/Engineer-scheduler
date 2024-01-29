

namespace BlApi;
/// <summary>
/// 
/// </summary>
public interface ITask
{
    public IEnumerable<BO.Task> RequestList(List<DO.Task> tasks);
    public BO.Task? RequestId(int id);
    public void AddEngineer(BO.Task task);
    public void DeleteTask(int id);
    public void UpdateTask(BO.Task task);
    public void UpdateStartDateTask(int id,DateTime date);




}
