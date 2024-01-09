
namespace Dal;


    internal static class DataSource
    {
    internal static class Config
    {
        internal const int StartProjectId = 1;
        private static int NextProjectId = StartProjectId;
        internal static int UpdateProjectId { get => NextProjectId++; }

        internal const int StartTaskId = 1;
        private static int NextTaskId = StartTaskId;
        internal static int UpdateTaskId { get => NextTaskId++; }

        internal const int StartDependencyId = 1;
        private static int NextDependencyId = StartTaskId;
        internal static int UpdateDependencyId { get => NextDependencyId++; }
    }
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Task> Taskes { get; } = new();

}   





