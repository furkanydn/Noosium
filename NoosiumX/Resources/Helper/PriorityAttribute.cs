using System;

namespace NoosiumX.Resources.Helper
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PriorityAttribute : Attribute
    {
        public PriorityAttribute(int priority) => Priority = priority;
        private int Priority { get; set; }
    }
}

