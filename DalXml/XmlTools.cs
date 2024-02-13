namespace Dal;

using DO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static DO.DalDeletionImpossible;

static class XMLTools
{
    const string s_xml_dir = @"..\xml\";
    static XMLTools()
    {
        if (!Directory.Exists(s_xml_dir))
            Directory.CreateDirectory(s_xml_dir);
    }
    internal static void InitNextId(string configPath, string nextId, string startId)
    {
        string filePath = $"{s_xml_dir + configPath}.xml";
        XElement? configXml;
        try
        {
            configXml = XElement.Load(filePath);
            string startVal=configXml.Element(startId)!.Value;
            configXml.Element(nextId)!.SetValue(startVal);
            configXml.Save(filePath);
        }

        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file:{s_xml_dir + configPath},{ex.Message}");



        }
    }
    //internal static void InitNextId(string configPath, string nextId, string startId)
    //{
    //    string filePath = $"{s_xml_dir + configPath}.xml";
    //    XElement? configXml;
    //    try
    //    {
    //        configXml = XElement.Load(filePath);
    //        string startVal = configXml.Element(startId)!.Value;
    //        configXml.Element(nextId)!.SetValue(startVal);
    //        configXml.Save(filePath);
    //    }

    //    catch (Exception ex)
    //    {
    //        throw new DalXMLFileLoadCreateException($"fail to load xml file:{s_xml_dir + configPath},{ex.Message}");



    //    }
    //}
    #region Extension Fuctions
    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;
    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
        DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;
    public static double? ToDoubleNullable(this XElement element, string name) =>
        double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;
    public static int? ToIntNullable(this XElement element, string name) =>
        int.TryParse((string?)element.Element(name), out var result) ? (int?)result : null;
    #endregion

    #region XmlConfig
    public static int GetAndIncreaseNextId(string data_config_xml, string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        int nextId = root.ToIntNullable(elemName) ?? throw new FormatException($"can't convert id.  {data_config_xml}, {elemName}");
        root.Element(elemName)?.SetValue((nextId + 1).ToString());
        XMLTools.SaveListToXMLElement(root, data_config_xml);
        return nextId;
    }
    #endregion

    #region SaveLoadWithXElement
    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }
    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);
            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }
    #endregion

    #region SaveLoadWithXMLSerializer
    public static void SaveListToXMLSerializer<T>(List<T> list, string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            new XmlSerializer(typeof(List<T>)).Serialize(file, list);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }
    public static List<T> LoadListFromXMLSerializer<T>(string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T>));
            return x.Deserialize(file) as List<T> ?? new();
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {filePath}, {ex.Message}");
        }
    }

    internal static DateTime? GetStartProject(string s_data_config_xml, string elementName)
    {
        XElement element = XMLTools.LoadListFromXMLElement(s_data_config_xml);
        element = element.Element(elementName) ?? throw new Exception();
        DateTime.TryParse(element.Value.ToString()==""? null: element.Value.ToString(), out  DateTime startProject);
        return startProject ;
    }

    internal static void  SetStartProject(string s_data_config_xml,string elementName ,DateTime? StartProject)
    {
        XElement element = XMLTools.LoadListFromXMLElement(s_data_config_xml);
        element.Element("StartProject")!.SetValue(StartProject.ToString());
        SaveListToXMLElement(element, s_data_config_xml);
    }
    internal static void SetEndProject(string s_data_config_xml, string elementName, DateTime? EndProject)
    {
        XElement element = XMLTools.LoadListFromXMLElement(s_data_config_xml);
        element.Element("EndProject")!.SetValue(EndProject.ToString());
        SaveListToXMLElement(element, s_data_config_xml);
    }
    internal static DateTime? GetEndProject(string s_data_config_xml, string elementName)
    {
        XElement element = XMLTools.LoadListFromXMLElement(s_data_config_xml);
        element = element.Element(elementName) ?? throw new Exception();
        DateTime.TryParse(element.Value.ToString() == "" ? null : element.Value.ToString(), out DateTime endProject);
        return endProject;
    }

    internal static void InitDate(string s_data_config_xml, string start, string end)
    {
        DateTime initDate = DateTime.MinValue;
        XElement element1 = XMLTools.LoadListFromXMLElement(s_data_config_xml);
        element1.Element("EndProject")?.SetValue(DateTime.MinValue.ToString());
        SaveListToXMLElement(element1, s_data_config_xml);
        XElement element2 = XMLTools.LoadListFromXMLElement(s_data_config_xml);
        element2.Element("StartProject")?.SetValue(DateTime.MinValue.ToString());
        SaveListToXMLElement(element2, s_data_config_xml);

    }
    #endregion
}