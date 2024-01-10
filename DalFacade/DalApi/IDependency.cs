


namespace DalApi;
using DO;

public interface IDependency : ICrud<Dependency>
{
    int Create(Dependency item);        //Creates new entity object in DAL
    Dependency? Read(int id);             //Reads entity object by its ID
    IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null);           //stage2 Reads all entity objects
    void Update(Dependency item);           //Updates entity object
    void Delete(int id);                //Deletes an object by its Id

    Dependency? Read(Func<Dependency, bool> filter); // stage 2
}


