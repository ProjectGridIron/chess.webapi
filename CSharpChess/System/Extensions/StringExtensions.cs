﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpChess.System.Extensions
{
    public static class StringExtensions
    {
        private static readonly char[] EndOfLineChars = {'\r', '\n'};

        public static string Repeat(this char s, int times) 
            => new string(s, times);
        public static string Repeat(this string s, int times)
            => Enumerable.Range(1, times).Aggregate("", (a, i) => a+s);

        public static IEnumerable<string> ToLines(this string s, StringSplitOptions options = StringSplitOptions.None)
        {
            return s.Split(EndOfLineChars, options);
        }
    }
}