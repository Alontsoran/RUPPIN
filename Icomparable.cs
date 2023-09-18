using ConsoleApp5;
using ConsoleApp6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5
{
    internal interface Icomparable
    {
        public bool CompareTo(object a,object b)
        {
            if (a is DataFile && b is DataFile)
            {
                DataFile A = (DataFile)a;
                DataFile B = (DataFile)b;
                if (A.GetSize() == B.GetSize())
                {
                    return true;
                }
                return false;
            }
            else
            {
                throw new Exception("Unvalid Type");
            }
        }
    }
}
