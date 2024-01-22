

using DalApi;
using DO;

namespace Dal;

internal class EngineerImplementation: IEngineer //With XMLSerializer for stage 3
{
    readonly string s_engineers_xml = "engineers";

    public int Create(Engineer item)
    {
        //Load the list from XML to a temp list of enigneers
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        //adding the new item to the temp list
        Engineers.Add(item);

        //save the new list into the XML
        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);

        return item.ID;
    }

    public void Delete(int id)
    {
        //Load the list from XML to a temp list of enigneers
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        //Remove the correct item with the same id from the list
        Engineers.RemoveAll(it => it.ID == id);

        //save the new list into the XML
        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);

    }

    public Engineer? Read(int id)
    {
        //Load the list from XML to a temp list of enigneers
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        //Return the item with the same id if it exists . else return null
        return Engineers.FirstOrDefault(item => item.ID == id);

    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        //Load the list from XML to a temp list of enigneers
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        //Return the item . else return null
        return Engineers.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {
        
        IEnumerable<Engineer> result;

        //Load the list from XML to a temp list of enigneers
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
       
        if (filter == null)
            result = from item in Engineers
                     select item;

        //DataSource.Engineers.Where(filter);
        else
            result = from item in Engineers
                     where filter(item)
                     select item;


        return result;
    }

    public void Update(Engineer item)
    {
        //Load the list from XML to a temp list of enigneers
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        //remove the item with the same id
        if (Engineers.RemoveAll(it => it.ID == item.ID) == 0)
            throw new DalNotExistsException($"Engineer with ID={item.ID} does not exist");

        //adding the update item to the end of the list
        Engineers.Add(item);

        //save the new list into the XML
        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);
        
    }
}
