
namespace Dal;
using DO;
using System.Collections.Generic;
using DalApi;
using static DalApi.Exceptions;

public class EngineerImplementation : IEngineer        //internal
{
    public int Create(Engineer item)     // אליעזר?
    {
        if (Read(item.ID) != null)
            throw new DalExistsException($"Engineer with ID={item.ID} alreadyExist");
        DataSource.Engineers.Add(item);
        return item.ID;
    }

    public void Delete(int id)
    {
        if (Read(id) == null)
            throw new DalNotExistsException($"Engineer with ID={id} alreadyDeleted");
        DataSource.Engineers.Remove(Read(id)!);
    }
    

    public Engineer? Read(int id)
    {
        if (DataSource.Engineers != null)
            return DataSource.Engineers.Find(item => item.ID == id);
        else
            return null;

    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Delete(item.ID);
        DataSource.Engineers.Add(item);

    }
}
