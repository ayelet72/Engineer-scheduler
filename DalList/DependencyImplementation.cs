﻿

namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;


internal class DependencyImplementation : IDependency
    //Building Dependency CRUD methods:for stage 1:
{
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependencyId;
        DataSource.Dependencies.Add(item with { ID = id });     // adding the item with the updated running key
        return id;
    }


    public void Delete(int id)
    {
        //if (Read(id) == null)
        //    throw new DalNotExistsException($"Dependency with ID={id} alreadyDeleted");
        //DataSource.Dependencies.Remove(Read(id)!);

        var itemToDelete = DataSource.Dependencies.Single(item => item.ID == id);           //stage 2
        if(itemToDelete==null)
            throw new DalNotExistsException($"Dependency with ID={id} alreadyDeleted");
        DataSource.Dependencies.Remove(itemToDelete);
    }

    public Dependency? Read(int id)
    {
        // return DataSource.Dependencies.Find(item => item.ID == id);           stage1
        return DataSource.Dependencies.FirstOrDefault(item => item.ID == id);

    }
    public Dependency? Read(Func<Dependency, bool> filter)
    {

        return DataSource.Dependencies.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {

        IEnumerable<Dependency> result;
        //return new List<Task>(DataSource.Engineers);             //stage1

        //DataSource.Dependencies.Select(item => item);
        if (filter == null)
            result = from item in DataSource.Dependencies
                     select item;

        //DataSource.Dependencies.Where(filter);
        else
            result = from item in DataSource.Dependencies
                     where filter(item)
                     select item;


        return result;

    }

    public void Update(Dependency item)
    {
        Delete(item.ID);
        DataSource.Dependencies.Add(item);
    }
    public void Reset()
    {
        DataSource.Dependencies.Clear();
        DataSource.Config.ResetDependencyID();
    }
}
