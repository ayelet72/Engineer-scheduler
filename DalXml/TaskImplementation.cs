

using DalApi;
using DO;

namespace Dal;

internal class TaskImplementation: ITask   //With XMLSerializer for stage 3
{
    readonly string s_tasks_xml = "tasks";

    public int Create(DO.Task item)
    {
        //Load the list from XML to a temp list of Tasks
        List<DO.Task> Tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);

        //update the id by run number
        int nextId = Config.NextTaskId;

        //create a temp item
        DO.Task copy = item with { Id = nextId };
        
        //adding item to the new list
        Tasks.Add(copy);

        //save the new list into the XML
        XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);

        return nextId;
    }

    public void Delete(int id)
    {
        //Load the list from XML to a temp list of Tasks
        List<DO.Task> Tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        if (Read(id) == null)
            throw new DalNotExistsException($"Task with ID={id} doesn't exsit");

        //remove the item with the same id in the list
        Tasks.RemoveAll(x => x.Id == id);

        //save the new list into the XML
        XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);

    }

    public DO.Task? Read(int id)
    {
        //Load the list from XML to a temp list of Tasks
        List<DO.Task> Tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);

        //return the item with the same id if it exists. else return null
        return Tasks.FirstOrDefault(item => item.Id == id);
       

    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        //Load the list from XML to a temp list of Tasks
        List<DO.Task> Tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        return Tasks.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<DO.Task> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        IEnumerable<DO.Task> result;
        //Load the list from XML to a temp list of Tasks
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
        //Load the list from XML to a temp list of Tasks
        List<DO.Task> Tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_tasks_xml);
        if (Read(item.Id) == null)
            throw new DalNotExistsException($"Task with ID={item.Id} doesn't exsit");

        //remove the item with the same id
       Tasks.RemoveAll(i => i.Id == item.Id);

        //adding the update item to the end of the list
       Tasks.Add(item);

        //save the new list into the XML
        XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);

    }
}
