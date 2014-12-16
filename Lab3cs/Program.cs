using System;
using System.Linq;
namespace Lab3cs
{
    public static class Program
    {
        public static void Main()
        {            
            var sourceList = new MyList<Book>()
            {
                new Book( "Azbuka", 2000, 1234, 200, 20 ),
                new Book( "Bukvar", 1988, 6546, 100, 40 ),
                new Book( "Script", 1843, 1456, 150, 20 ),
                new Book( "Manual", 1991, 2071, 900, 1 )
            };
            var m = new MySerializer<Book>( MyMode.Binary );
            
            m.Serialize( sourceList, "test" );
            
            MyList<Book> targetList = m.Deserialize( "test" );

            foreach ( Book b in targetList ) {
                Console.WriteLine( b );
            }
            
            Console.ReadLine();
        }
    }
}
