
namespace Dal;
using DO;
using System.Collections.Generic;
using DalApi;
using static DO.Exceptions;
using System.Linq;

internal class EngineerImplementation : IEngineer
//Building Engineer CRUD methods:for stage 1:
{
    public int Create(Engineer item)     
    {
        if(Read(item.ID) !=null)               //stage2
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
        //if (DataSource.Engineers != null)
        //    return DataSource.Engineers.Find(item => item.ID == id);
        //else
        //    return null;
        return DataSource.Engineers.FirstOrDefault(item => item.ID == id);      //stage 2

    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {

        return DataSource.Engineers.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<Engineer> ReadAll(Func<Engineer,bool>? filter=null)

    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);

    }

    public void Update(Engineer item)
    {
        Delete(item.ID);
        DataSource.Engineers.Add(item);
        
    }
}
