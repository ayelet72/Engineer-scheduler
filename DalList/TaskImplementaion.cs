
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

internal class TaskImplementation : ITask
//Building Task CRUD methods:for stage 1:
{
    public int Create(Task item)
    {
        int id = DataSource.Config.NextTaskId;

        // adding the item with the updated running key
        DataSource.Tasks.Add(item with { Id = id, CreateAtDate = item.CreateAtDate });

        return id;
    }

    public void Delete(int id)
    {
        if (Read(id) == null)
            throw new DalNotExistsException($"Task with ID={id} doesn't exsit");

        DataSource.Tasks.RemoveAll(x => x.Id == id);
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(item => item.Id == id);
    }
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        IEnumerable<Task> result;
        //return new List<Task>(DataSource.Tasks);             //stage1

        //DataSource.Tasks.Select(item => item);
        if (filter == null)
            result = from item in DataSource.Tasks
                     select item;

        //DataSource.Tasks.Where(filter);
        else
            result = from item in DataSource.Tasks
                     where filter(item)
                     select item;


        return result;
    }

    public void Update(Task item)
    {
        if (Read(item.Id) == null)
            throw new DalNotExistsException($"Task with ID={item.Id} doesn't exsit");


        DataSource.Tasks.RemoveAll(i => i.Id == item.Id);
        DataSource.Tasks.Add(item);
    }
    public void Reset()
    {      
        DataSource.Tasks.Clear();
        DataSource.Config.ResetTaskID();
    }
}
