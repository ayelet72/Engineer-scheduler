using BlApi;
using BlImplementation;
using DalApi;
using DO;
using System.Collections;
using System.Reflection;
using System.Threading.Tasks;


namespace BO
{
    internal static class Tools
    {
        private static DalApi.IDal _dal = DalApi.Factory.Get;
        private static IBl bl = BlApi.Factory.Get();
        public static Status CalcStatus(int id)
        {
            //Unscheduled, Scheduled, OnTrack , InJeopardy, Done


            DO.Task? doTask = _dal.Task.Read(id);
            //checking what is the status of the task according to the datetime
            if (doTask != null)
            {
                if (doTask?.ScheduledDate == null)
                    return Status.Unscheduled;
                else if (doTask?.StartDate == null)
                    return Status.Scheduled;
                else if (doTask?.StartDate != null && doTask?.EngineerId != null && doTask?.CompleteDate == null)
                    return Status.OnTrack;
                else if (doTask?.CompleteDate != null)
                    return Status.Done;

            }
            //for control purposes
            return Status.Unscheduled;


        }
        public static List<TaskInList>? SetDependencies(int id)
        {
            //create list of TaskInList which contains all the tasks that the our task depends on

            return _dal.Dependency.ReadAll()
                .Where(item => item.DependentTask == id)
                .Select(item =>
                {
                    DO.Task? dependsOn = _dal.Task.Read(item.DependsOnTask);
                    return new TaskInList
                    {
                        Id = dependsOn!.Id,
                        Alias = dependsOn.Alias,
                        Status = CalcStatus(dependsOn.Id),
                        Description = dependsOn.Description
                        
                    };
                })
                .ToList();
        }
        public static BO.EngineerInTask? CreateEngineerInTask(int id)
        {
            //BO.EngineerInTask? engineer = null;
            DO.Task? temp = _dal.Task.Read(id);
            if (temp != null && temp.EngineerId != 0)
            {
                string? name = (_dal.Engineer.Read(temp.EngineerId))?.Name;
                int tempId = (_dal.Engineer.Read(temp.EngineerId))!.ID;
                return new EngineerInTask
                {
                    Name = name,
                    Id = tempId
                };
            }
            return null;
        }


        // a help boolen method 
        public static bool IsTaskLinkedToOtherTasks(int id)
        {


            IEnumerable<BO.Task> tasks = bl.Task.ReadAll();
            bool hasDependency = tasks.Any(item => item.Dependencies?.Any(dep => dep.Id == id) == true);
            return hasDependency;
           
        }

        public static void AddDependency(int id, int dependentOnTask)
        {

            _dal.Dependency.Create(new DO.Dependency
            (ID: 0,
            DependentTask: id,                      //משימה שתלויה ב...
            DependsOnTask: dependentOnTask          //המשימה שתלויים בה

            )
            );
        }
    
        public static DateTime? CheckStartDate(BO.Task boTask)
        {
            if (boTask.ScheduledDate > boTask.StartDate)
                throw new BO.BlInvalidDateException($"Startproject is beforeScheduledDate");
            if (boTask.Dependencies != null)
            {
                bool t = boTask.Dependencies.Any(x => _dal.Task.Read(x.Id)?.StartDate > boTask.StartDate);
                if (t)
                    throw new BO.BlInvalidDateException($"Scheduled date is before Scheduled date of a dependes on task");
            }
            return boTask.StartDate;
        }

        public static string ToStringProperty<T>(this T obj, string str = "")
        {

            foreach (PropertyInfo item in obj.GetType().GetProperties())
            {
                object? value = item.GetValue(obj);
                if (value != null)
                {
                    if (value is IEnumerable enumerable && !(value is string))
                    {
                        str += "\n" + item.Name + ": ";
                        foreach (var objInList in enumerable)
                        {
                            if (objInList != null)
                            {
                                str += objInList.ToString() + ", ";
                            }
                        }
                    }
                    else
                    {
                        str += "\n" + item.Name + ": " + value.ToString() + ", ";
                    }
                }
            }
            return str;
        
        }

        public static DateTime? CheckCompleteDate(Task boTask)
        {
            if (boTask.StartDate > boTask.CompleteDate || boTask.CompleteDate > bl.EndProject)
                throw new BO.BlInvalidDateException($"CompleteDate isn't in the correct span time");
            if (boTask.Dependencies != null)
            {
                bool t = boTask.Dependencies.Any(x => _dal.Task.Read(x.Id)?.CompleteDate > boTask.CompleteDate);
                if (t)
                    throw new BO.BlInvalidDateException($"Complete date is before Complete date of a dependes on task");
            }


            return boTask.CompleteDate;
        }

        // Calculate the possible RequierdEffortTime
        public static TimeSpan? SetRequiredEffortTime(Task boTask)
        {


            var dependencies = _dal.Dependency.ReadAll(x => x.DependsOnTask == boTask.Id).ToList();

            var t = dependencies.Max(x => _dal.Task.Read(x.DependentTask)!.ScheduledDate);

            TimeSpan? calcTime = t - boTask.ScheduledDate;

            return calcTime;

        }
    }
}
