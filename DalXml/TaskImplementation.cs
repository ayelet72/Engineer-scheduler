

using DalApi;
using DO;

namespace Dal;

internal class TaskImplementation:ITask
{
    readonly string s_tasks_xml = "tasks";

    public int Create(DO.Task item)
    {
        List<DO.Task> Tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        int nextId = Config.NextTaskId;
        DO.Task copy = item with { Id = nextId };
        Tasks.Add(copy);

        XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);

        return item.Id;
    }

    public void Delete(int id)
    {
        List<DO.Task> Tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        if (Read(id) == null)
            throw new DalNotExistsException($"Task with ID={id} doesn't exsit");

        Tasks.RemoveAll(x => x.Id == id);
        XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);

    }

    public DO.Task? Read(int id)
    {
        List<DO.Task> Tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        return Tasks.FirstOrDefault(item => item.Id == id);

    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> Tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        return Tasks.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<DO.Task> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        IEnumerable<DO.Task> result;
        List<DO.Task> Tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
    
        if (filter == null)
            result = from item in Tasks
                     select item;

        //DataSource.Tasks.Where(filter);
        else
            result = from item in Tasks
                     where filter(item)
                     select item;


        return result;
    }

    public void Update(DO.Task item)
    {
        List<DO.Task> Tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        if (Read(item.Id) == null)
            throw new DalNotExistsException($"Task with ID={item.Id} doesn't exsit");


       Tasks.RemoveAll(i => i.Id == item.Id);
       Tasks.Add(item);
       XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);

    }
}
