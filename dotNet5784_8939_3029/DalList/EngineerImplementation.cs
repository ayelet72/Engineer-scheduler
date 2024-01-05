
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer        //internal
{
    public int Create(Engineer item)     // אליעזר?
    {
        if (Read(item.ID) != null)
            throw new Exception($"Engineer with ID={item.ID} alreadyExist");
        DataSource.Engineers.Add(item);
        return item.ID;
    }

    public void Delete(int id) => throw new Exception("Cannot be deleted");
    

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(item => item.ID == id);
        //throw new NotImplementedException();
    }

    public List<Engineer?> ReadAll()
    {
        return new List<Engineer?>(DataSource.Engineers);
        //throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        Delete(item.ID);
        DataSource.Engineers.Add(item);

    }
}
