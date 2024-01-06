using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DO;
using DalApi;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int id = DataSource.Config.UpdateTaskId;
        DataSource.Taskes.Add(item with { Id = id });         // adding the item with the updated running key
        return item.Id;
    }

    public void Delete(int id)
    {
        if (Read(id) == null)
            throw new Exception($"Task with ID={id} alreadyDeleted");
        DataSource.Taskes.Remove(Read(id)!);
    }

    public Task? Read(int id)
    {
        return DataSource.Taskes.Find(item => item.Id == id);

        throw new NotImplementedException();
    }

    public List<Task> ReadAll()
    {

        return new List<Task>(DataSource.Taskes);
        //throw new NotImplementedException();
      
    }

    public void Update(Task item)
    {
        Delete(item.Id);
        DataSource.Taskes.Add(item);
    }
}
