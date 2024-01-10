

namespace DalApi;
using DO;
public interface IEngineer: ICrud<Engineer>
{

    int Create(Engineer item);          //Creates new entity object in DAL
    Engineer? Read(int id);             //Reads entity object by its ID
    IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null);           //stage2 Reads all entity objects
    void Update(Engineer item);           //Updates entity object
    void Delete(int id);                //Deletes an object by its Id
    Engineer? Read(Func<Engineer, bool> filter); // stage 2
}
