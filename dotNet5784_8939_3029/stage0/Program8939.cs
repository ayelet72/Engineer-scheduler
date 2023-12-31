//Hadar Cohen 213953029
//Ayelet Hashachar Abayev 323098939

using System.Text;
using System.Threading.Tasks;


namespace stage0
{
    partial class Stage0
    {

        private static void Main(string[] args)
        {
            welcome8939();
            welcome3029();
            Console.ReadKey();
        }
        private static void welcome8939()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("{0} welcome to my first console application ", name);
        }
        static partial void welcome3029();

    }
}