

namespace DalTest;
using DO;
using DalApi;

public static class Initialization
{
    private static IEngineer? s_dalEngineer;            //stage 1
    private static IDependency? s_dalDependency;        //stage 1
    private static ITask? s_dalTask;                    //stage 1

    private static readonly Random s_rand = new();

    private static void createEngineers()
    {
 
        int[] arrID = { s_rand.Next(100000000, 999999999), s_rand.Next(100000000, 999999999), s_rand.Next(100000000, 999999999), s_rand.Next(100000000, 999999999), s_rand.Next(100000000, 999999999) };
        //int[] arrCost = { s_rand.Next(100, 1000), s_rand.Next(100, 1000), s_rand.Next(100, 1000), s_rand.Next(100, 1000), s_rand.Next(100, 1000) };
        string[] nameEngineer = {"Mosh", "Dan","Jake","Gil", "Noa" };
        string[] emailEngineer = { "mosh@jct.com", "dan@jct.com", "jake@jct.com", "gil@jct.com", "noa@jct.com" };
        EngineerExperience[] levelEngineer = { EngineerExperience.Advanced, EngineerExperience.Beginner, EngineerExperience.AdvancedBeginner, EngineerExperience.Expert, EngineerExperience.Intermediate };

        for (int i = 0; i <5;  i++)
        {
            Engineer NewEngineer = new Engineer(arrID[i], emailEngineer[i], nameEngineer[i],levelEngineer[i]);
            s_dalEngineer!.Create(NewEngineer);
        }
    }

    private static void createTaskes()
    {

        int[] arrID = { s_rand.Next(100000000, 999999999), s_rand.Next(100000000, 999999999), s_rand.Next(100000000, 999999999), s_rand.Next(100000000, 999999999), s_rand.Next(100000000, 999999999) };
        //int[] arrCost = { s_rand.Next(100, 1000), s_rand.Next(100, 1000), s_rand.Next(100, 1000), s_rand.Next(100, 1000), s_rand.Next(100, 1000) };
        string[] nameEngineer = { "Mosh", "Dan", "Jake", "Gil", "Noa" };
        string[] emailEngineer = { "mosh@jct.com", "dan@jct.com", "jake@jct.com", "gil@jct.com", "noa@jct.com" };
        EngineerExperience[] levelEngineer = { EngineerExperience.Advanced, EngineerExperience.Beginner, EngineerExperience.AdvancedBeginner, EngineerExperience.Expert, EngineerExperience.Intermediate };

        for (int i = 0; i < 20; i++)
        {
            Task NewTask = new Engineer(arrID[i], emailEngineer[i], nameEngineer[i], levelEngineer[i]);
            s_dalTask!.Create(NewTask);
        }
    }


}
