using System;

namespace ConsoleApp6
{
    public abstract class AD_File
    {
        public AD_File(string fileName)
        {
            FileName = fileName;
        }
        protected string filename;
        public string FileName
        {
            get { return filename; }
            set
            {
                foreach (char c in value)
                {
                    if (c == '>' || c == '?' || c == '*' || c == ':' || c == '/' || c == '|' || c == '<')
                    {
                        throw new ArgumentException("Invalid characters in the filename");
                    }
                }
                filename = value;
            }
        }

        protected DateTime Date { get; } = DateTime.Now;

        public abstract int GetSize();

        public override string ToString()
        {
            return $"{FileName} {Date} ";
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            AD_File otherFile = (AD_File)obj;
            return FileName == otherFile.FileName;
        }
    }
}
