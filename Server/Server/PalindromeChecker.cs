using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Server
{
    class PalindromeChecker
    {
        public static bool IsPalindrome(string text)
        {
            Thread.Sleep(1000);
            text = text.ToLower().Trim();
            int start = 0;
            int end = text.Length - 1;
            for (; start < end; start++, end--)
            {
                Thread.Sleep(20);
                if (text[start] != text[end])
                    return false;
            }
            return true;
        }
    }
}
