﻿using System;

namespace LeagueLeo.Domain.Exception
{
    [Serializable()]
    public class WordNotFoundException : System.Exception
    {
        public WordNotFoundException(Guid wordId) 
            : base($"Word with id {wordId} not found")
        {
        }

        public WordNotFoundException() {
        }

        public WordNotFoundException(string message) : base(message)
        {
        }

        public WordNotFoundException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected WordNotFoundException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
        { }

    }
}
