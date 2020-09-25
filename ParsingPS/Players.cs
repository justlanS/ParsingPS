using System;
using System.Collections.Generic;
using System.Text;

namespace ParsingPS
{
    public class Players
    {
        public string PlayerName { get; private set; }

        public double StartingStack { get; private set; }

        public int SeatNumber { get; private set; }

        public Players(string playerName,
                      double startingStack,
                      int seatNumber)
        {
            PlayerName = playerName;
            StartingStack = startingStack;
            SeatNumber = seatNumber;

        }
              
    }


}
