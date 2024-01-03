
namespace Dal;

// במה startProjectid מאותחל
    internal static class DataSource
    {
    internal static class Config
    {
        internal const int startProjectId = 1000;       // ??
        private static int nextProjectId = startProjectId;
        internal static int NextProjectId { get => nextProjectId++; }
    }
    internal static List<DO.Engineer> Engineers { get; } = new();
        internal static List<DO.Dependency> Dependencies { get; } = new();
        internal static List<DO.Config> Configs { get; } = new();

}   





