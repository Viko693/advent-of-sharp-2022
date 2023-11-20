using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
// Creating a boolean isListingContents is useful -
//- otherwise you would have to manually put a lot of extra clauses to change core functionalities in the loop


class Day_7a
{

    class Directory
    {
        public string Name { get; set; }
        public List<File> Files { get; set; }
        public List<Directory> SubDirectories { get; set; }
        public Directory Parent { get; set; }
        public Directory(string name)
        {
            Name = name;
            Files = new List<File>();
            SubDirectories = new List<Directory>();

        }

        // Method to calculate total size
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
    static void Main()
    {
        var lines = System.IO.File.ReadAllLines("inputs/Day_7.txt");
        Directory root = ParseFileSystem(lines);

        // Use the root for further operations like calculating sizes
    }

    static Directory ParseFileSystem(string[] lines)
    {
        Directory root = new Directory("/");
        Directory currentDirectory = root;
        bool isListingContents = false;

        foreach (var line in lines)
        {
            string trimmedLine = line.TrimStart(new char[] { '$', ' ' });

            // If it's a 'cd' command, process it
            if (trimmedLine.StartsWith("cd "))
            {
                // No longer listing contents if we encounter a 'cd' command
                isListingContents = false;

                var path = trimmedLine.Substring(3).Trim(); // Extract the path after "cd "
                if (path == "/")
                {
                    currentDirectory = root; // Change to root directory
                    Console.WriteLine("Changed to root directory.");
                }
                else if (path == "..")
                {
                    if (currentDirectory.Parent != null)
                    {
                        currentDirectory = currentDirectory.Parent; // Change to parent directory
                        Console.WriteLine($"Changed to parent directory: {currentDirectory.Name}");
                    }
                }
                else
                {
                    Directory subDirectory = null; // Start with no subdirectory found
                    foreach (var dir in currentDirectory.SubDirectories)
                    {
                        if (dir.Name == path)
                        {
                            subDirectory = dir; // Subdirectory found
                            break; // Exit the loop once the directory is found
                        }
                    }
                    if (subDirectory != null)
                    {
                        currentDirectory = subDirectory; // Change to the specified subdirectory
                        Console.WriteLine($"Changed to subdirectory: {currentDirectory.Name}");
                    }
                }
            }
            // If it's an 'ls' command, toggle the flag to start listing contents
            else if (trimmedLine.StartsWith("ls"))
            {
                isListingContents = true;
                Console.WriteLine($"Listing contents of directory: {currentDirectory.Name}");
            }
            // If we are listing contents, process directory or file creation
            else if (isListingContents)
            {
                if (trimmedLine.StartsWith("dir "))
                {
                    string dirName = trimmedLine.Substring(4).Trim(); // Extract the directory name
                    Directory newDirectory = new Directory(dirName) { Parent = currentDirectory };
                    currentDirectory.SubDirectories.Add(newDirectory); // Add new directory
                    Console.WriteLine($"Created new directory: {dirName}");
                }
                else if (char.IsDigit(trimmedLine[0])) // Simple check to assume it's a file
                {
                    var parts = trimmedLine.Split(new[] { ' ' }, 2);
                    if (parts.Length == 2 && int.TryParse(parts[0], out int fileSize))
                    {
                        string fileName = parts[1];
                        File newFile = new File(fileName, fileSize);
                        currentDirectory.Files.Add(newFile); // Add new file
                        Console.WriteLine($"Created new file: {fileName} of size {fileSize}");
                    }
                }
            }
        }

        return root;
    }


    // ... Directory and File classes





}