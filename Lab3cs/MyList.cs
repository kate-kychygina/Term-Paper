using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Lab3cs
{
    public class MyList<T> : IList<T>
    {
        #region Node class definition

        // Using different type "S" instead of "T" for generalization's sake and to avoid warning 
        private class Node<S>
        {
            public Node<S> Next { get; set; }
            public Node<S> Prev { get; set; }
            public S Data { get; set; }            
            public Node( S data )
            {
                Data = data;
            }
        }

        #endregion

        #region Private members and properties

        private Node<T> First;
        private Node<T> Last;                        
        
        #endregion

        #region Properties

        public int Count { get; private set; }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        
        
        #endregion

        #region Constructors

        public MyList() {}
        public MyList( int number )
        {
            if ( number <= 0 ) {
                throw new ArgumentOutOfRangeException();
            }
            for ( int i = 0; i != number; ++i ) {
                Add( default( T ) );
            }
        }              
        public MyList( IEnumerable<T> source )
        {
            if ( source == null ) {
                throw new ArgumentNullException();
            }
            foreach ( T Data in source ) {
                Add( Data );
            }
        }

        #endregion

        #region Private methods ( for internal usage )
                      
        private bool IsEmpty()
        {
            return ( Count == 0 ) ? true : false;
        }        
        private void InsertAfter( Node<T> node, T data )
        {
            var newNode = new Node<T>( data );
            newNode.Prev = node;
            newNode.Next = node.Next;
            if ( node.Next == null ) {
                Last = newNode;
            } else {
                node.Next.Prev = newNode;
            }
            node.Next = newNode;
        }        
        private void InsertBefore( Node<T> node, T data )
        {
            var newNode = new Node<T>( data );
            newNode.Prev = node.Prev;
            newNode.Next = node;
            if ( node.Prev == null ) {
                First = newNode;
            } else {
                node.Prev.Next = newNode;
            }
            node.Prev = newNode;
        }     
        private void CheckArgument( int index )
        {
            if ( index >= Count || index < 0 ) {
                throw new ArgumentOutOfRangeException();
            }
        }               
        private Node<T> ItemAt( int index )
        {
            // Uses "Count" property to determine whether the desired node
            // is in the first or second "half" of the list, and thus to
            // traverse "forward" from begin, or "backward" from end    
            if ( index <= Count / 2 ) {
                var item = First;
                for ( int i = 0; i != index; ++i ) {
                    item = item.Next;
                }
                return item;
            } else {
                var item = Last;
                for ( int i = Count - 1; i != index; --i ) {
                    item = item.Prev;
                }
                return item;
            }
        }

        #endregion

        #region Public methods

        public void Add( T data )
        {
            if ( data == null ) {
                throw new ArgumentNullException();
            }
            if ( IsEmpty() ) {
                First = Last = new Node<T>( data );
                ++Count;
            } else {
                InsertAfter( Last, data );
                ++Count;
            }
        }               
        public void AddRange( IEnumerable<T> source )
        {
            if ( source == null ) {
                throw new ArgumentNullException();
            }
            foreach ( T Data in source ) {
                Add( Data );
            }
        }
        public void AddToBeginning( T data )
        {
            if ( data == null ) {
                throw new ArgumentNullException();
            }
            if ( IsEmpty() ) {
                First = Last = new Node<T>( data );
                ++Count;
            } else {
                InsertBefore( First, data );
                ++Count;
            }
        }
        public void Clear()
        {
            First = Last = null;
            Count = 0;
        }
        public bool Contains( T data )
        {
            if ( data == null ) {
                throw new ArgumentNullException();
            }
            foreach ( T item in this ) {
                if ( item.Equals( data ) ) {
                    return true;
                }
            }
            return false;
        }
        public void CopyTo( T[] target, int index )
        {
            if ( target == null ) {
                throw new ArgumentNullException();
            }
            if ( index < 0 ) {
                throw new ArgumentOutOfRangeException();
            }
            if ( Count > target.Length - index ) {
                throw new ArgumentException();
            }
            int i = index;
            foreach ( T data in this ) {
                target[i] = data;
                ++i;
            }
        }                     
        public T Find( Predicate<T> match )
        {
            if ( match == null ) {
                throw new ArgumentNullException();
            }
            foreach ( T data in this ) {
                if ( match( data ) ) {
                    return data;
                }
            }
            return default( T );
        }               
        public MyList<T> FindAll( Predicate<T> match )
        {
            if ( match == null ) {
                throw new ArgumentNullException();
            }
            MyList<T> temp = new MyList<T>();
            foreach ( T data in this ) {
                if ( match( data ) ) {
                    temp.Add( data );
                }
            }
            return temp;
        }             
        public int IndexOf( T data )
        {
            if ( data == null ) {
                throw new ArgumentNullException();
            }
            for ( int i = 0; i != Count; ++i ) {
                if ( this[i].Equals( data ) ) {
                    return i;
                }
            }
            return -1;
        }                     
        public void Insert( int index, T data )
        {
            if ( data == null ) {
                throw new ArgumentNullException();
            }
            if ( index == Count ) {
                Add( data );
            } else if ( IsEmpty() ) {
                CheckArgument( index );
                Add( data );
            } else {
                CheckArgument( index );
                var node = ItemAt( index );
                if ( node == First ) {
                    AddToBeginning( data );
                }
                InsertAfter( node.Prev, data );
                ++Count;
            }
        }                       
        public bool Remove( T data )
        {
            if ( data == null ) {
                throw new ArgumentNullException();
            }
            for ( int i = 0; i != Count; ++i ) {
                if ( this[i].Equals( data ) ) {
                    RemoveAt( i );
                    return true;
                }
            }
            return false;
        }              
        public void RemoveAt( int index )
        {
            CheckArgument( index );
            var node = ItemAt( index );
            if ( node.Prev == null ) {
                First = node.Next;
            } else {
                node.Prev.Next = node.Next;
            }
            if ( node.Next == null ) {
                Last = node.Prev;
            } else {
                node.Next.Prev = node.Prev;
            }
            node = null;
            --Count;
        }
        public void Sort()
        {
            Sort( Comparer<T>.Default );
        }  
        public void Sort( IComparer<T> comp )
        {
            Sort( ( x, y ) => comp.Compare( x, y ) );
        }     
        public void Sort( Comparison<T> comp )
        {
            if ( comp == null ) {
                throw new ArgumentNullException();
            }
            T temp;
            for ( int i = 0; i != Count; ++i ) {
                for ( int j = 0; j != Count - i - 1; ++j ) {
                    if ( comp( this[j], this[j + 1] ) > 0 ) {
                        temp = this[j];
                        this[j] = this[j + 1];
                        this[j + 1] = temp;
                    }
                }
            }            
        }              
        public T this[int index]
        {
            get
            {
                CheckArgument( index );
                return ItemAt( index ).Data;
            }
            set
            {
                CheckArgument( index );
                ItemAt( index ).Data = value;
            }
        }

        #endregion
                
        #region Methods, necessary to implement foreach
        public IEnumerator<T> GetEnumerator()
        {
            for ( var node = First; node != null; node = node.Next ) {
                yield return node.Data;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
