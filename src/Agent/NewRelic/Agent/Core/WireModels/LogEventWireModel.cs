﻿// Copyright 2020 New Relic, Inc. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using NewRelic.Collections;
using NewRelic.SystemExtensions;

namespace NewRelic.Agent.Core.WireModels
{
    public class LogEventWireModel : IHasPriority
    {
        private const uint MaxMessageLengthInBytes = 32 * 1024;

        /// <summary>
        /// The UTC timestamp in unix milliseconds. 
        /// </summary>
        public long TimeStamp { get; }

        /// <summary>
        /// The log message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The log level.
        /// </summary>
        public string Level { get; }

        /// <summary>
        /// The span id of the segment.
        /// </summary>
        public string SpanId { get; }

        /// <summary>
        /// The traced id of the transaction.
        /// </summary>
        public string TraceId { get; }

        /// <summary>
        /// If present, the truncated (300 lines) stack trace of the exception stored as a single string.
        /// </summary>
        public string ErrorStack { get; }

        /// <summary>
        /// If present, the message of the exception.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// If present, exception class name the exception.
        /// </summary>
        public string ErrorClass { get; }

        private float _priority;
        public float Priority
        {
            get { return _priority; }
            set
            {
                const float priorityMin = 0.0f;
                if (value < priorityMin || float.IsNaN(value) || float.IsNegativeInfinity(value) || float.IsPositiveInfinity(value))
                {
                    throw new ArgumentException($"LogEventWireModel requires a valid priority value greater than {priorityMin}, value used: {value}");
                }

                _priority = value;
            }
        }

        public LogEventWireModel(long unixTimestampMS, string message, string level, string spanId, string traceId)
        {
            TimeStamp = unixTimestampMS;
            Message = message.TruncateUnicodeStringByBytes(MaxMessageLengthInBytes);
            Level = level;
            SpanId = spanId;
            TraceId = traceId;
        }

        public LogEventWireModel(long unixTimestampMS, string message, string level, string spanId, string traceId, float priority)
        {
            TimeStamp = unixTimestampMS;
            Message = message.TruncateUnicodeStringByBytes(MaxMessageLengthInBytes);
            Level = level;
            SpanId = spanId;
            TraceId = traceId;
            Priority = priority;
        }

        public LogEventWireModel(long unixTimestampMS, string message, string level, ICollection<string> errorStack, string errorMessage, string errorClass, string spanId, string traceId)
        {
            TimeStamp = unixTimestampMS;
            Message = message.TruncateUnicodeStringByBytes(MaxMessageLengthInBytes);
            Level = level;
            SpanId = spanId;
            TraceId = traceId;
            ErrorStack = string.Join(" \n", errorStack);
            ErrorMessage = errorMessage;
            ErrorClass = errorClass;
        }

        public LogEventWireModel(long unixTimestampMS, string message, string level, ICollection<string> errorStack, string errorMessage, string errorClass, string spanId, string traceId, float priority)
        {
            TimeStamp = unixTimestampMS;
            Message = message.TruncateUnicodeStringByBytes(MaxMessageLengthInBytes);
            Level = level;
            SpanId = spanId;
            TraceId = traceId;
            Priority = priority;
            ErrorStack = string.Join(" \n", errorStack);
            ErrorMessage = errorMessage;
            ErrorClass = errorClass;
        }
    }
}
