using System;
using System.IO;

namespace ParsingPS
{
    class Program
    {
        static void Main(string[] args)
        {
            string importPath = @"c:\Temp\import.txt";
            string exportPath = @"c:\Temp\export.txt";

            string[] readText = File.ReadAllLines(importPath);

            Parsing parsing = new Parsing();
            double handID = parsing.ParseHandId(readText);
            Players[] players = new Players[parsing.PlayersNumberParse(readText)];
            players = parsing.ParsePlayers(readText);

            StreamWriter str = new StreamWriter(exportPath);
            str.WriteLine($"HandID = {handID}");
            for (int i = 0; i < players.Length; i++)
            {
                str.WriteLine($"Seat #{players[i].SeatNumber}, Name: {players[i].PlayerName} , Stack: {players[i].StartingStack}");
            }
            str.Close();


        }

    }
}









