
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class TaskImplementation : ITask
     //Building Task CRUD methods:for stage 1:
{
        public int Create(Task item)
        {
            int id = DataSource.Config.UpdateTaskId;
            DataSource.Taskes.Add(item with { Id = id, CreateAtDate=item.CreateAtDate });         // adding the item with the updated running key
            return item.Id;
        }

        public void Delete(int id)
        {
                throw new DalDeletionImpossible($"Impossible to delete");
        }

        public Task? Read(int id)
        {
            return DataSource.Taskes.Find(item => item.Id == id);
        }

        public List<Task> ReadAll()
        {

            return new List<Task>(DataSource.Taskes);
           
        }

        public void Update(Task item)
        {

            if (DataSource.Taskes.RemoveAll(i => i.Id == item.Id) == 0)
            throw new DalNotExistsException($"Task with ID={item.Id} doesn't exsit");
            DataSource.Taskes.Add(item);
        }
    }
