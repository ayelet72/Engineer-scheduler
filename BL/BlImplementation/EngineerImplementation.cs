

using BlApi;
using BO;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Linq;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
   
    public int Create(BO.Engineer boEngineer)
    // a function that creates an engineer
    {
        
        if (boEngineer.Id > 0 && boEngineer.Name != null&& boEngineer.Email!=null&& boEngineer.Cost>0)
        {
            try {
                    int numId = _dal.Engineer.Create(new DO.Engineer()
                    {

                        ID = boEngineer.Id,
                        Name = boEngineer.Name,
                        EMail = boEngineer.Email,
                        Level = (DO.EngineerExperience)boEngineer.Level,
                        Cost = boEngineer.Cost
                    }
                    );

                return numId;  
            }
            catch (DO.DalExistsException ex)
            {
                throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
            }
            
        }
        throw new BO.BlInvalidDataException($"Engineer with ID={boEngineer.Id} invalid");

    }
    // a function that creates an engineer
    public void Delete(int id)
    // a function that deletes an engineer
    {
        BO.Engineer boEngineer = Read(id);
        DO.Engineer doEngineer = _dal.Engineer.Read(id)!;

        //Engineer that is active or already finish a task cannot be deleted
        if (boEngineer.Task != null || doEngineer.Active==true)
            throw new BO.BlCannotBeDeletedException($"Engineer with ID={boEngineer.Id} cannot be deleted");

        //delete engineer from the data base 
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch(DO.DalNotExistsException ex)
        {
            throw new BlDoesNotExistException($"Engineer with id={id} isn't exsit", ex);
        }
        
    }
    // a function that deletes the engineer
    public void Update(BO.Engineer boEngineer)
    // a function that updates the engineer
    {
        if (_dal.Engineer.Read(boEngineer.Id) == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} does Not exist");

        if (boEngineer.Name != null && boEngineer.Email != null && boEngineer.Cost > 0 && 
            (_dal.Engineer.Read(boEngineer.Id)!).Level<= (DO.EngineerExperience)boEngineer.Level && (int)boEngineer.Level<5)
            
        {
            
            

            //if there is an update in the Task of the Engineer, update the task only if it haven't done yet
            try {
                if(boEngineer.Task!=null)
                {
                    DO.Task? task = _dal.Task.Read(boEngineer.Task.Id);

                    if (task != null && task.CompleteDate == null)
                    {

                        _dal.Task.Update(task with
                        {
                            Alias = (boEngineer.Task).Alias,
                            EngineerId = boEngineer.Id,
                            Complexity = (DO.EngineerExperience)boEngineer.Level
                        }
                       );

                        // if the manager or the engineer assigned a task for the engineer active=true.
                        _dal.Engineer.Update(new DO.Engineer()
                        {

                            ID = boEngineer.Id,
                            Name = boEngineer.Name,
                            EMail = boEngineer.Email,
                            Level = (DO.EngineerExperience)boEngineer.Level,
                            Cost = boEngineer.Cost,
                            Active = true
                        }
                        );
                    }
                }
                  
                // if the manager or the engineer didn't assign a task for the engineer keep the original active.
                _dal.Engineer.Update(new DO.Engineer()
                {

                    ID = boEngineer.Id,
                    Name = boEngineer.Name,
                    EMail = boEngineer.Email,
                    Level = (DO.EngineerExperience)boEngineer.Level, 
                    Cost = boEngineer.Cost,
                    Active = _dal.Engineer.Read(boEngineer.Id)!.Active
                }
                );
            }
            catch (DO.DalNotExistsException ex)
            {
                throw new BlDoesNotExistException($"Doesn't exsit", ex);
            }




        }
        else
            throw new BO.BlInvalidDataException($"Engineer with ID={boEngineer.Id} invalid");





    }
    public BO.Engineer Read(int id)
    // a function that searches the engineer with that specific id

    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email=doEngineer.EMail,
            Level=(BO.EngineerExperience)doEngineer.Level,
            Cost=doEngineer.Cost
        };

    }
    // a function that returns an IEnumerable engineers
    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer, bool>? filter = null)
    // a function that returns IEnumerable list of the engineers

    {
        if (filter == null)
            return (from item in _dal.Engineer.ReadAll()
                    select new BO.Engineer
                    {
                        Id =item.ID,
                        Name = item.Name,
                        Email = item.EMail,
                        Level = (BO.EngineerExperience)item.Level,
                        Cost = item.Cost,
                        Task = _dal.Task.ReadAll()
                                    .Where(item => item.EngineerId == item.Id)
                                    .Select(item => new BO.TaskInEngineer
                                    {
                                        Id = item.Id,
                                        Alias = item.Alias
                                    })
                                    .FirstOrDefault()
                    });

        //return an IEnumerable<BO.Engineer> filtered by a filter
        else
            return (from item in _dal.Engineer.ReadAll()
                    where filter(item)
                    select new BO.Engineer
                    {
                        Id = item.ID,
                        Name = item.Name,
                        Email = item.EMail,
                        Level = (BO.EngineerExperience)item.Level,
                        Cost = item.Cost,
                        Task = _dal.Task.ReadAll()
                                    .Where(item => item.EngineerId == item.Id)
                                    .Select(item => new BO.TaskInEngineer
                                    {
                                        Id = item.Id,
                                        Alias = item.Alias
                                    })
                                    .FirstOrDefault()
                    });

    }

      

}

    

        