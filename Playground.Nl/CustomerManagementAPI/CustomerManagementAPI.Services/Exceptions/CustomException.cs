using System;
using System.Collections.Generic;

namespace Playground.Nl.CustomerManagementAPI.Services.Exceptions
{
    public class CustomException : Exception
    {
        public IList<CustomExceptionMessage> Messages { get; set; } = new List<CustomExceptionMessage>();

        public CustomException()
        {
        }

        public CustomException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }

        public CustomException(string message)
            : this("", message)
        {
        }

        public CustomException(string key, string message)
        {
            Messages.Add(new CustomExceptionMessage
            {
                Key = key,
                Message = message
            });
        }

        public CustomException(IEnumerable<CustomExceptionMessage> messages)
        {
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }
    }

    public class CustomExceptionMessage
    {
        public string Key { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}