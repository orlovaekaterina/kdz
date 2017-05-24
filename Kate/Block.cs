using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kate
{
    public class Person
    {
        int length;

        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        public Person(int length)
        {
            Length = length;
        }
    }
}
