using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

//TO DO 
// Directory class - name, list of files with their size and name and list with subdirectories 
// File Class - name and size 
// Read line by line
// Make sure directory context is always present 
// cd- change the current directory , ls - lists the contents of the current directory***
// Parse all the input
// - Pay attention to the current directory
// - if line starts with cd - change current directory
// - if line starts with ls - parse files until you read another command - check if it is a new subdirectory or file
// Build the file system tree, starting with the root dir
// with each line , update the directory 
// once the tree is built, calculate the size of each directory,
// check how - because the size is all files + all subdirectories 
// Finally, check which directories have over the specified limit size

//NOTES 
class Day_7a
{

    static void Main_disabled()
    {
        var lines = System.IO.File.ReadAllLines("inputs/Day_7.txt");
        
    }


class Directory
{
    public string Name { get; set; }
    public List<File> Files { get; set; }
    public List<Directory> SubDirectories { get; set; }

    public Directory(string name)
    {
        Name = name;
        Files = new List<File>();
        SubDirectories = new List<Directory>();
    }   // Method to calculate total size
    public int CalculateTotalSize()
    {
        int totalSize = Files.Sum(file => file.Size);
        foreach (var subDir in SubDirectories)
        {
            totalSize += subDir.CalculateTotalSize();
        }
        return totalSize;
    }
}

class File
{
    public string Name { get; set; }
    public int Size { get; set; }

    public File(string name, int size)
    {
        Name = name;
        Size = size;
    }
}

}