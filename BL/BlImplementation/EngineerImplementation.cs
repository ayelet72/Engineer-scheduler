

using BlApi;
using BO;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void AddEngineer(BO.Engineer engineer)
    {
        throw new NotImplementedException();
    }

    public int Create(Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
        (boEngineer.Id, boEngineer.Email!, boEngineer.Name!, boEngineer.Cost, boEngineer.Level);
        try
        {
            int id = _dal.Engineer.Create(doEngineer);
            return id;
        }
        catch (DO.DalExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }

    }

    public void DeleteEngineer(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Engineer? RequestId(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer> RequestList(List<DO.Engineer> engineers)
    {
        throw new NotImplementedException();
    }

    public void UpdateEngineer(BO.Engineer engineer)
    {
        throw new NotImplementedException();
    }
}
