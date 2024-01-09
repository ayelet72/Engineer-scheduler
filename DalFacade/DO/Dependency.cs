
namespace DO;
/// <summary>
/// הערה: לשאול את המרצה לגבי בנאי פרמטרים אם זה אוטומטי, דבר שני מה המטרה של הישות
/// </summary>
/// <param name="ID">id of</param>
/// <param name="DependentTask"></param>
/// <param name="DependsOnTask"></param>
public record Dependency
(
int ID,
int DependentTask,
int DependsOnTask
)

{
    public Dependency() : this(0,0,0) { }       //empty ctor for stage 1
    public int Id { get; set; }
    public int DEpendentTask { get; set;}
    public int DEpendsOnTask { get;set;}

}