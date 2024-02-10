
namespace BlApi;
/// <summary>
/// 
/// </summary>
public interface IEngineer
{
    public int Create(BO.Engineer boengineer);
    public void Delete(int id);
    public void Update(BO.Engineer engineer);
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer, bool>? filter = null);
}
