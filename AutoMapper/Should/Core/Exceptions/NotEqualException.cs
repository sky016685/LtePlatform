namespace AutoMapper.Should.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when two values are unexpectedly equal.
    /// </summary>
    public class NotEqualException : AssertActualExpectedException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="NotEqualException"/> class.
        /// </summary>
        /// <param name="expected">The expected object value</param>
        /// <param name="actual">The actual object value</param>
        /// <param name="userMessage">The user message to be shown on failure</param>
        public NotEqualException(object expected, object actual, string userMessage = "CustomizeAssert.NotEqual() Failure")
            : base(expected, actual, userMessage) { }
    }
}