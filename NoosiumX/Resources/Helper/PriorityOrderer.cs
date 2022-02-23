namespace NoosiumX.Resources.Helper
{
    using System.Collections.Generic;
    using Xunit.Abstractions;
    using Xunit.Sdk;
    using System;
    using System.Linq;
    
    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var sortedMethod = new SortedDictionary<int, List<TTestCase>>();
            foreach (var testCase in testCases)
            {
                var priority = 0;
                foreach (var attributeInfo in testCase.TestMethod.Method.GetCustomAttributes(typeof(PriorityAttribute)
                             .AssemblyQualifiedName)) priority = attributeInfo.GetNamedArgument<int>("Priority");
                GetOrCreate(sortedMethod,priority).Add(testCase);
            }

            foreach (var list in sortedMethod.Keys.Select(priority => sortedMethod[priority]))
            {
                list.Sort((x,y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name,y.TestMethod.Method.Name));
                foreach (var testCase in list)
                    yield return testCase;
            }
        }

        private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            if (dictionary.TryGetValue(key, out var result)) return result;

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }
}

