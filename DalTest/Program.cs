//Hadar Cohen 213953029
//Ayelet Hashachar Abayev 323098939



//להמשיך לשים לב למחלקת ולבנאי של המשימות


using DalApi;
using DO;
using Dal;
using System.Collections.Specialized;
using System.Reflection.Emit;



namespace DalTest
{
    public class Program
    {
        //private static IEngineer? s_dalEngineer = new EngineerImplementation();           //stage 1
        //private static ITask? s_dalTask = new TaskImplementation();                      //stage 1
        //private static IDependency? s_dalDependency = new DependencyImplementation();    //stage 1

        static readonly IDal s_dal = new DalList(); //stage 2
        private static void Main(string[] args)
        {
            try
            {
                //Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);           stage 1
                Initialization.Do(s_dal);                                               //stage 2
                int choice;
                MainMenu();

                do  //choos the Item you want to work with:
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
        public static void MainMenu() //choosing item
        {
            Console.WriteLine("Select a PDS you want to check:");
            Console.WriteLine("0. Exit main menu");
            Console.WriteLine("1. Engineer");
            Console.WriteLine("2. Task");
            Console.WriteLine("3. Dependency");

        }
        public static void SubMenu(string pds) // choosing the function you want to activate on the chosen item.
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
                } while (choice != 1);

            }


