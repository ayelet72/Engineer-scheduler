
namespace Dal;


internal static class DataSource
{
    internal static class Config
    {

        internal const int StartTaskId = 1;
        private static int nextTaskId = StartTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

        internal const int StartDependencyId = 1;
        private static int nextDependencyId = StartTaskId;
        internal static int NextDependencyId { get => nextDependencyId++; }
        internal static DateTime? Startproject { get; set; }
        internal static DateTime? EndProject { get; set; }
        //internal static void ResetDependencyID() { nextDependencyId = StartDependencyId; }
        //internal static void ResetTaskID() { nextTaskId = StartTaskId; }
        //internal static void ResetDate() { Startproject = DateTime.MinValue; EndProject = DateTime.MinValue; }

    }
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
    

}





