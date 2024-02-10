using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

sealed internal class DalXml : IDal

{
    public DateTime? StartProject { get { return Config.StartProject; } set { Config.StartProject = value; } }
    public DateTime? EndProject { get { return Config.EndProject; } set { Config.EndProject = value; } }

    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public void Reset()
    {
        XMLTools.SaveListToXMLElement(new XElement("ArrayOfDependency"), "dependencies");
        XMLTools.SaveListToXMLElement(new XElement("ArrayOfTask"), "tasks");
        XMLTools.SaveListToXMLElement(new XElement("ArrayOfEngineer"), "engineers");

        Config.InitTaskID();
        Config.InitDependencyID();
        
    }
}
