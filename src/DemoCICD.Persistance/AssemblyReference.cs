using System.Reflection;

namespace DemoCICD.Persistance
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
