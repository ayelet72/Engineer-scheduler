

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int id = DataSource.Config.UpdateDependencyId;
        DataSource.Dependencies.Add(item with { ID = id });         // adding the item with the updated running key
        return item.ID;
    }


    public void Delete(int id)
    {
        if (Read(id) == null)
            throw new Exception($"Dependency with ID={id} alreadyDeleted");
        DataSource.Dependencies.Remove(Read(id)!);
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.Find(item => item.ID == id);
 
    }

    public List<Dependency?> ReadAll()
    {
        return new List<Dependency?>(DataSource.Dependencies);
        //throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        Delete(item.ID);
        DataSource.Dependencies.Add(item);
    }
}
