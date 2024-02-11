
namespace BlApi;
/// <summary>
/// Create- create a new DO.Engineer by using BO object 
/// Delete- Delete the BO object from the data-bace
/// Update 1)- update the data-bace object according to the BO object 
/// Read - search the BO object in the data-bace
/// ReadAll - returns a List of all the Engineers 
/// </summary>
public interface IEngineer
{
    public int Create(BO.Engineer boengineer);
    public void Delete(int id);
    public void Update(BO.Engineer engineer);
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer, bool>? filter = null);
}
