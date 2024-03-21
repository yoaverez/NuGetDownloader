namespace Utils
{
    /// <summary>
    /// Extension method for the <see cref="Task"/> class.
    /// </summary>
    public static class TaskExtensionMethods
    {
        /// <summary>
        /// A waiter for this task instance.
        /// </summary>
        /// <typeparam name="T">The return type the task to wait.</typeparam>
        /// <param name="task">The task to wait.</param>
        /// <returns>The result of this task instance.</returns>
        public static T TaskWaiter<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }
    }
}
