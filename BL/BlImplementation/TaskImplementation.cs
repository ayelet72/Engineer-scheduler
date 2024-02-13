
using BlApi;
using BO;

using DO;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private IBl bl =  BlApi.Factory.Get();
       
    public int Create(BO.Task boTask)
    // a function that create a task

    {
        //cheking if engineer is exsit and if the input data is valid
        if (boTask.Alias == null || boTask.Description == null)
            throw new BO.BlInvalidDataException($"Invalid data ");


        try
        {

            // if there are dependencies add them too
            if (boTask.Dependencies != null)
            {
                var result = (boTask.Dependencies).ToList();
                result.ForEach(item => Tools.AddDependency(boTask.Id, item.Id));
            }

            //creating a new task in the data base
            int numId = _dal.Task.Create(new DO.Task


            (Id: 0,
            Alias: boTask.Alias,
            EngineerId:0,
            Description: boTask.Description,
            CreateAtDate: boTask.CreateAtDate,
            Complexity: boTask.Complexity,
            RequiredEffortTime: boTask.RequiredEffortTime,
            Remarks: boTask.Remarks,
            Deliverables: boTask.Deliverables

            ));
            
            
            return numId;
        }
        catch (DO.DalExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }
        
            throw new BO.BlInvalidDataException($"Task with ID={boTask.Id} is invalid");

    }

    public void Delete(int id)
    // a function that deletes a task 

    {
        //if (_dal.Task.Read(id) == null)
        //    throw new BO.BlDoesNotExistException($"Task with ID={id} doesn't exist");

        //using a help boolen method that checks if the task is linked to other tasks
        if (Tools.IsTaskLinkedToOtherTasks(id))
            throw new BO.BlCannotBeDeletedException($"Task with ID={id} is linked to other tasks");
        try
        {
            _dal.Task.Delete(id);
        }
        catch(DalNotExistsException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} doesn't exists", ex);
        }

    }

    public BO.Task Read(int id)
    // a function that searches a task in the list and returns a BO obj

    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
       
            return new BO.Task()
            {
                Id = id,
                Alias = doTask.Alias,
                Description = doTask.Description,
                CreateAtDate = doTask.CreateAtDate,
                Status = Tools.CalcStatus(id),
                Complexity = doTask.Complexity,
                RequiredEffortTime = doTask.RequiredEffortTime,
                Dependencies = Tools.SetDependencies(id),
                StartDate = doTask.StartDate,
                ScheduledDate = doTask.ScheduledDate,
                CompleteDate = doTask.CompleteDate,
                ForCastDate = doTask.StartDate > doTask.ScheduledDate ?
                            doTask.StartDate + doTask.RequiredEffortTime : doTask.ScheduledDate + doTask.RequiredEffortTime,
                Deliverables = doTask.Deliverables,
                Remarks = doTask.Remarks,
                Engineer = Tools.CreateEngineerInTask(id)

            };
   
        
    }

   
    public void Update(BO.Task boTask)
    // a function that update the task

    {
        DO.Task task = _dal.Task.Read(boTask.Id) ??
            throw new BO.BlDoesNotExistException($"Task with ID={boTask.Id} does Not exist");
        DO.Engineer engineer = _dal.Engineer.Read(boTask.Engineer!.Id) ??
            throw new BO.BlDoesNotExistException($"Engineer with ID={boTask.Engineer.Id} does Not exist");
        
        if (boTask.Alias == null || boTask.Description == null || engineer.Level < task.Complexity)
            throw new BO.BlInvalidDataException($"Invalid data "); 


        //TODO start end date
        if (bl.StartProject == null)
        {
            // update engineer too
            try {
                _dal.Engineer.Update(engineer with
                {
                    Active = true
                }
                );
                //if there is no scheduel project it is possible to update this fields:
                _dal.Task.Update(task with
                {
                    Alias = boTask.Alias,
                    EngineerId = boTask.Engineer.Id,
                    Description = boTask.Description,
                    StartDate = Tools.CheckStartDate(boTask),
                    CompleteDate = Tools.CheckCompleteDate(boTask),
                    Complexity = boTask.Complexity,
                    RequiredEffortTime = boTask.RequiredEffortTime, //Tools.SetRequiredEffortTime(boTask),
                    Remarks = boTask.Remarks,
                    Deliverables = boTask.Deliverables
                });
            }
            catch(DO.DalNotExistsException ex)
            {
                throw new BlDoesNotExistException($"doesn't exsit", ex);
            }
   

            //if there is an update in the dependencies field of BO.Task

            List<Dependency> dependency = _dal.Dependency.ReadAll(x => x.DependentTask == boTask.Id).ToList();

            //case 1: there is a need to add a dependency in the data base
            var t = from dep in boTask.Dependencies
                    let index = dependency.FindIndex(x => x.DependsOnTask == dep.Id) //using LINQ - let: for definding bool index 
                    where index==-1
                    select _dal.Dependency.Create(new DO.Dependency()
                    {
                        DependentTask = boTask.Id,
                        DependsOnTask = dep.Id
                    });

            //case 2: there is a needed to delete a dependency in the date base
            if (boTask.Dependencies != null)
            {
                var removeList = from dep in dependency
                                 where boTask.Dependencies.FindIndex(x => x.Id != dep.DependsOnTask) == -1
                                 select dep;
                try
                {
                    if (removeList.Any())
                        removeList.ToList().ForEach(x => _dal.Dependency.Delete(x.ID));
                }
                catch(DO.DalNotExistsException ex)
                {
                    throw new BlDoesNotExistException($"doesn't exsit", ex);
                }

            }
        }

        //if there is scheduel it is possible to update only these fields:
        else
        {
            try
            {
                _dal.Engineer.Update(engineer with
                {
                    Active = true
                });


                _dal.Task.Update(task with
                {
                    Alias = boTask.Alias,
                    EngineerId = boTask.Engineer.Id,
                    Description = boTask.Description,
                    Complexity = boTask.Complexity,
                    Remarks = boTask.Remarks,
                    Deliverables = boTask.Deliverables
                });
            }
            catch (DO.DalNotExistsException ex)
            {
                throw new BlDoesNotExistException($"doesn't exsit", ex);
            }


        }

        throw new BO.BlInvalidDataException($"the input data is invalid");
    }

    public IEnumerable<BO.Task> ReadAll(Func<DO.Task, bool>? filter = null)
    // a function that return all the tasks in IEnumerable 
    {


        if (filter == null)
            return (from item in _dal.Task.ReadAll()
                    select new BO.Task
                    {
                        Id = item.Id,
                        Alias = item.Alias,
                        Status=Tools.CalcStatus(item.Id),
                        Dependencies=Tools.SetDependencies(item.Id),
                        Description = item.Description,
                        CreateAtDate = item.CreateAtDate,
                        ScheduledDate=item.ScheduledDate,
                        RequiredEffortTime=item.RequiredEffortTime,
                        Engineer=Tools.CreateEngineerInTask(item.Id),
                        Complexity = item.Complexity,
                        CompleteDate=item.CompleteDate

                    });

        //return an IEnumerable<BO.Task> filtered by a filter
        else
            return ( from item in _dal.Task.ReadAll()
                     where filter(item)
                     select new BO.Task
                     {
                         Id = item.Id,
                         Alias = item.Alias,
                         Status = Tools.CalcStatus(item.Id),
                         Dependencies = Tools.SetDependencies(item.Id),
                         Description = item.Description,
                         CreateAtDate = item.CreateAtDate,
                         RequiredEffortTime = item.RequiredEffortTime,
                         ScheduledDate = item.ScheduledDate,
                         Engineer = Tools.CreateEngineerInTask(item.Id),
                         Complexity = item.Complexity,
                         CompleteDate = item.CompleteDate
                     });
        

    }
    
    public void Update(int id, DateTime scheduelTime)
    // a function that update the scheduel time
    {
        DO.Task task = _dal.Task.Read(id) ??
             throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        //if there is a start date of the project
        if (bl.StartProject!=DateTime.MinValue)
        {
            //it is not possible that schedele date of task will be before the start date of project
            if (bl.StartProject > scheduelTime || bl.EndProject < scheduelTime|| task.CreateAtDate > scheduelTime )
                throw new BO.BlInvalidDateException($"Scheduled date isn't in the time span correctly");
           

        }
        var boTask = Read(id);
        //checking with the tasks the task depedent on 
        if (boTask.Dependencies != null)
        {
            bool t = boTask.Dependencies.Any(x => Read(x.Id)?.ScheduledDate==null);
            if (t)
                throw new BO.BlDoesNotExistException($"The scheduled date of the task that the task depends on does not exist ");

             t = boTask.Dependencies.Any(x => Read(x.Id)?.ForCastDate > boTask.ScheduledDate);
            if (t)
                throw new BO.BlInvalidDateException($"Scheduled date is before ForCastDate date of a dependes on task");

        }

        //If it passed all the normality tests it can be update
        try
        {
            _dal.Task.Update(task with
            {
                ScheduledDate = scheduelTime
            });
        }
        catch (DO.DalNotExistsException ex)
        {
            throw new BlDoesNotExistException($"doesn't exsit", ex);
        }


    }
}


