using System;
using System.IO;
using System.Xml;

class  Question_2b_Solution
{
    static void Main()
    {   // Prepare the text file
        var lines = File.ReadAllLines("inputs/Day_2.txt");
        // prepare the total score variable 
        int totalScore = 0;
        // loop through the input file lines 
        foreach (string line in lines)
        {
            // Extract the opponent's move (first character) and your move (third character)
            char opponentMove = line[0];
            char yourMove = line[2];

            int moveScore = GetMoveScore(yourMove);
            int outcomeScore = GetOutcomeScore(opponentMove, yourMove);

            totalScore += moveScore + outcomeScore;
        }
        //output the total score
        Console.WriteLine("Total Score: " + totalScore);
    }
    //Function to get the score for your move
    static int GetMoveScore(char move)
    {
        switch (move)
        {
            case 'Q': return 1; //rock
            case 'W': return 2; //paper
            case 'E': return 3; //scissors
            default: return 0;
        }
    }
    //get the outcome score based on both moves
    static int GetOutcomeScore(char opponentMove, char yourMove)
    {
        int score = 0;
        switch ((opponentMove, yourMove))
        {
            case ('A', 'W'): // Rock vs Paper
            case ('B', 'E'): // Paper vs Scissors
            case ('C', 'Q'): // Scissors vs Rock
                score = 6; 
                break;
            case ('A', 'Q'): // Rock vs Rock
            case ('B', 'W'): // Paper vs Paper
            case ('C', 'E'): // Scissors vs Scissors
                score = 3; 
                break;
            default: 
                score = 0;
                break;
        }
        return score;
    }







}