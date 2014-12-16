using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lab3cs
{
    [Serializable]
    public class Book : ISerializable
    {
        public string Name { get; private set; }
        public int Year { get; private set; }
        public int Serial { get; private set; }
        public int Price { get; private set; }
        public int Amount { get; private set; }
        public Book( string name, int year, int serial, int price, int amount )
        {
            Name = name;
            Year = year;
            Serial = serial;
            Price = price;
            Amount = amount;
        }
        void IncreasePrice( int percent )
        {
            if ( percent <= 0 || percent > 100 ) {
                throw new ArgumentException();
            }
            Price += ( int )( Price * ( double )percent / 100.0 );
        }
        int GetValue()
        {
            return Amount * Price;
        }
        public override string ToString()
        {
            return String.Format( "{0}, {1}, {2}, ${3}, {4} cop" + ( Amount == 1 ? "y" : "ies" ), Name, Year, Serial, Price, Amount );
        }
        public void GetObjectData( SerializationInfo info, StreamingContext context )
        {
            info.AddValue( "Name", Name, typeof( string ) );
            info.AddValue( "Year", Year );
            info.AddValue( "Serial", Serial );
            info.AddValue( "Price", Price );
            info.AddValue( "Amount", Amount );
        }
        private Book( SerializationInfo info, StreamingContext context )
        {
            Name = info.GetString( "Name" );
            Year = ( int )info.GetValue( "Year", typeof( int ) );
            Serial = ( int )info.GetValue( "Serial", typeof( int ) );
            Price = ( int )info.GetValue( "Price", typeof( int ) );
            Amount = ( int )info.GetValue( "Amount", typeof( int ) );
        }
    }
}