            else if (pds == "Task")
            {
                int choice;
                do
                {
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
                        case 1:
                            Console.WriteLine("exit sub menu");
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }

                }while(choice != 1);
            }
            else
            {
                int choice;
                do
                {
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
                        case 1:
                            Console.WriteLine("exit sub menu");
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    } 
                } while (choice != 1);



            }

        }
        public static Engineer? InputEngineerData() 
            //input function -> to receive all Engineer parameters 
        {
            
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);
            string newEmail = Console.ReadLine()!;
            string newName = Console.ReadLine()!;
            string newCost = Console.ReadLine()!;
            int.TryParse(newCost, out int newCostNum);
            string level= Console.ReadLine()!;
            Enum.TryParse<EngineerExperience>(level, out EngineerExperience selectedLevel);     //Bonus: converter from string to Enum
            int intValue = Convert.ToInt32(selectedLevel);
            while (intValue > 4 || intValue < 0)
            {
                Console.WriteLine("ERROR");
                level = Console.ReadLine()!;
                Enum.TryParse<EngineerExperience>(level, out  selectedLevel);     //Bonus: converter from string to Enum
                intValue = Convert.ToInt32(selectedLevel);

            }

            Engineer engineer = new Engineer(newIdNum, newEmail, newName, newCostNum, selectedLevel);
            return engineer;

            //throw new LevelDoesntExist($"Level Engineer doesnt exsit");

            
        }

        

        public static void AddEngineer() //add item
        {
            Console.WriteLine("enter id, email,name and cost of the new engineer");
            try
            { 
                Console.WriteLine(s_dal!.Engineer.Create(InputEngineerData()!));
            
            }
            catch (Exception mesg)
            {
                Console.WriteLine(mesg.Message);
            }

        }

        public static void ReadEngineer() //search item 
        {
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);
            Console.WriteLine(s_dal!.Engineer.Read(newIdNum));

        }
        public static void ReadAllEngineer()
        {
            foreach (Engineer engineer in s_dal!.Engineer.ReadAll())
            {
                Console.WriteLine(engineer);
            }

        }
        public static void UpdateEngineer() // update item, and printing the data befor updating 
        {
            Console.WriteLine("enter id, email,name and cost of the update engineer");
            Engineer UpdateEngineer = InputEngineerData()!;
            Console.WriteLine(s_dal!.Engineer.Read(UpdateEngineer.ID)!);     //printing the previous engineer with the same id in the list
            try
            {
                s_dal!.Engineer.Update(UpdateEngineer);           //updating the engineer with the same id in the list and adding the update enginner to the end  of list
            }
            catch(Exception mesg)
            {
                Console.WriteLine(mesg);
            }
        }
        public static void DeleteEngineer()     // delete item
        {
            Console.WriteLine("enter id of the engineer you want to delete");
            int deleteId = int.Parse(Console.ReadLine()!);
            try
            {
                s_dal!.Engineer.Delete(deleteId);
            }
            catch(Exception mesg)
            {
                Console.WriteLine(mesg);
            }
            
        }

    
        public static DO.Task InputTaskData()
           // input function -> to receive all Task parameters 
        { 
            int newIdEngineer = int.Parse(Console.ReadLine()!);
            string alias = Console.ReadLine()!;
            string description = Console.ReadLine()!;
            string levelCom = Console.ReadLine()!;
            Enum.TryParse<DO.EngineerExperience>(levelCom, out DO.EngineerExperience selectedLevelCom);
            int intValue = Convert.ToInt32(selectedLevelCom);
            while (intValue > 4 || intValue < 0)
            {
                Console.WriteLine("ERROR");
                levelCom = Console.ReadLine()!;
                Enum.TryParse<EngineerExperience>(levelCom, out selectedLevelCom);     //Bonus: converter from string to Enum
                intValue = Convert.ToInt32(selectedLevelCom);

            }
           


            DO.Task task = new DO.Task(0,newIdEngineer, selectedLevelCom, alias, description,false, DateTime.Now);
            return task;
        }
        public static void AddTask() //add item
        {
            Console.WriteLine("enter idEngineer, alias, description and level complexity of the new Task");
            try
            {
                Console.WriteLine(s_dal!.Task.Create(InputTaskData()!));
            }
            catch(Exception mesg)
            {
                Console.WriteLine(mesg);
            }
            


        }
        public static void ReadTask()  //search item 
        {
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);

            Console.WriteLine(s_dal!.Task.Read(newIdNum));


        }
        public static void ReadAllTask()
        {
            foreach (DO.Task task in s_dal!.Task.ReadAll())
            {
                Console.WriteLine(task);
            }

        }
        public static void UpdateTask()  //update item, and printing the data befor updating
        {
            Console.WriteLine("enter id");
            int idInput= int.Parse(Console.ReadLine()!); 
            if(s_dal!.Task.Read(idInput)==null)
            {


            }

            Console.WriteLine("enter id, idEngineer, alias,description of the update task");

            DO.Task UpdateTask = InputTaskData()!;
            Console.WriteLine(s_dal!.Task.Read(UpdateTask.Id)!);     //printing the previous Task with the same id in the list
            try
            {
                s_dal!.Task.Update(UpdateTask);           //updating the Task with the same id in the list and adding the update Task to the end  of list

            }
            catch (Exception mesg)
            {
                Console.WriteLine(mesg);
            }
            
        }
        public static void DeleteTask() //delete item 
        {

            Console.WriteLine("enter id of the Task you want to delete");
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newdeleteId);

            try
            {
                s_dal!.Task.Delete(newdeleteId);
            }
            catch (Exception mesg)
            {
                Console.WriteLine(mesg);
            }
        }

      
        public static Dependency? InputDependencyData()
             //     input function -> to receive all Engineer parameters 
        {
            string dependentTask = Console.ReadLine()!;
            int.TryParse(dependentTask, out int convertDependentTask);
            string dependsOnTask = Console.ReadLine()!;
            int.TryParse(dependsOnTask, out int convertDependsOnTask);
            
            Dependency dependency = new Dependency(0, convertDependentTask, convertDependsOnTask);
            return dependency;
        }
        public static void AddDependency() //add item
        {

            Console.WriteLine("enter id of dependentTask and id of dependsOnTask of the new dependency");
            try
            {
                Console.WriteLine(s_dal!.Dependency.Create(InputDependencyData()!));
            }

            catch (Exception mesg)
            {
                Console.WriteLine(mesg);
            }
        }
        public static void ReadDependency() //search item
        {
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);

            Console.WriteLine(s_dal!.Dependency.Read(newIdNum));

        }
        public static void ReadAllDependency() 
        {
            foreach (Dependency dependency in s_dal!.Dependency.ReadAll())      
            {
                Console.WriteLine(dependency);
            }

        }
        public static void UpdateDependency() //update item, and printing the data befor updating
        {

            Console.WriteLine("enter id dependet task and id depends on task of the update Dependency");
            Dependency UpdateDependency = InputDependencyData()!;
            Console.WriteLine(s_dal!.Task.Read(UpdateDependency.ID)!);     //printing the previous Dependency with the same id in the list
            try
            {
                s_dal!.Dependency.Update(UpdateDependency);           //updating the Dependency with the same id in the list and adding the update Dependency to the end  of list
            }

            catch (Exception mesg)
            {
                Console.WriteLine(mesg);
            }
        }
        public static void DeleteDependency()//delete item
        {

            Console.WriteLine("enter id of the Dependency you want to delete");
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newdeleteId);

           

            try
            {
                s_dal!.Dependency.Delete(newdeleteId);
            }
            catch(Exception mesg)
            {
                Console.WriteLine(mesg);
            }
            
        }
    }
}






