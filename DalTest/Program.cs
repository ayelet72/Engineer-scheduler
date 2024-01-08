using DalApi;
using DO;
using Dal;
using System.Collections.Specialized;



namespace DalTest
{
   

    public class Program
    {
        private static IEngineer? s_dalEngineer = new EngineerImplementation();           //stage 1
        private static ITask? s_dalTask = new TaskImplementation();                      //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation();    //stage 1


        private static void Main(string[] args)
        {
            try
            {
                Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
                int choice;
                MainMenu();

                do
                {
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
                        case 0:
                            Console.WriteLine("bye");
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                } while (choice != 0);
            }

            catch(Exception mesg)
            {
                Console.WriteLine(mesg);
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
                do
                {
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
                        case 1:
                            Console.WriteLine("exit sub menu");
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                } while (choice != 1) ;
                
            }



            else if (pds == "Task")
            {
                int choice;
                choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {

                    case 2:

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
            
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);
            string newEmail = Console.ReadLine()!;
            string newName = Console.ReadLine()!;
            string newCost = Console.ReadLine()!;
            int.TryParse(newCost, out int newCostNum);
            string level= Console.ReadLine()!;
            Enum.TryParse<EngineerExperience>(level, out EngineerExperience selectedLevel);     //Bonus: converter from string to Enum

            Engineer engineer = new Engineer(newIdNum, newEmail, newName, newCostNum, selectedLevel);
            return engineer;
        }
        public static void AddEngineer()
        {
            Console.WriteLine("enter id, email,name and cost of the new engineer");
            try
            { 
                Console.WriteLine(s_dalEngineer!.Create(InputEngineerData()!));
            
            }
            catch (Exception mesg)
            {
                Console.WriteLine(mesg.Message);
            }

        }

        public static void ReadEngineer()
        {
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);
        
            Console.WriteLine(s_dalEngineer!.Read(newIdNum));

        }
        public static void ReadAllEngineer()
        {
            foreach (Engineer engineer in s_dalEngineer!.ReadAll())
            {
                Console.WriteLine(engineer);
            }

        }
        public static void UpdateEngineer()
        {
            Console.WriteLine("enter id, email,name and cost of the update engineer");
            Engineer UpdateEngineer = InputEngineerData()!;
            Console.WriteLine(s_dalEngineer!.Read(UpdateEngineer.ID)!);     //printing the previous engineer with the same id in the list
            try
            {
                s_dalEngineer.Update(UpdateEngineer);           //updating the engineer with the same id in the list and adding the update enginner to the end  of list
            }
            catch(Exception mesg)
            {
                Console.WriteLine(mesg);
            }
        }
        public static void DeleteEngineer()
        {
            Console.WriteLine("enter id of the engineer you want to delete");
            int deleteId = int.Parse(Console.ReadLine()!);
            try
            {
                s_dalEngineer!.Delete(deleteId);
            }
            catch(Exception mesg)
            {
                Console.WriteLine(mesg);
            }
            
        }

    
        public static DO.Task InputTaskData()
        { 
            int newIdEngineer = int.Parse(Console.ReadLine()!);
            string alias = Console.ReadLine()!;
            string description = Console.ReadLine()!;
            string levelCom = Console.ReadLine()!;
            Enum.TryParse<DO.EngineerExperience>(levelCom, out DO.EngineerExperience selectedLevelCom);


            DO.Task task = new DO.Task(0,newIdEngineer, selectedLevelCom, alias, description,false, DateTime.Now);
            return task;
        }
        public static void AddTask()
        {
            Console.WriteLine("enter idEngineer, alias, desription of the new Task");
            try
            {
                Console.WriteLine(s_dalTask!.Create(InputTaskData()!));
            }
            catch(Exception mesg)
            {
                Console.WriteLine(mesg);
            }
            


        }
        public static void ReadTask()
        {
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);

            Console.WriteLine(s_dalTask!.Read(newIdNum));


        }
        public static void ReadAllTask()
        {
            foreach (DO.Task task in s_dalTask!.ReadAll())
            {
                Console.WriteLine(task);
            }

        }
        public static void UpdateTask()
        {
            Console.WriteLine("enter idEngineer, alias,description of the update task");
            DO.Task UpdateTask = InputTaskData()!;
            Console.WriteLine(s_dalTask!.Read(UpdateTask.Id)!);     //printing the previous Task with the same id in the list
            try
            {
                s_dalTask.Update(UpdateTask);           //updating the Task with the same id in the list and adding the update Task to the end  of list

            }
            catch (Exception mesg)
            {
                Console.WriteLine(mesg);
            }
            
        }
        public static void DeleteTask()
        {

            Console.WriteLine("enter id of the Task you want to delete");
            string deleteId = Console.ReadLine()!;
            int.TryParse(deleteId, out int newdeleteId);
     
            try
            {
                s_dalTask!.Delete(newdeleteId);
            }
            catch(Exception mesg)
            {
                Console.WriteLine(mesg);
            }
            

        }

        //        int ID,
        //int DependentTask,
        //int DependsOnTask
        public static Dependency? InputDependencyData()
        {
            string dependentTask = Console.ReadLine()!;
            int.TryParse(dependentTask, out int convertDependentTask);
            string dependsOnTask = Console.ReadLine()!;
            int.TryParse(dependsOnTask, out int convertDependsOnTask);
            
            Dependency dependency = new Dependency(0, convertDependentTask, convertDependsOnTask);
            return dependency;
        }
        public static void AddDependency()
        {

            Console.WriteLine("enter id of dependentTask and id of dependsOnTask of the new dependency");
            try
            {
                Console.WriteLine(s_dalDependency!.Create(InputDependencyData()!));
            }

            catch (Exception mesg)
            {
                Console.WriteLine(mesg);
            }
        }
    public static void ReadDependency()
        {
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);

            Console.WriteLine(s_dalDependency!.Read(newIdNum));

        }
        public static void ReadAllDependency()
        {
            foreach (Dependency dependency in s_dalDependency!.ReadAll())       //איך מדפיס ולתקן בהתאם
            {
                Console.WriteLine(dependency);
            }

        }
        public static void UpdateDependency()
        {

            Console.WriteLine("enter id dependet task and id depends on task of the update Dependency");
            Dependency UpdateDependency = InputDependencyData()!;
            Console.WriteLine(s_dalTask!.Read(UpdateDependency.ID)!);     //printing the previous Dependency with the same id in the list
            try
            {
                s_dalDependency!.Update(UpdateDependency);           //updating the Dependency with the same id in the list and adding the update Dependency to the end  of list
            }

            catch (Exception mesg)
            {
                Console.WriteLine(mesg);
            }
        }
        public static void DeleteDependency()
        {

            Console.WriteLine("enter id of the Dependency you want to delete");

            int deleteId = int.Parse(Console.ReadLine()!);

            try
            {
                s_dalDependency!.Delete(deleteId);
            }
            catch(Exception mesg)
            {
                Console.WriteLine(mesg);
            }
            
        }
    }
}






