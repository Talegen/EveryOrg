/*
 * Talegen Every.org API Client Library
 * (c) Copyright Talegen, LLC.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/
namespace Talegen.EveryOrg.Client
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// This class contains the exception for the Persona API client.
    /// </summary>
    public class ApiClientException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClientException"/> class.
        /// </summary>
        public ApiClientException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClientException"/> class.
        /// </summary>
        /// <param name="message">Contains the exception message.</param>
        public ApiClientException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClientException"/> class.
        /// </summary>
        /// <param name="message">Contains the exception message.</param>
        /// <param name="innerException">Contains the inner exception.</param>
        public ApiClientException(string message, Exception innerException)
            : base(message, innerException)
        {
            if (innerException is HttpRequestException hex)
            {
                this.HttpRequestException = hex;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClientException"/> class.
        /// </summary>
        /// <param name="message">Contains the exception message.</param>
        /// <param name="innerException">Contains the inner exception.</param>
        public ApiClientException(string message, Exception innerException, int statusCode)
            : base(message, innerException)
        {
            if (innerException is HttpRequestException hex)
            {
                this.HttpRequestException = hex;
            }

            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Gets or sets an optional HTTP request exception.
        /// </summary>
        public HttpRequestException? HttpRequestException { get; }

        /// <summary>
        /// Gets or sets an optional HTTP response message.
        /// </summary>
        public int? StatusCode { get; }
    }

}
