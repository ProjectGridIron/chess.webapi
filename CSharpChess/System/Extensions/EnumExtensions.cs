﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpChess.System.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetAll<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}