﻿using System;

namespace ParsingPS
{
    public static class IntParse
    {
        public static int Parse(string text, int startindex = 0)
        {
            int Value = 0;
            char currentChar = text[startindex++];
            bool negative = currentChar == '-';

            while (currentChar >= 0x30 && currentChar <= 0x39)
            {
                Value = (Value * 10) + currentChar - 0x30;
                if (startindex >= text.Length)
                {
                    break;
                }
                currentChar = text[startindex++];
            }

            return negative ? -Value : Value;
        }

        public static int Parse(char text)
        {
            if (text >= 0x30 && text <= 0x39)
            {
                return text - 0x30;
            }
            throw new ArgumentOutOfRangeException(text.ToString());
        }
    }
}