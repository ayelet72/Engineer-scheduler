
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;


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
                throw new DalNotExistsException($"Task with ID={id} alreadyDeleted");
            DataSource.Taskes.Remove(Read(id)!);
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

            Delete(item.Id);
            DataSource.Taskes.Add(item);
        }
    }
