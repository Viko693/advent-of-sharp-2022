
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

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


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

        // Calculates the total size of files in this directory and all subdirectories.
        public int CalculateTotalSize()
        {
            // Start with the total size of files directly in this directory.
            int totalSize = 0;
            foreach (var file in Files)
            {
                totalSize += file.Size;
            }
            // Add the size of files in all subdirectories, recursively.
            foreach (var subDir in SubDirectories)
            {
                totalSize += subDir.CalculateTotalSize();
            }
            return totalSize;
        }
    }

    private static Directory root = new Directory("/");

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
        Directory root = ParseFileSystem(lines); // No need to redeclare root, use the static field directly.

        int sumOfSizes = CalculateDirectorySizes(root); // Calculate sizes using the root directory.

        Console.WriteLine($"The sum of total sizes for directories with size <= 100,000 is: {sumOfSizes}");
    }




    // Parses the entire filesystem from the given lines of input.
    static Directory ParseFileSystem(string[] lines)
    {
        // Create the root directory.
        Directory currentDirectory = root;
        // Flag to track when we're listing directory contents.
        bool isListingContents = false;

        // Process each line to build the filesystem.
        foreach (var line in lines)
        {
            ProcessLine(line, ref currentDirectory, ref isListingContents);
        }

        // Return the fully parsed root directory.
        return root;
    }

    // Processes a single line of input to build the filesystem.
    static void ProcessLine(string line, ref Directory currentDirectory, ref bool isListingContents)
    {
        // Remove leading '$' and whitespace from the line.
        string trimmedLine = line.TrimStart(new char[] { '$', ' ' });

        if (trimmedLine.StartsWith("cd "))
        {
            ProcessCdCommand(trimmedLine, ref currentDirectory);
        }
        else if (trimmedLine.StartsWith("ls"))
        {
            isListingContents = true;
            Console.WriteLine($"Listing contents of directory: {currentDirectory.Name}");
        }
        // Handle the actual listing of files and directories after 'ls'.
        else if (isListingContents)
        {
            ProcessLsContents(trimmedLine, currentDirectory);
        }
    }
    static void ProcessCdCommand(string command, ref Directory currentDirectory)
    {
        // Extract the directory path from the command.
        var path = command.Substring(3).Trim();

        // If the path is "/", we're changing to the root directory.
        if (path == "/")
        {
            currentDirectory = root; // Set the current directory to root.
            Console.WriteLine("Changed to root directory.");
        }
        // If the path is "..", we're moving up to the parent directory.
        else if (path == "..")
        {
            // Make sure the current directory isn't already the root directory.
            if (currentDirectory.Parent != null)
            {
                currentDirectory = currentDirectory.Parent; // Set the current directory to its parent.
                Console.WriteLine($"Changed to parent directory: {currentDirectory.Name}"); // Confirm the change for the user.
            }
        }
        // Otherwise, we're changing to a subdirectory within the current directory.
        else
        {
            // Attempt to find the subdirectory by name.
            Directory subDirectory = currentDirectory.SubDirectories.FirstOrDefault(d => d.Name == path);

            // If found, change to that subdirectory.
            if (subDirectory != null)
            {
                currentDirectory = subDirectory; // Change the current directory to the found subdirectory.
                Console.WriteLine($"Changed to directory: {currentDirectory.Name}");
            }
            else
            {
                Console.WriteLine($"Directory '{path}' not found.");
            }
        }
    }

    // Parses the contents listed after the 'ls' command and creates new directories or files
    static void ProcessLsContents(string contentLine, Directory currentDirectory)
    {
        if (contentLine.StartsWith("dir "))
        {

            string dirName = contentLine.Substring(4).Trim();

            // Make a new directory object and remember who its parent is.
            Directory newDirectory = new Directory(dirName) { Parent = currentDirectory };

            // Stick this new directory in the current one.
            currentDirectory.SubDirectories.Add(newDirectory);


            Console.WriteLine($"Created directory: {dirName}");
        }
        // If the line starts with a number, it's a file with its size listed first.
        else if (char.IsDigit(contentLine[0]))
        {
            // Break the line into size and name.
            var parts = contentLine.Split(new[] { ' ' }, 2);

            // Check we've got two bits: the size and the name.
            if (parts.Length == 2 && int.TryParse(parts[0], out int fileSize))
            {

                string fileName = parts[1];


                File newFile = new File(fileName, fileSize);


                currentDirectory.Files.Add(newFile);


                Console.WriteLine($"Created file: {fileName} ({fileSize} bytes)");
            }
        }
    }

    // Method to traverse the tree and calculate sizes
    static int CalculateDirectorySizes(Directory directory)
    {
        int sumOfSizes = 0;

        // Include the size of the current directory if it meets the condition
        int dirSize = directory.CalculateTotalSize();
        if (dirSize <= 100000)
        {
            sumOfSizes += dirSize;
        }

        // Recursively calculate sizes for subdirectories
        foreach (var subDir in directory.SubDirectories)
        {
            sumOfSizes += CalculateDirectorySizes(subDir);
        }

        return sumOfSizes;
    }

}


