using HW5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ConsoleApp6
{
    enum filetypeExtrension { TXT = 1, DOC = 2, DOCX = 3, PDF = 4, PPTX = 5 };
    internal class DataFile:AD_File, Icomparable
    {

        public DataFile(string filename,string data) :base(filename)
        {
            this.Data = data;

        }
        filetypeExtrension type ;
        public filetypeExtrension Type { get { return type; }set { type = value; } }
        string data;
       
        public string Data { get { return data; } set { data = value; } }
        public override int GetSize()
        {
            return Data.Length;
        }
        public override string ToString()
        {
            double size = ((double)GetSize() / 1024);
            return base.ToString() + ""+ size+ " KB ";
        }
        public override bool Equals(object obj)
        {
            if (obj==null||this.GetType()!=obj.GetType())
            {
                return false;
            }
            DataFile data = (DataFile)obj;
            if (data.FileName == this.FileName && data.Data == this.Data)
            {
                return true;
            }
            return false;
        }
    }
}
