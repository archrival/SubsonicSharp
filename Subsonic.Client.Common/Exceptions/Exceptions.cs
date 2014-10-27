﻿using System;
using Subsonic.Common.Classes;

namespace Subsonic.Client.Exceptions
{
    public class SubsonicErrorException : Exception
    {
        public SubsonicErrorException() { }

        public SubsonicErrorException(string message, Error error) : base(message)
        {
            Error = error;
        }

        public SubsonicErrorException(string message) : base(message)
        {
            Error = new Error {Message = message, Code = -1};
        }

        public SubsonicErrorException(string message, Exception innerException) : base(message, innerException)
        {
            Error = new Error {Message = message, Code = -1};
        }

        private Error Error { get; set; }
    }

    public class SubsonicApiException : Exception
    {
        public SubsonicApiException() { }

        public SubsonicApiException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public SubsonicApiException(string message) : base(message)
        {

        }
    }

    public class SubsonicInvalidApiException : Exception
    {
        public SubsonicInvalidApiException() { }

        public SubsonicInvalidApiException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public SubsonicInvalidApiException(string message) : base(message)
        {

        }
    }
}
