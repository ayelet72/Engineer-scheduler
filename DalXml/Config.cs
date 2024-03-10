using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal static class Config
{
    static string s_data_config_xml = "data-config";

    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static DateTime? StartProject
    {
        get => XMLTools.GetStartProject(s_data_config_xml, "StartProject");
        set => XMLTools.SetStartProject(s_data_config_xml, "StartProject", value);
    }
    internal static DateTime? EndProject
    {
        get => XMLTools.GetStartProject(s_data_config_xml, "EndProject");
        set => XMLTools.SetEndProject(s_data_config_xml, "EndProject", value);
    }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }
    internal static void InitTaskID() => XMLTools.InitNextId(s_data_config_xml, "NextTaskId", "InitTaskID");
    internal static void InitDependencyID() => XMLTools.InitNextId(s_data_config_xml, "NextDependencyId", "InitDependencyID");
    internal static void InitStartAndEndProject() => XMLTools.InitDate(s_data_config_xml, "StartProject", "EndProject");
    
    // internal static void InitEndProject() => XMLTools.InitNextId(s_data_config_xml, "NextDependencyId", "InitDependencyID");



}
