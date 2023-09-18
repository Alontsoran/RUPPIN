using ConsoleApp6;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;

namespace ConsoleApp5
{
    internal class Folder : AD_File, IComplete
    {
        string path;
        AD_File[] Files;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public static Folder root  = new Folder("root","");

        public Folder(string name, string path) : base(name)
        {
            Path =path+"\\";
        }
        public static Folder Cd(string path)
        {
            bool folderFound = false;
            string[] folderNames = path.Split('\\');
            Folder currentFolder = root;
            foreach (string folderName in folderNames)
            {
                if (folderName == "")
                    continue;
              
                foreach (AD_File file in currentFolder.Files)
                {
                    if (file is Folder folder && folder.FileName == folderName)
                    {
                        currentFolder = folder;
                        folderFound = true;
                        break;
                    }
                }
                if (folderFound == false)
                {
                    Console.WriteLine("Folder not found");
                }
            }
            return currentFolder;
        }
       
        public string GetFullPath()
        {
            return path+"\\"+FileName;
        }

        public override int GetSize()
        {
            int sum = 0;
            if (Files==null)
            {
                return sum=0;
            }
          
            foreach (AD_File file in Files)
            {
                if (file is DataFile)
                {
                    DataFile dataFile = (DataFile)file;
                    sum += dataFile.GetSize();
                }
                else if (file is Folder)
                {
                    Folder subFolder = (Folder)file;
                    sum += subFolder.GetSize(); 
                }
            }
            return sum;
        }

        public void AddFileToArray(AD_File file)
        {
            if (Files==null)
            {

                AD_File[] temp = new AD_File[1];
                temp[0] = file;
                Files = temp;
            }
            else
            {
                foreach (AD_File existingFile in Files)
                {
                    if (existingFile.Equals(file))
                    {
                        throw new ArgumentException("File already exists.");
                    }
                }
                AD_File [] temp = new AD_File[Files.Length + 1];
                for (int i = 0; i < Files.Length; i++)
                {
                    temp[i] = Files[i];
                }
                temp[temp.Length - 1] = file;
                Files = temp;
            }
        }
        public DataFile MkFile(string filename, string data)
        {
            DataFile newFile = new DataFile(filename+".txt", data);
            AddFileToArray(newFile);
            return newFile;
        }

        public void MkDir(string folderName)
        {
            Folder newFolder = new Folder(folderName, Path+folderName);
            AddFileToArray(newFolder);
        }

        public override string ToString()
        {
            string result = "";
            if (Files==null)
            {
                return result;
            }
            foreach (AD_File file in Files)
            {
                if (file is Folder)
                {
                    Folder a = (Folder)file;
                    result +=a.FileName+" "+a.Date+" <DIR> "+"\n";
                }
                if (file is DataFile)
                {
                    DataFile a = (DataFile)file;
                    result += a.ToString() + "\n";
                }
            }
            return result;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Folder other = (Folder)obj;
            if (!string.Equals(FileName, other.FileName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return AreFilesAndSubfoldersEqual(Files, other.Files);
        }
        private bool AreFilesAndSubfoldersEqual(AD_File[] files1, AD_File[] files2)
        {
            if (files1 == null && files2 == null)
            {
                return true;
            }
            if (files1 == null || files2 == null || files1.Length != files2.Length)
            {
                return false;
            }
            for (int i = 0; i < files1.Length; i++)
            {
                if (!files1[i].Equals(files2[i]))
                {
                    return false;
                }

                if (files1[i] is Folder folder1 && files2[i] is Folder folder2)
                {
                    if (!AreFilesAndSubfoldersEqual(folder1.Files, folder2.Files))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool IsFull(int num)
        {
            if (GetSize()>num)
            {
                return true;

            }
               return false;
        }
        public bool Fc(string source, string dest)
        {
            AD_File sourceFile = Cd(source);
            AD_File destFile =Cd(dest);
            if (sourceFile.Equals(destFile))
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        private AD_File GetFile(string path)
        {
            foreach (AD_File file in Files)
            {
                Folder a = (Folder)file;
                if (a.GetFullPath() == path)
                {
                    return file;
                }
            }
            return null;
        }
    }
}



    

