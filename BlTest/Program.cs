using BlApi;
using BO;
using DalApi;
using DalTest;
using DO;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
//Hadar Cohen 213953029
//Ayelet Hashachar Abayev 323098939

namespace BlTest
{
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        static readonly IDal s_dal = DalApi.Factory.Get;
        private static void Main(string[] args)
        {
            try
            {

                int choice;
                MainMenu();

                do                       //choos the Item you want to work with:
                {
                    choice = int.Parse(Console.ReadLine()!);
                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("bye");
                            Initialization.Reset();
                            break;
                        case 1:
                            Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
                            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
                            if (ans == "Y")
                                Initialization.Do();
                            break;

                        case 2:
                            SubMenuTask();
                            break;
                        case 3:
                            SubMenuEngineer();
                            break;
                        case 4:
                            s_bl.CreateSchedule();
                            Console.WriteLine($"Schedule Project: \n"+ $"Start: {s_bl.StartProject!.GetValueOrDefault()} \n"+ $"End: {s_bl.EndProject!.GetValueOrDefault()}");
                            break;
                        case 5:

                            Console.WriteLine($"Project Status: {s_bl.CalcStatusProject()} \n");
                            break;

                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                } while (choice != 0);
            }

            catch (Exception mesg)
            {
                PrintException(mesg);
            }

        }
        public static void MainMenu() //choosing item
        {
            Console.WriteLine("Welcome to creating your project:");
            Console.WriteLine("0. Exit main menu");
            Console.WriteLine("1. Initialization");
            Console.WriteLine("2. Task");
            Console.WriteLine("3. Engineer");
            Console.WriteLine("4. Create Project");
            Console.WriteLine("5. View Status Project");

        }
        public static void SubMenuEngineer()
        // choosing the function you want to activate on the chosen item.
        {

            Console.WriteLine("0. Exit main menu");
            Console.WriteLine("1. Add new engineer");
            Console.WriteLine("2. Read engineer");
            Console.WriteLine("3. ReadAll engineers");
            Console.WriteLine("4. Update engineer");
            Console.WriteLine("5. Delete engineer");

            int choice;
            do
            {
                choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("exit sub menu");
                        MainMenu();
                        return;
                    case 1:
                        AddEngineer();
                        break;
                    case 2:
                        ReadEngineer();
                        break;
                    case 3:
                        ReadAllEngineer();
                        break;
                    case 4:
                        UpdateEngineer();
                        break;
                    case 5:
                        DeleteEngineer();
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            } while (choice != 0);
        }
        public static void SubMenuTask() 
           // choosing the function you want to activate on the chosen item.
        {
            Console.WriteLine("0. Exit main menu");
            Console.WriteLine("1. Add new task");
            Console.WriteLine("2. Update schedule date of task");
            Console.WriteLine("3. Read task");
            Console.WriteLine("4. ReadAll tasks");
            Console.WriteLine("5. Update task");
            Console.WriteLine("6. Delete task");
            int choice;
            do
            {
                choice = int.Parse(Console.ReadLine()!);
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("exit sub menu");
                        MainMenu();
                        return;
                    case 1:

                        AddTask();
                        break;
                    case 2:
                        UpdateScheduleDate();
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
                        Console.WriteLine("ERROR");
                        break;
                }

            } while (choice != 0);

        }


        public static BO.Engineer? InputEngineerData()
        //input function -> to receive all Engineer parameters 
        {
            Console.WriteLine($"Enter an id of engineer");
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);
            Console.WriteLine($"Enter an email of engineer");
            string newEmail = Console.ReadLine()!;
            Console.WriteLine($"Enter a name of engineer");
            string newName = Console.ReadLine()!;
            Console.WriteLine($"Enter a level of engineer");
            string level = Console.ReadLine()!;
            Console.WriteLine($"Enter a cost of engineer");
            string newCost = Console.ReadLine()!;
            int.TryParse(newCost, out int newCostNum);
            Enum.TryParse<BO.EngineerExperience>(level, out BO.EngineerExperience selectedLevel);     //Bonus: converter from string to Enum
            int intValue = Convert.ToInt32(selectedLevel);
            while (intValue > 4 || intValue < 0)
            {
                Console.WriteLine("ERROR");
                level = Console.ReadLine()!;
                Enum.TryParse<BO.EngineerExperience>(level, out selectedLevel);     //Bonus: converter from string to Enum
                intValue = Convert.ToInt32(selectedLevel);

            }

            BO.Engineer engineer = new BO.Engineer()
            {
                Id = newIdNum,
                Email = newEmail,
                Name = newName,
                Cost = newCostNum,
                Level = selectedLevel
            };
            return engineer;



        }

        public static BO.Engineer? InputUpdateEngineerData()
        //input function -> to receive all Engineer parameters 
        {
            Console.WriteLine($"Enter an id of engineer");
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);
            Console.WriteLine($"Enter an email of engineer");
            string newEmail = Console.ReadLine()!;
            Console.WriteLine($"Enter a name of engineer");
            string newName = Console.ReadLine()!;
            Console.WriteLine($"Enter a level of engineer");
            string level = Console.ReadLine()!;
            Console.WriteLine($"Enter a cost of engineer");
            string newCost = Console.ReadLine()!;
            
            string num = Console.ReadLine()!;
            int.TryParse(num, out int choice);
            int.TryParse(newCost, out int newCostNum);
            Enum.TryParse<BO.EngineerExperience>(level, out  BO.EngineerExperience selectedLevel);     //Bonus: converter from string to Enum
            int intValue = Convert.ToInt32(selectedLevel);
            while (intValue > 4 || intValue < 0)
            {
                Console.WriteLine("ERROR");
                level = Console.ReadLine()!;
                Enum.TryParse<BO.EngineerExperience>(level, out selectedLevel);     //Bonus: converter from string to Enum
                intValue = Convert.ToInt32(selectedLevel);

            }
            Console.WriteLine($"If you want to assign an engineer to a task, enter 1");
            if (choice==1)
            {
                Console.WriteLine("enter id and alias of the assignTask of engineer");
                string id = Console.ReadLine()!;
                string alias = Console.ReadLine()!;
                int.TryParse(id, out int idTask);
                BO.Engineer engineer = new BO.Engineer()
                {
                    Id = newIdNum,
                    Email = newEmail,
                    Name = newName,
                    Cost = newCostNum,
                    Level = selectedLevel,
                    Task = new BO.TaskInEngineer
                    {
                        Id = idTask,
                        Alias = alias
                    }
                };
                return engineer;
            }
            else
            {
                BO.Engineer engineer = new BO.Engineer()
                {
                    Id = newIdNum,
                    Email = newEmail,
                    Name = newName,
                    Cost = newCostNum,
                    Level = selectedLevel,
                };
                return engineer;
            }
            





        }

        public static void AddEngineer() //add item
        {
            Console.WriteLine("enter id, email,name and cost of the new engineer");
            try
            {
                Console.WriteLine(s_bl!.Engineer.Create(InputEngineerData()!));

            }
            catch (Exception mesg)
            {
                Console.WriteLine(mesg.Message);
            }
        }

        public static void ReadEngineer() //search item 
        {
            Console.WriteLine("Enter item id");
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);
            Console.WriteLine(s_bl!.Engineer.Read(newIdNum));

        }
        public static void ReadAllEngineer()
        {
            s_bl!.Engineer.ReadAll().ToList().ForEach(x => Console.WriteLine(x));
        }
        public static void UpdateEngineer() // update item, and printing the data befor updating 
        {
            Console.WriteLine("enter id, email,name ,cost, level of the update engineer");
            BO.Engineer UpdateEngineer = InputUpdateEngineerData()!;
            Console.WriteLine("If you want to assign an engineer to a task enter 1");

            Console.WriteLine(s_bl!.Engineer.Read(UpdateEngineer.Id)!);     //printing the previous engineer with the same id in the list
            try
            {

                s_bl!.Engineer.Update(UpdateEngineer);
            }
            //updating the engineer with the same id in the list and adding the update enginner to the end  of list
            catch (Exception mesg)
            {
                Console.WriteLine(mesg);
            }
        }



        public static void DeleteEngineer()     // delete item
        {
            Console.WriteLine("enter id of the engineer you want to delete");
            string num = Console.ReadLine()!;
            int.TryParse(num, out int deleteId);
            try
            {
                s_bl!.Engineer.Delete(deleteId);
            }
            catch (Exception mesg)
            {
                Console.WriteLine(mesg);
            }

        }


        public static BO.Task InputTaskData()
        // input function -> to receive all Task parameters 
        {
            Console.WriteLine($"Enter an alias to task");
            string alias = Console.ReadLine()!;
            Console.WriteLine($"Enter a description to task");
            string description = Console.ReadLine()!;
            Console.WriteLine($"Enter a complexity to task");
            string levelCom = Console.ReadLine()!;
            Enum.TryParse<BO.EngineerExperience>(levelCom, out BO.EngineerExperience selectedLevelCom);
            Console.WriteLine($"Enter a requierdEffortTime to task");
            string? timeSpan = Console.ReadLine();
            Console.WriteLine($"if you want to add tasks that the task depends on enter 1");
            string num = Console.ReadLine()!;
            int.TryParse(num, out int choice);
            List<TaskInList>? dependencies = null;
            if (choice == 1)
            {

                while (choice == 1)
                {
                    //לשבת על זה!
                    BO.TaskInList taskInList = InputTaskInListData();
                    dependencies?.Add(taskInList);
                    Console.WriteLine($"enter 1 if you want to add more, else enter 0");
                    num = Console.ReadLine()!;
                    int.TryParse(num, out choice);
                }
            }

            TimeSpan.TryParse(timeSpan, out TimeSpan requierdEffortTime);
            int intValue = Convert.ToInt32(selectedLevelCom);
            while (intValue > 4 || intValue < 0)
            {
                Console.WriteLine("ERROR Complexity");
                Console.WriteLine("enter again a complexity");
                levelCom = Console.ReadLine()!;
                Enum.TryParse<BO.EngineerExperience>(levelCom, out selectedLevelCom);     //Bonus: converter from string to Enum
                intValue = Convert.ToInt32(selectedLevelCom);

            }

            BO.Task task = new BO.Task()
            {
                Id = 0,
                Alias = alias,
                Description = description,
                CreateAtDate = DateTime.Now,
                Complexity = selectedLevelCom,
                RequiredEffortTime = requierdEffortTime,
                Dependencies = dependencies

            };

            return task;
        }

        public static BO.TaskInList InputTaskInListData()
        // input function -> to receive all Task In List parameters 

        {
            Console.WriteLine($"Enter id of the task");
            string num = Console.ReadLine()!;
            int.TryParse(num, out int id);
            BO.Task task =s_bl.Task.Read(id);
            //Console.WriteLine($"Enter alias of the task");
            //string alias = Console.ReadLine()!;
            //Console.WriteLine($"Enter description of the task");
            //string description = Console.ReadLine()!;

            return new BO.TaskInList()
            {
                Id = id,
                Alias = task.Alias,
                Description = task.Description,
                Status=task.Status


            };

        }
        public static void AddTask() //add item
        {
            Console.WriteLine("Enter alias, description ,level complexity and requierfEffortTime of the new Task");
            try
            {
                Console.WriteLine(s_bl!.Task.Create(InputTaskData()!));
            }
            catch (Exception mesg)
            {
                Console.WriteLine(mesg);
            }



        }

        public static void UpdateScheduleDate()
            //a specific attention to the scedual date -> to update it 
        {
            DateTime scheduled;
            try
            {
                Console.WriteLine("Enter an id of the task");
                string numID = Console.ReadLine()!;
                int.TryParse(numID, out int IdNum);
                Console.WriteLine("Please enter a scheduel date (YYYY-MM-DD):");
                string userInput = Console.ReadLine()!;
                bool check = (DateTime.TryParse(userInput, out DateTime date));

                while (!check)
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                    Console.WriteLine("Please enter a scheduel date (YYYY-MM-DD):");
                    userInput = Console.ReadLine()!;
                    check = (DateTime.TryParse(userInput, out DateTime inputDate));


                    if (check)
                    {
                        scheduled = inputDate;
                        s_bl.Task.Update(IdNum, scheduled);

                    }
                }
                s_bl.Task.Update(IdNum, date);
            }
            catch(Exception mesg)
            {
                PrintException(mesg);
                    
            }
            

        }
        public static void ReadTask()  //search item 
        {
            Console.WriteLine("Enter item id");
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newIdNum);

            Console.WriteLine(s_bl!.Task.Read(newIdNum));


        }
        public static void ReadAllTask()
        {
            try
            {
                s_bl!.Task.ReadAll().ToList().ForEach(x => Console.WriteLine(x));
            }
            catch (Exception mesg)
            {

                PrintException(mesg);
            }



        }
        public static void UpdateTask()  //update item, and printing the data befor updating
        {
            Console.WriteLine("if there is a ScheduleDate of the project press 1, else press 0");
            string num = Console.ReadLine()!;
            int.TryParse(num, out int choice);

            if (choice == 1)
            {

                Console.WriteLine(" enter id of the task");
                num = Console.ReadLine()!;
                int.TryParse(num, out int id);
                Console.WriteLine(" enter id of the engineer");
                num = Console.ReadLine()!;
                int.TryParse(num, out int engineerID);
                Console.WriteLine(" enter alias of the task");
                string alias = Console.ReadLine()!;
                Console.WriteLine(" enter description of the task");
                string description = Console.ReadLine()!;
                Console.WriteLine(" enter remarks of the task");
                string remarks = Console.ReadLine()!;
                Console.WriteLine(" enter deliverables of the task");
                string deliverables = Console.ReadLine()!;

                //printing the original task
                try
                {
                    Console.WriteLine(s_bl.Task.Read(id));

                    s_bl.Task.Update(new BO.Task
                    {
                        Id = id,
                        Alias = alias,
                        Description = description,
                        Remarks = remarks,
                        Deliverables = deliverables,
                        Engineer = new BO.EngineerInTask
                        {
                            Id = engineerID,
                            Name = s_dal.Engineer.Read(engineerID)?.Name
                        }

                    });
                }
                catch (Exception mesg)
                {

                    PrintException(mesg);
                }


            }

            //there is no schedeule date of project
            if (choice == 0)
            {
                Console.WriteLine(" enter id of the task");
                num = Console.ReadLine()!;
                int.TryParse(num, out int id);
                Console.WriteLine("enter 1 if you want to ");
                num = Console.ReadLine()!;
                int.TryParse(num, out choice);
                int? engineerID = null;
                if (choice==1)
                {
                    Console.WriteLine("enter id of the engineer");
                    num = Console.ReadLine()!;
                    int.TryParse(num, out int idEngineer);
                    engineerID = idEngineer;

                }
                
                Console.WriteLine(" enter alias of the task");
                string alias = Console.ReadLine()!;
                Console.WriteLine("enter description of the task");
                string description = Console.ReadLine()!;
                Console.WriteLine("enter remarks of the task");
                string remarks = Console.ReadLine()!;
                Console.WriteLine("enter deliverables of the task");
                string deliverables = Console.ReadLine()!;
                Console.WriteLine($"Enter a complexity to task");
                string levelCom = Console.ReadLine()!;
                Enum.TryParse<BO.EngineerExperience>(levelCom, out BO.EngineerExperience selectedLevelCom);
                int intValue = Convert.ToInt32(selectedLevelCom);
                while (intValue > 4 || intValue < 0)
                {
                    Console.WriteLine("ERROR");
                    levelCom = Console.ReadLine()!;
                    Enum.TryParse<BO.EngineerExperience>(levelCom, out selectedLevelCom);     //Bonus: converter from string to Enum
                    intValue = Convert.ToInt32(selectedLevelCom);

                }
                Console.WriteLine($"enter a requierdEffortTime to task");
                string? timeSpan = Console.ReadLine();
                Console.WriteLine($"if you want to add or delete tasks that the task depends on enter 1");
                num = Console.ReadLine()!;
                int.TryParse(num, out choice);
                List<TaskInList>? dependencies = s_bl.Task.Read(id).Dependencies;
                if (choice == 1)
                {

                    while (choice == 1)
                    {
                        Console.WriteLine($"enter 2 if you want to add more else enter 3 to delete");
                        num = Console.ReadLine()!;
                        int.TryParse(num, out choice);
                        if (choice == 2)
                        {
                            BO.TaskInList taskInList = InputTaskInListData();
                            dependencies?.Add(taskInList);
                        }
                           
                        if (choice == 3)
                        {
                            BO.TaskInList taskInList = InputTaskInListData();
                            dependencies?.Remove(taskInList);
                        }
                            
                        Console.WriteLine($"enter 1 if you want to add/delete more, else enter 0");
                        num = Console.ReadLine()!;
                        int.TryParse(num, out choice);
                    }

                }

                TimeSpan.TryParse(timeSpan, out TimeSpan requierdEffortTime);
                

                DateTime? startDate = null;
                Console.WriteLine($"if you want to enter Startproject enter 1,else 0");
                num = Console.ReadLine()!;
                int.TryParse(num, out choice);
                if (choice == 1)
                {
                    startDate = InputDate(startDate);

                }
                DateTime? completeDate = null;
                Console.WriteLine($"if you want to enter CompleteDate enter 1,else 0");
                num = Console.ReadLine()!;
                int.TryParse(num, out choice);
                if (choice == 1)
                {
                    completeDate = InputDate(completeDate);

                }


                //printing the original task

                try
                {
                    if(engineerID!=null)
                    {
                        Console.WriteLine(s_bl.Task.Read(id));
                        s_bl.Task.Update(new BO.Task
                        {
                            Id = id,
                            Alias = alias,
                            Description = description,
                            CompleteDate = completeDate,
                            StartDate = startDate,
                            Complexity = selectedLevelCom,
                            Dependencies = dependencies,
                            RequiredEffortTime = requierdEffortTime,
                            Remarks = remarks,
                            Deliverables = deliverables,
                            Engineer = new BO.EngineerInTask
                            {
                                Id = (int)engineerID,
                                Name = s_dal.Engineer.Read((int)engineerID)?.Name
                            }

                        });
                    }
                    else
                    {
                        Console.WriteLine(s_bl.Task.Read(id));
                        s_bl.Task.Update(new BO.Task
                        {
                            Id = id,
                            Alias = alias,
                            Description = description,
                            CompleteDate = completeDate,
                            StartDate = startDate,
                            Complexity = selectedLevelCom,
                            Dependencies = dependencies,
                            RequiredEffortTime = requierdEffortTime,
                            Remarks = remarks,
                            Deliverables = deliverables,
                            

                        });
                    }
                    
                }
                catch (Exception mesg)
                {

                    PrintException(mesg);
                }




            }

        }

        private static DateTime? InputDate(DateTime? date)
        // input function -> to receive all the DateTime parameters 
        {
            Console.WriteLine("Please enter a date (YYYY-MM-DD):");
            string userInput = Console.ReadLine()!;
            bool check = (DateTime.TryParse(userInput, out DateTime inputDate));
            while (!check)
            {
                Console.WriteLine("Invalid date format. Please try again.");
                Console.WriteLine("Please enter a date (YYYY-MM-DD):");
                userInput = Console.ReadLine()!;
                check = (DateTime.TryParse(userInput, out inputDate));
                if (check)
                {
                    date = inputDate;

                }
            }

            return date;
        }

        public static void DeleteTask() //delete item 
        {

            Console.WriteLine("enter id of the Task you want to delete");
            string newId = Console.ReadLine()!;
            int.TryParse(newId, out int newdeleteId);

            try
            {
                s_bl!.Task.Delete(newdeleteId);
            }
            catch (Exception mesg)
            {

                PrintException(mesg);
            }
        }

        public static void PrintException(Exception messege)
        {
            Console.Write(messege.GetType() +":");
            Console.WriteLine(messege.Message);
            if (messege.InnerException != null)
                Console.WriteLine("Dal Exeption:" + messege.InnerException.GetType() + "\n" + messege.InnerException.Message);
        }
    }

}
