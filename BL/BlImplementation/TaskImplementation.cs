
using BlApi;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void AddEngineer(BO.Task task)
    {
        throw new NotImplementedException();
    }

    public void DeleteTask(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? RequestId(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task> RequestList(List<DO.Task> tasks)
    {
        throw new NotImplementedException();
    }

    public void UpdateStartDateTask(int id, DateTime date)
    {
        throw new NotImplementedException();
    }

    public void UpdateTask(BO.Task task)
    {
        throw new NotImplementedException();
    }
}
