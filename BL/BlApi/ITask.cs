

using BO;

namespace BlApi;
/// <summary>
/// Create- create a new DO.Task by using BO object 
/// Delete- 
/// </summary>
public interface ITask
{
    public int Create(BO.Task boTask);
    public void Delete(int id);
    public void Update(BO.Task boTask);
    public BO.Task Read(int id);
    public IEnumerable<BO.Task> ReadAll(Func<DO.Task, bool>? filter = null);
    public void Update(int id, DateTime scheduelTime);
}