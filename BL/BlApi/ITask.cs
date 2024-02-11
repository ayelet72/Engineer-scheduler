

using BO;

namespace BlApi;
/// <summary>
/// Create- create a new DO.Task by using BO object 
/// Delete- Delete the BO object from the data-bace
/// Update 1)- update the data-bace object according to the BO object 
/// Read - search the BO object in the data-bace
/// ReadAll - returns a List of all the tasks 
/// Update 2)- update the scheduel time

/// </summary>
public interface ITask
{
    public int Create(BO.Task boTask);
    public void Delete(int id);
    public void Update(BO.Task boTask); //1
    public BO.Task Read(int id);
    public IEnumerable<BO.Task> ReadAll(Func<DO.Task, bool>? filter = null);
    public void Update(int id, DateTime scheduelTime); //2
}