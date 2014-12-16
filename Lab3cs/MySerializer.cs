using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Collections;
using System.IO;

namespace Lab3cs
{
    public enum MyMode { Binary, Soap }  
    public class MySerializer<T> where T : ISerializable
    {
        private IFormatter MyFormatter;
        public MySerializer( MyMode m )
        {
            if ( m == MyMode.Binary ) {
                MyFormatter = new BinaryFormatter();
            } else if ( m == MyMode.Soap ) {
                MyFormatter = new SoapFormatter();
            }
        }
        public void Serialize( IList<T> list, string filename )
        {
            FileStream fs = new FileStream( filename, FileMode.Create );
            foreach ( T data in list ) {
                MyFormatter.Serialize( fs, data );
            }            
            fs.Close();
        }
        public MyList<T> Deserialize( string filename )
        {
            FileStream fs = new FileStream( filename, FileMode.Open );
            var list = new MyList<T>();
            while ( fs.Position != fs.Length ) {
                list.Add( ( T )MyFormatter.Deserialize( fs ) );
            }            
            return list;
        }
    }
}
