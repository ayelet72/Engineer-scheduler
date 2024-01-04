
namespace Dal;

// במה startProjectid מאותחל
    internal static class DataSource
    {
    internal static class Config
    {
        internal const int StartProjectId = 1;
        private static int nextProjectId = StartProjectId;
        internal static int NextProjectId { get => NextProjectId++; }

        internal const int StartTaskId = 1;
        private static int NextTaskId = StartTaskId;
        internal static int NextTaskId { get => NextTaskId++; }

        internal const int StartTaskId = 1;
        private static int NextTaskId = StartTaskId;
        internal static int NextTaskId { get => NextTaskId++; }
    }
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Config> Configs { get; } = new();

}   





