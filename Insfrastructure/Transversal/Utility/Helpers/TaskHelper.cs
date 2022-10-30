using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IFramework.Infrastructure.Utility.Extensions;

namespace IFramework.Infrastructure.Utility.Helpers
{
    public static class TaskHelper
    {
        public static List<TResult> RunTasks<TResult, TObject>(Func<object?, TResult> action, List<TObject> objects, int? taskCount = null)
        {

            if (objects == null || objects.Count == 0)
            {
                return null;
            }

            taskCount = taskCount == null ? objects.Count : (int)taskCount;

            var result = new List<TResult>();

            for (int i = 0; i < objects.Count; i = i + (int)taskCount)
            {
                var range = (int)taskCount;
                var results = RunPartialTasks<TResult, TObject>(action, objects, i, range);
                result.AddRange(results);
            }

            return result;
        }

        private static List<TResult> RunPartialTasks<TResult, TObject>(Func<object?, TResult> action, List<TObject> objects, int start, int end)
        {
            var result = new List<TResult>();
            var tasks = new List<Task<TResult>>();

            var partialObjects = objects.GetByRange<TObject>(start, end);

            for (int i = 0; i < partialObjects.Count; i++)
            {
                tasks.Add(Task<TResult>.Factory.StartNew(action, partialObjects[i]));
            }

            Task.WaitAll(tasks.ToArray());

            tasks.ForEach(t =>
            {
                result.Add(t.Result);
            });

            return result;

        }
    }

}
