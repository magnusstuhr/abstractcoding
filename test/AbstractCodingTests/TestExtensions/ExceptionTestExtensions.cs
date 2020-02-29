using System;
using Xunit;

namespace AbstractCodingTests.TestExtensions
{
    public static class ExceptionTestExtensions
    {
        public static void Verify<T>(this Exception exception, string expectedMessage) where T : Exception
        {
            Assert.NotNull(exception);
            Assert.IsType<T>(exception);
            Assert.Equal(expectedMessage, exception.Message);
        }

        public static void VerifyArgumentException(this Exception exception, string expectedMessage,
            string parameterName = null)
        {
            var exceptionMessage = CreateExceptionMessageForParameterName(parameterName, expectedMessage);
            exception.Verify<ArgumentException>(exceptionMessage);
        }

        public static void VerifyArgumentNullException(this Exception exception, string parameterName,
            string expectedMessage = null)
        {
            expectedMessage ??= "Value cannot be null.";
            var exceptionMessage =
                CreateExceptionMessageForParameterName(parameterName, expectedMessage);
            exception.Verify<ArgumentNullException>(exceptionMessage);
        }

        private static string CreateExceptionMessageForParameterName(string parameterName, string expectedMessage)
        {
            var paramNameMessage = CreateParameterMessageFragment(parameterName);
            var exceptionMessage = !IsNullOrWhiteSpace(parameterName)
                ? ConcatExceptionMessageWithParameterMessage(expectedMessage, paramNameMessage)
                : expectedMessage;
            return exceptionMessage;
        }

        private static string ConcatExceptionMessageWithParameterMessage(string exceptionMessage,
            string paramNameMessage)
        {
            return $"{exceptionMessage}{paramNameMessage}";
        }

        private static string CreateParameterMessageFragment(string parameterName)
        {
            return $" (Parameter '{parameterName}')";
        }

        private static bool IsNullOrWhiteSpace(string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
    }
}