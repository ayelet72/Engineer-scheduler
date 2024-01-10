

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;


internal class DependencyImplementation : IDependency
    //Building Dependency CRUD methods:for stage 1:
{
    public int Create(Dependency item)
    {
        int id = DataSource.Config.UpdateDependencyId;
        DataSource.Dependencies.Add(item with { ID = id });     // adding the item with the updated running key
        return item.ID;
    }


    public void Delete(int id)
    {
        //if (Read(id) == null)
        //    throw new DalNotExistsException($"Dependency with ID={id} alreadyDeleted");
        //DataSource.Dependencies.Remove(Read(id)!);

        var itemToDelete = DataSource.Dependencies.Single(item => item.ID == id);           //stage 2
        DataSource.Dependencies.Remove(itemToDelete);
    }

    public Dependency? Read(int id)
    {
        // return DataSource.Dependencies.Find(item => item.ID == id);           stage1
        return DataSource.Dependencies.FirstOrDefault(item => item.Id == id);

    }
    public Dependency? Read(Func<Dependency, bool> filter)
    {

        return DataSource.Dependencies.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {

        //return new List<Dependency>(DataSource.Dependencies);
        //throw new NotImplementedException();
        if (filter == null)
            return DataSource.Dependencies.Select(item => item);
        else
            return DataSource.Dependencies.Where(filter);
    }

    public void Update(Dependency item)
    {
        Delete(item.ID);
        DataSource.Dependencies.Add(item);
    }
}
