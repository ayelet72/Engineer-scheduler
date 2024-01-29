
namespace BlApi;
/// <summary>
/// 
/// </summary>
public interface IEngineer
{
    public int Create(BO.Engineer boengineer);
    public IEnumerable<BO.Engineer> RequestList(List<DO.Engineer> engineers);
    public BO.Engineer? RequestId(int id);
    public void AddEngineer(BO.Engineer engineer);
    public void DeleteEngineer(int id);
    public void UpdateEngineer(BO.Engineer engineer);
}
