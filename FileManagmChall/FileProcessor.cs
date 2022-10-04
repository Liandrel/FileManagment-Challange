using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagmChall
{
    public static class FileProcessor
    {
        public static void CopyFiles(string sourceDircetory, string destinationDirectory, bool inclideSubfolders, bool flattenStructure)
        {
            SearchOption searchOption;
            if (inclideSubfolders == true)
            {
                searchOption = SearchOption.AllDirectories;
            }
            else
            {
                searchOption = SearchOption.TopDirectoryOnly;
            }

            var fileList = Directory.GetFiles(sourceDircetory, "*", searchOption);

            foreach (string fileToCopy in fileList)
            {
                CopyFile(sourceDircetory, fileToCopy, destinationDirectory, flattenStructure);
            }
        }
        private static void CopyFile(string sourceDirectory, string sourceFile, string destinationDirectory, bool flattenStructure)
        {
            var sourceFolders = sourceDirectory.Split('\\');
            var sourceFileFolders = Path.GetDirectoryName(sourceFile).Split('\\');
            string fileName = Path.GetFileName(sourceFile);
            List<string> subFolders = new();

            for (int i = sourceFolders.Length; i < sourceFileFolders.Length; i++)
            {
                subFolders.Add(sourceFileFolders[i]);
            }
            if (flattenStructure == true)
            {
                string newFileName = "";
                foreach (string subFolder in subFolders)
                {
                    newFileName += subFolder + "_";
                }
                newFileName += fileName;

                string destinationFile = Path.Combine(destinationDirectory, newFileName);
                try
                {
                    File.Copy(sourceFile, destinationFile, false);
                }
                catch (IOException)
                {
                    //Ignore and continue to not overwrite
                }
            }
            else
            {
                string newDestination = destinationDirectory;
                foreach (string subFolder in subFolders)
                {
                    newDestination = Path.Combine(newDestination, subFolder);
                }

                Directory.CreateDirectory(newDestination);

                string newFileName = Path.Combine(newDestination, fileName);
                try
                {
                    File.Copy(sourceFile, newFileName, false);

                }
                catch (IOException)
                {
                    //Ignore and continue to not overwrite
                }
            }
        }
    }
}
