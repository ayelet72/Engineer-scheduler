

namespace DalTest;
using DO;
using DalApi;
using static System.Runtime.InteropServices.JavaScript.JSType;

public static class Initialization
{
    private static IEngineer? s_dalEngineer;            //stage 1
    private static IDependency? s_dalDependency;        //stage 1
    private static ITask? s_dalTask;                    //stage 1

    private static readonly Random s_rand = new();

    private static void createEngineers()
    {


        string[] nameEngineer = { "Mosh", "Dan", "Jake", "Gil", "Noa" };
        string[] emailEngineer = { "mosh@jct.com", "dan@jct.com", "jake@jct.com", "gil@jct.com", "noa@jct.com" };
        EngineerExperience[] levelEngineer = { EngineerExperience.Advanced, EngineerExperience.Beginner, EngineerExperience.AdvancedBeginner, EngineerExperience.Expert, EngineerExperience.Intermediate };

        for (int i = 0; i < 5; i++)
        { 
            //Beginner,AdvancedBeginner,Intermediate, Advanced ,Expert

            int numID = s_rand.Next(100000000, 999999999);
            int numCost=0;
            if (levelEngineer[i]== EngineerExperience.Expert)
            {
                 numCost = s_rand.Next(800, 1000);
            }
            if (levelEngineer[i] == EngineerExperience.Beginner)
            {
                 numCost = s_rand.Next(100, 200);
            }
            if (levelEngineer[i] == EngineerExperience.AdvancedBeginner)
            {
                 numCost = s_rand.Next(200, 400);
            }

            if (levelEngineer[i] == EngineerExperience.Advanced)
            {
                numCost = s_rand.Next(600, 800);
            }
            if (levelEngineer[i] == EngineerExperience.Intermediate)
            {
                numCost = s_rand.Next(400, 600);
            }
            Engineer NewEngineer = new Engineer(numID, emailEngineer[i], nameEngineer[i], numCost, levelEngineer[i]);
            s_dalEngineer!.Create(NewEngineer);
        }
    }

    private static void createTaskes()
    {


        string[] description = {"User Registration and Authentication", "Expense Entry Form",
                                "Expense Categories", "Expense List View", "Data Storage", "Monthly Expense Report",
                                "Expense Chart", "Budget Setting", "Expense Notifications", "Currency Localization",
                                "Export Data", "Search and Filtering", "User Profile" , "Expense Editing and Deletion",
                                "Data Security Measures", "Responsive Design", "Offline Mode", "Feedback and Ratings",
                                "Expense Analytics", "Testing and Bug Fixes" };

        EngineerExperience experience;
        DateTime dateTime;

        for (int i = 0; i < 20; i++)
        {
             dateTime = DateTime.Now.AddDays(-s_rand.Next(60) - 20);
            experience = (EngineerExperience)s_rand.Next(5);
            s_dalTask!.Create(new Task(
                Id:0,
                EngineerId:0,
                Complexity: experience,
                Alias: $"Task #{i}",
                Description: description[i],
                CreateAtDate: dateTime
                ));
     


        }
    }
    private static void createDependcies()
    {
        Tuple<int, int>[] pairsArray = new Tuple<int, int>[]
        {
            Tuple.Create(2,1),Tuple.Create(3,2),Tuple.Create(4,2),Tuple.Create(5,1),Tuple.Create(5,2),
            Tuple.Create(5,3),Tuple.Create(5,4),Tuple.Create(6,4),Tuple.Create(7,4),Tuple.Create(8,3),
            Tuple.Create(9,4),Tuple.Create(9,8),Tuple.Create(11,4),Tuple.Create(12,4),Tuple.Create(13,1),
            Tuple.Create(14,4),Tuple.Create(15,5),Tuple.Create(15,1),Tuple.Create(17,5),Tuple.Create(18,1),
            Tuple.Create(19,6),Tuple.Create(20,1),Tuple.Create(20,2),Tuple.Create(20,3),Tuple.Create(20,4),
            Tuple.Create(20,5),Tuple.Create(20,6),Tuple.Create(20,7),Tuple.Create(20,8),Tuple.Create(20,9),
            Tuple.Create(20,10),Tuple.Create(20,11),Tuple.Create(20,12),Tuple.Create(20,13),Tuple.Create(20,14),
            Tuple.Create(20,15),Tuple.Create(20,16),Tuple.Create(20,17),Tuple.Create(20,18),Tuple.Create(20,19)

        };

        for (int i = 0; i < 40; i++)
        {
            Dependency NewDependency = new Dependency(0, pairsArray[i].Item1, pairsArray[i].Item2); ;
            s_dalDependency!.Create(NewDependency);
        }
    }
    public static void Do(IEngineer? engineer, ITask? task, IDependency? dependency)
    {
        s_dalEngineer = engineer ?? throw new NullReferenceException("DAL can not be null!");

        s_dalTask = task ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dependency ?? throw new NullReferenceException("DAL can not be null!");

        createEngineers();
        createTaskes();
        createDependcies();


    }
}
