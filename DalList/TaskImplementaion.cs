
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

internal class TaskImplementation : ITask
//Building Task CRUD methods:for stage 1:
{
    public int Create(Task item)
    {
        int id = DataSource.Config.UpdateTaskId;

        // adding the item with the updated running key
        DataSource.Taskes.Add(item with { Id = id, CreateAtDate = item.CreateAtDate });

        return id;
    }

    public void Delete(int id)
    {
        if (Read(id) == null)
            throw new DalNotExistsException($"Task with ID={id} doesn't exsit");

        DataSource.Taskes.RemoveAll(x => x.Id == id);
    }

    public Task? Read(int id)
    {
        //return DataSource.Taskes.Find(item => item.Id == id);             stage1
        return DataSource.Taskes.FirstOrDefault(item => item.Id == id);
    }
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Taskes.FirstOrDefault(item => filter(item));
    }
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        IEnumerable<Task> result;
        //return new List<Task>(DataSource.Taskes);             //stage1

        //DataSource.Taskes.Select(item => item);
        if (filter == null)
            result = from item in DataSource.Taskes
                     select item;

        //DataSource.Taskes.Where(filter);
        else
            result = from item in DataSource.Taskes
                     where filter(item)
                     select item;


        return result;
    }

    public void Update(Task item)
    {
        if (Read(item.Id) == null)
            throw new DalNotExistsException($"Task with ID={item.Id} doesn't exsit");


        DataSource.Taskes.RemoveAll(i => i.Id == item.Id);
        DataSource.Taskes.Add(item);
    }
}
