using System;
using System.Globalization;


namespace ParsingPS
{
    class Parsing
    {
        int zoomHandIdStartIndex = 22;
        int gameIdStartIndex = 17;
        int tournamentIdStartindex = 43;
        public double ParseHandId(string[] handLines)
        {



            string line = handLines[0];

            int firstDigitIndex;

            char handIDchar = line[11];
            switch (handIDchar)
            {
                case 'Z':
                    firstDigitIndex = zoomHandIdStartIndex;
                    break;
                case 'H':
                    firstDigitIndex = gameIdStartIndex;
                    break;
                default:
                    if (line[tournamentIdStartindex - 1] == '#')
                    {
                        firstDigitIndex = line.IndexOf('#') + 1;
                    }
                    else
                    {
                        firstDigitIndex = line.LastIndexOf('#') + 1;
                    }
                    break;
            }

            int lastDigitIndex = line.IndexOf(':');

            string handId = line.Substring(firstDigitIndex, lastDigitIndex - firstDigitIndex);
            return double.Parse(handId);
        }

        public int PlayersNumberParse(string[] handLines)
        {
            int playersLastLineRead = -1;
            bool playersLine = false;
            for (int playersLineNumber = 2; playersLineNumber < handLines.Length; playersLineNumber++)
            {
                string playersHandLine = handLines[playersLineNumber].TrimEnd();

                if (!playersLine && !playersHandLine.StartsWith("Seat ") && playersHandLine[6] != ':')
                {
                    continue;
                }
                else if (playersLine && !playersHandLine.StartsWith("Seat "))
                {
                    playersLastLineRead = playersLineNumber;
                    break;
                }
                playersLine = true;

                char playersLastChar = playersHandLine[playersHandLine.Length - 1];
                if (playersLastChar != ')' && playersLastChar != 't')
                {
                    playersLastLineRead = playersLineNumber;
                    break;
                }
            }

            return playersLastLineRead - 2;
        }
        public Players[] ParsePlayers(string[] handLines)
        {
            Parsing playersParsing = new Parsing();
            int x = playersParsing.PlayersNumberParse(handLines);
            Players[] playerList = new Players[x];

            int lastLineRead = -1;
            bool foundSeats = false;



            for (int lineNumber = 2; lineNumber < handLines.Length - 1; lineNumber++)
            {
                string line = handLines[lineNumber].TrimEnd();



                if (!foundSeats && !line.StartsWith("Seat ") && line[6] != ':')
                {
                    continue;
                }
                else if (foundSeats && !line.StartsWith("Seat "))
                {
                    lastLineRead = lineNumber;
                    break;
                }
                foundSeats = true;

                char endChar = line[line.Length - 1];


                if (endChar != ')' && endChar != 't')
                {
                    lastLineRead = lineNumber;
                    break;
                }

                const int seatNumberStartIndex = 4;
                int spaceIndex = line.IndexOf(' ', seatNumberStartIndex);
                int colonIndex = line.IndexOf(':', spaceIndex + 1);
                int seatNumber = IntParse.Parse(line, spaceIndex + 1);

                int openParenIndex = line.LastIndexOf('(');

                if (line[openParenIndex + 1] == 'm')
                {
                    line = line.Remove(openParenIndex);
                    openParenIndex = line.LastIndexOf('(');
                }

                int spaceAfterOpenParen = line.IndexOf(' ', openParenIndex);

                string playerName = line.Substring(colonIndex + 2, (openParenIndex - 1) - (colonIndex + 1));
                char scopeIndex = line[openParenIndex + 1];
                if ((scopeIndex == '0') | (scopeIndex == '1') | (scopeIndex == '2') | (scopeIndex == '3') | (scopeIndex == '4') | (scopeIndex == '5') | (scopeIndex == '6') | (scopeIndex == '7') | (scopeIndex == '8') | (scopeIndex == '9'))
                {
                    string stackString = line.Substring(openParenIndex + 1, spaceAfterOpenParen - (openParenIndex + 1));
                    double stack = Convert.ToDouble(stackString, CultureInfo.InvariantCulture);

                    playerList[lineNumber - 2] = new Players(playerName, stack, seatNumber);
                }
                else
                {
                    string stackString = line.Substring(openParenIndex + 2, spaceAfterOpenParen - (openParenIndex + 2));
                    double stack = Convert.ToDouble(stackString, CultureInfo.InvariantCulture);

                    playerList[lineNumber - 2] = new Players(playerName, stack, seatNumber);
                }
               // double stack = Convert.ToDouble(stackString, CultureInfo.InvariantCulture);

                //playerList[lineNumber - 2] = new Players(playerName, stack, seatNumber);

            }



            return playerList;
        }
    }
}
