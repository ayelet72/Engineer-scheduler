
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer        //internal
{
    public int Creat(Engineer item)     // אליעזר?
    {
        //int newId = DataSource.Config.NextProjectId;
        //IEngineer objCopy = (IEngineer)obj.Clone();
        //// Assuming T has a property named "Id"
        //typeof(IEngineer).GetProperty("Id").SetValue(objCopy, newId);

        //DataSource.Config.NextRunningId++;
        //DataSource.Config.ObjectsList.Add(objCopy);

        //return newId;
        ////int newId=
        //Engineers
        //throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
