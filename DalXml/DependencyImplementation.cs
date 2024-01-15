

using DalApi;
using DO;

namespace Dal;

internal class DependencyImplementation:IDependency
{
    readonly string s_dependencies_xml = "dependencies";

    public int Create(Dependency item)
    {
        List<Dependency> Dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependencies_xml);
        int nextId = Config.NextDependencyId;
        Dependency copy = item with { Id = nextId };
        Dependencies.Add(copy);

        XMLTools.SaveListToXMLSerializer(Dependencies, s_dependencies_xml);

        return item.Id;
    }

    public void Delete(int id)
    {
        List<Dependency> Dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependencies_xml);
        var itemToDelete = Dependencies.Single(item => item.ID == id);           //stage 2
        Dependencies.Remove(itemToDelete);
        XMLTools.SaveListToXMLSerializer(Dependencies, s_dependencies_xml);
    }

    public Dependency? Read(int id)
    {
        List<Dependency> Dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependencies_xml);
        return Dependencies.FirstOrDefault(item => item.Id == id);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        List<Dependency> Dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependencies_xml);
        return Dependencies.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        List<Dependency> Dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependencies_xml);
        IEnumerable<Dependency> result;
        
        if (filter == null)
            result = from item in Dependencies
                     select item;

        
        else
            result = from item in Dependencies
                     where filter(item)
                     select item;


        return result;
    }

    public void Update(Dependency item)
    {
        List<Dependency> Dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependencies_xml);
        Delete(item.ID);
        Dependencies.Add(item);
        XMLTools.SaveListToXMLSerializer(Dependencies, s_dependencies_xml);
    }
}
