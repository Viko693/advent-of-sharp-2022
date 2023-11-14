using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
// Looks like spending more time doing Day_6a paid off, 
//I only had to change the length to 13 from 3 in the FindMaker function
class Day_6b
{

    static void Main_disabled()
    {
        var dataStreams = System.IO.File.ReadAllLines("inputs/Day_6.txt");
        foreach (var dataStream in dataStreams)
        {
            var position = FindMarker(dataStream);
            Console.WriteLine($"First marker after character: {position}");
        }
    }


    static int FindMarker(string dataStream)
    {
        for (int i = 13; i < dataStream.Length; i++)
        {
            if (CharDiffCheck(dataStream, i - 13, i))
            {
                return i + 1; // +1 because positions start at 1, not 0
            }
        }
        return -1; // Return -1 if no marker is found
    }

    // Checking if the characters are the same in a loop in case more or less items in 1 sequence are needed to be checked
    static bool CharDiffCheck(string dataStream, int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            for (int j = i + 1; j <= end; j++)
            {
                if (dataStream[i] == dataStream[j])
                {
                    return false;
                }
            }
        }
        return true;
    }

}

