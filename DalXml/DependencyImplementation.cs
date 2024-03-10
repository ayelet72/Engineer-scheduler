

using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Dal;

internal class DependencyImplementation : IDependency //With XElement for stage 3
{
    readonly string s_dependencies_xml = "dependencies";

    public int Create(Dependency item)
    {
        int nextId = Config.NextDependencyId;

        //load the XElement list from XML
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependencies_xml);

        //creating a element from type XElement that has the same properties as item
        XElement element = new XElement("Dependency",
                                new XElement("ID", nextId),
                                new XElement("DependentTask", item.DependentTask),
                                new XElement("DependsOnTask", item.DependsOnTask)
                                );
        //adding element to the list dependencies
        dependencies.Add(element);

        //save the new XElement list in the XML
        XMLTools.SaveListToXMLElement(dependencies, s_dependencies_xml);

        return nextId;
    }

    public void Delete(int id)
    {
        //load the XElement list from XML
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependencies_xml);

        //creating a temp XElement that its id equal to id that the func gets
        XElement? toDelete = dependencies.Elements("Dependency").FirstOrDefault(x => (int?)x.Element("ID") == id);

        //item isn't exsits
        if (toDelete == null)
            throw new DalNotExistsException($"Dependency with {id} does not exist!!!");

        //if item exsits, remove it from dependencies
        toDelete.Remove();

        //save the new XElement list in the XML
        XMLTools.SaveListToXMLElement(dependencies, s_dependencies_xml);
    }

    public Dependency? Read(int id)
    {
        //load the XElement list from XML
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependencies_xml);

        //creating a temp XElement that its id equal to id that the func gets
        XElement? findElement = dependencies.Elements("Dependency").FirstOrDefault(x => (int?)x.Element("ID") == id);
        if (findElement != null)
        {
            //  Dependency class has properties like ID, etc.
            int dependencyId = (int)findElement.Element("ID")!;
            int dependencyTask = (int)findElement.Element("DependentTask")!;
            int dependencyOnTask = (int)findElement.Element("DependsOnTask")!;

            // Create a new Dependency object
            Dependency dependency = new Dependency
            {
                ID = dependencyId,
                DependentTask = dependencyTask,
                DependsOnTask = dependencyOnTask

            };

            return dependency;
        }

        // If the element with the specified ID is not found, return null
        return null;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        //load the XElement list from XML
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependencies_xml);

        //  Dependency class has properties like ID, etc.
        Dependency? findElement = dependencies
            .Elements("Dependency")
            .Select(element => new Dependency
            {
                ID = (int)element.Element("ID")!,
                DependentTask = (int)element.Element("DependentTask")!,
                DependsOnTask = (int)element.Element("DependsOnTask")!

            })
            .FirstOrDefault(filter);

        return findElement;

    }

    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        //load the XElement list from XML
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependencies_xml);
        IEnumerable<Dependency> allDependencies = dependencies
       .Elements("Dependency")
       .Select(element => new Dependency
       {
           ID = (int)element.Element("ID")!,
           DependentTask = (int)element.Element("DependentTask")!,
           DependsOnTask = (int)element.Element("DependsOnTask")!
       });

        if (filter != null)
        {

            allDependencies = allDependencies.Where(filter);
        }

        return allDependencies.ToList();

    }

    public void Update(Dependency item)
    {

        //load the XElement list from XML
        XElement dependencies = XMLTools.LoadListFromXMLElement(s_dependencies_xml);

        XElement? toUpdate = dependencies.Elements("Dependency").FirstOrDefault(x => (int?)x.Element("ID") == item.ID);

        if (toUpdate == null)
            throw new DalNotExistsException($"Dependency with {item.ID} does not exist!!!");

        toUpdate.ReplaceAll(item);

        //save the new XElement list in the XML
        XMLTools.SaveListToXMLElement(dependencies, s_dependencies_xml);
    }
    public void Reset()
    {
        List<Dependency> Dependencies = XMLTools.LoadListFromXMLSerializer<Dependency>(s_dependencies_xml);
        Dependencies.Clear();
        XMLTools.SaveListToXMLSerializer(Dependencies, s_dependencies_xml);
        Config.InitDependencyID();

    }
}
