

using DalApi;
using DO;

namespace Dal;

internal class EngineerImplementation: IEngineer
{
    readonly string s_engineers_xml = "engineers";

    public int Create(Engineer item)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);

        Engineers.Add(item);

        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);

        return item.ID;
    }

    public void Delete(int id)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        Engineers.RemoveAll(it => it.ID == id);
        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);

    }

    public Engineer? Read(int id)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        return Engineers.FirstOrDefault(item => item.ID == id);

    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        return Engineers.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {
        IEnumerable<Engineer> result;
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
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineers_xml);
        if (Engineers.RemoveAll(it => it.ID == item.ID) == 0)
            throw new DalNotExistsException($"Engineer with ID={item.ID} does not exist");
        Engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(Engineers, s_engineers_xml);
        
    }
}
