﻿using System.Reflection;

namespace DemoCICD.Domain
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(Assembly).Assembly;
    }
}
