
namespace Dal;
using DalApi;
using DO;

    sealed public class DalList : IDal
    {
        public IEngineer Engineer => new EngineerImplementation();    //throw new NotImplementedException(); 

        public ITask Task => new TaskImplementation();//throw new NotImplementedException();

        public IDependency Dependency => new DependencyImplementation();//throw new NotImplementedException();
    }

