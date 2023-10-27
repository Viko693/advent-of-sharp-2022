using System;
using System.IO;
using System.Xml;

class  Day_2a
{
    static void Main_disabled()
    {   // Prepare the text file
        var lines = File.ReadAllLines("inputs/Day_2.txt");
        // prepare the total score variable 
        int totalScore = 0;
        // loop through the input file lines 
        foreach (string line in lines)
        {
            // Extract the opponent's move 
            char opponentMove = line[0];
            char yourMove = line[2];

            int moveScore = GetMoveScore(yourMove);
            int outcomeScore = GetOutcomeScore(opponentMove, yourMove);

            totalScore += moveScore + outcomeScore;
        }
        //output the total score
        Console.WriteLine("Total Score: " + totalScore);
    }
    // get the score for your move
    static int GetMoveScore(char move)
    {
        switch (move)
        {
            case 'X': return 1; //rock
            case 'Y': return 2; //paper
            case 'Z': return 3; //scissors
            default: return 0;
        }
    }
    //get the outcome score  
    static int GetOutcomeScore(char opponentMove, char yourMove)
    {
        int score = 0;
        switch ((opponentMove, yourMove))
        {
            case ('A', 'Y'): // Rock vs Paper
            case ('B', 'Z'): // Paper vs Scissors
            case ('C', 'X'): // Scissors vs Rock
                score = 6; 
                break;
            case ('A', 'X'): // Rock vs Rock
            case ('B', 'Y'): // Paper vs Paper
            case ('C', 'Z'): // Scissors vs Scissors
                score = 3; 
                break;
            default: 
                score = 0;
                break;
        }
        return score;
    }







}
// Possible improvement - create a function that holds the Rock x Paper x Scissors logic, saving computing power 