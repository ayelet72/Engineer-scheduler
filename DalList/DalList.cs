
namespace Dal;
using DalApi;
using DO;

    sealed internal class DalList : IDal
    {
        public static IDal Instance { get; } = new DalList();
        private DalList() { }
        public IEngineer Engineer => new EngineerImplementation();    //throw new NotImplementedException(); 

        public ITask Task => new TaskImplementation();//throw new NotImplementedException();

        public IDependency Dependency => new DependencyImplementation();//throw new NotImplementedException();
    }

