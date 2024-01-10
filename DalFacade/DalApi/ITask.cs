

namespace DalApi;

using DO;

public interface ITask : ICrud<Task>
{
    int Create(Task item);          //Creates new entity object in DAL
    Task? Read(int id);             //Reads entity object by its ID
    IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null);     //stage2 Reads all entity objects
    void Update(Task item);                                      //Updates entity object
    void Delete(int id);                                         //Deletes an object by its Id
    Task? Read(Func<Task, bool> filter); // stage 2
}       
