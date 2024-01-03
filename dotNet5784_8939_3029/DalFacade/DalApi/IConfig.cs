
namespace DalApi;
using DO;

public interface Config
{
    int Creat(DO.Config item);          //Creates new entity object in DAL
    DO.Config? Read(int id);             //Reads entity object by its ID
    List<DO.Config> ReadAll();           //stage 1 only, Reads all entity objects
    void Update(DO.Config item);           //Updates entity object
    void Delete(int id);                //Deletes an object by its Id
}


