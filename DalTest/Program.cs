using DalApi;
using DO;
using Dal;
using System.Collections.Specialized;
using System.Reflection.Emit;

namespace DalTest
{
    public class Program
    {
        private static IEngineer? s_dalEngineer = new EngineerImplementation();           //stage 1
        private static ITask? s_dalTask = new TaskImplementation();                      //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation();    //stage 1


        private static void Main(string[] args)
        {
            Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
            int choice;
            MainMenu();
            choice = int.Parse(Console.ReadLine()!);
            switch (choice)
            {

                case 1:
                    string pdsName1 = "Engineer";
                    SubMenu(pdsName1);
                    break;
                case 2:
                    string pdsName2 = "Task";
                    SubMenu(pdsName2);
                    break;
                case 3:
                    string pdsName3 = "Dependency";
                    SubMenu(pdsName3);
                    break;
                default:
                    break;


            }
        }
        public static void MainMenu()
        {
            Console.WriteLine("Select a PDS you want to check:");
            Console.WriteLine("0. Exit main menu");
            Console.WriteLine("1. Engineer");
            Console.WriteLine("2. Task");
            Console.WriteLine("3. Dependency");

        }
        public static void SubMenu(string pds)
        {
            Console.WriteLine("1. Exit main menu");
            Console.WriteLine("2. Add new item");
            Console.WriteLine("3. Read item");
            Console.WriteLine("4. ReadAll items");
            Console.WriteLine("5. Update item");
            Console.WriteLine("6. Delete item");
            if (pds == "Engineer")
            {
                int choice;
                choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {

                    case 2:
                        AddEngineer();
                        break;
                    case 3:
                        ReadEngineer();
                        break;
                    case 4:
                        ReadAllEngineer();
                        break;
                    case 5:
                        UpdateEngineer();
                        break;
                    case 6:
                        DeleteEngineer();
                        break;
                    default:
                        Console.WriteLine("exit sub menu");

                        break;
                }


            }

            else if (pds == "Task")
            {
                int choice;
                choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {

                    case 2:
                        //Console.WriteLine
                        AddTask();
                        break;
                    case 3:
                        ReadTask();
                        break;
                    case 4:
                        ReadAllTask();
                        break;
                    case 5:
                        UpdateTask();
                        break;
                    case 6:
                        DeleteTask();
                        break;
                    default:
                        Console.WriteLine("exit sub menu");

                        break;
                }
            }
            else
            {
                int choice;
                choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {

                    case 2:
                        //Console.WriteLine
                            AddDependency();
                        break;
                    case 3:
                        ReadDependency();
                        break;
                    case 4:
                        ReadAllDependency();
                        break;
                    case 5:
                        UpdateDependency();
                        break;
                    case 6:
                        DeleteDependency();
                        break;
                    default:
                        Console.WriteLine("exit sub menu");

                        break;
                }


            }
            
        }
        public static Engineer? InputEngineerData()
        {
            
            int newId = int.Parse(Console.ReadLine()!);
            string newEmail = Console.ReadLine()!;
            string newName = Console.ReadLine()!;
            int newCost = int.Parse(Console.ReadLine()!);
            Engineer engineer = new Engineer(newId, newEmail, newName, newCost);
            return engineer;
        }
        public static void AddEngineer()
        {
            Console.WriteLine("enter id, email,name and cost of the new engineer");
            Console.WriteLine(s_dalEngineer!.Create(InputEngineerData()!));
        }
        
        public static void ReadEngineer()
        {
            int newId = int.Parse(Console.ReadLine()!);
          Console.WriteLine( s_dalEngineer!.Read(newId));
            
        }
        public static void ReadAllEngineer()
        {
            foreach(Engineer engineer in s_dalEngineer!.ReadAll())
            {
                Console.WriteLine(engineer);
            }

        }
        public static void UpdateEngineer()
        {
            Console.WriteLine("enter id, email,name and cost of the update engineer");
            Engineer UpdateEngineer = InputEngineerData()!;
            Console.WriteLine(s_dalEngineer!.Read(UpdateEngineer.Id)!);     //printing the previous engineer with the same id in the list
            s_dalEngineer.Update(UpdateEngineer);           //updating the engineer with the same id in the list and adding the update enginner to the end  of list

        }
        public static void DeleteEngineer()
        {
            Console.WriteLine("enter id of the engineer you want to delete");
            int deleteId= int.Parse(Console.ReadLine()!);
            s_dalEngineer!.Delete(deleteId);
        }

        //int EngineerId,
        //DO.EngineerExperience Complexity,
        //string?Alias=null,
        //string?Description=null,
        //bool IsMilestone=false,
        //DateTime? CreateAtDate=null,
        //DateTime? StartDate=null,
        //DateTime? ScheduledDate=null,
        //DateTime?DeadlineDate=null,
        //DateTime? CompleteDate=null,
        //TimeSpan? RequiredEffortTime=null,
        //string? Deliverables=null,
        //string? Remarks=null

        public static Task InputTaskData()
        {
                        
            int newIdEngineer = int.Parse(Console.ReadLine()!);
            string newEmail = Console.ReadLine()!;
            string newName = Console.ReadLine()!;
            int newCost = int.Parse(Console.ReadLine()!);
            Task task = new Task(newId, newEmail, newName, newCost);
            return task;
        }
        public static void AddTask()
        {


        }
        public static void ReadTask()
        {


        }
        public static void ReadAllTask()
        {


        }
        public static void UpdateTask()
        {


        }
        public static void DeleteTask()
        {


        }


        public static Dependency? InputDependencyData()
        {

            int newIdEngineer = int.Parse(Console.ReadLine()!);
            string newEmail = Console.ReadLine()!;
            string newName = Console.ReadLine()!;
            int newCost = int.Parse(Console.ReadLine()!);
            Dependency dependency = new Dependency(newId, newEmail, newName, newCost);
            return dependency;
        }
        public static void AddDependency()
        {


        }
        public static void ReadDependency()
        {


        }
        public static void ReadAllDependency()
        {


        }
        public static void UpdateDependency()
        {


        }
        public static void DeleteDependency()
        {


        }
    }
}




