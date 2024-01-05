using DalApi;
using DO;
using Dal;

namespace DalTest
{
    public class Program
    {
        private static IEngineer? s_dalEngineer= new EngineerImplementation();           //stage 1
        private static ITask? s_dalTask = new TaskImplementation();                      //stage 1
        private static IDependency? s_dalDependency= new DependencyImplementation();    //stage 1
        public static void MainMenu()
        {
            Console.WriteLine("Select a PDS you want to check:");
            Console.WriteLine("0. Exit main menu");
            Console.WriteLine("1. PDS 1");
            Console.WriteLine("2. PDS 2");
            Console.WriteLine("3. PDS 3");

        }
        public static void SubMenu()
        {
            Console.WriteLine("1. Exit main menu");
            Console.WriteLine("2. Add new item");
            Console.WriteLine("3. Read item");
            Console.WriteLine("4. ReadAll items");
            Console.WriteLine("5. Update item");
            Console.WriteLine("5. Delete item");

        }
        public static void AddPDS()
        {
            

        }
        private static void Main(string[] args)
        {
            Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
            int choice;
           
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Monday");
                    break;
                case 2:
                    Console.WriteLine("Tuesday");
                    break;
                case 3:
                    Console.WriteLine("Wednesday");
                    break;
                case 4:
                    Console.WriteLine("Thursday");
                    break;
                case 5:
                    Console.WriteLine("Friday");
                    break;
                case 6:
                    Console.WriteLine("Saturday");
                    break;
                case 7:
                    Console.WriteLine("Sunday");
                    break;
            }
        }

        

    }
}
