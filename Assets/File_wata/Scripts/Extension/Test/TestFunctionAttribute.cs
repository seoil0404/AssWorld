using System;

namespace Wata.Extension.Test {
    
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class TestFunctionAttribute: Attribute {
        public readonly int Priority;
        public readonly string Name;
        public readonly bool RuntimeOnly;

        public TestFunctionAttribute(int priority, string name = "", bool runtimeOnly = false) =>
            (Name, Priority, RuntimeOnly) = (name, priority, runtimeOnly);
        
        public TestFunctionAttribute(string name = "", int priority = 0, bool runtimeOnly = false) =>
            (Name, Priority, RuntimeOnly) = (name, priority, runtimeOnly);
        
    }
}