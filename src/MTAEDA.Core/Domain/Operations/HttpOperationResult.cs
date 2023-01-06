using MTAEDA.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MTAEDA.Core.Domain.Operations
{
    public class HttpOperationResult<T> : OperationResult<T>
    {
        public override T? Value { get; set; }
        public string StatusMessage { get;private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public HttpOperationResult() {
            StatusMessage = Resources.SuccessMessage;
        }

        public static HttpOperationResult<T> Create()
        {
            return new HttpOperationResult<T> { StatusMessage = SetStatusMessage(HttpStatusCode.OK) };
        }

        public static HttpOperationResult<T> Create(T? value)
        {
            var operationResult = Create();
            operationResult.Value = value;
            return operationResult;
        }
        public static HttpOperationResult<T> Create(T? value, HttpStatusCode statusCode) { 
        if((int)statusCode >= 400 && (int)statusCode < 600)
            {
                throw new InvalidOperationException("If the HTTP status code is between 400 and 600, a corresponding Exception object should also be set.");
            }
            var operationResult = Create(value);
            operationResult.StatusCode = statusCode;
            return operationResult;
        }

        public static HttpOperationResult<T> Create(T? value, HttpStatusCode statusCode, Exception exception)
        {
            var operationResult = Create(value, statusCode);
            operationResult.Exception = exception;
            return operationResult;
        }

        private static string SetStatusMessage(HttpStatusCode statusCode)
        {
            int statusCodeValue = (int)statusCode;
            string statusMessage = Resources.SuccessMessage;
            switch(statusCodeValue)
            {
                case 401:
                    statusMessage = Resources.BadAuthenticationMessage; break;
                case 403:
                    statusMessage = Resources.BadPermissionsMessage; break;
                case 500:
                case 501:
                case 502:
                case 503:
                case 504:
                    statusMessage = Resources.GeneralErrorMessage; break;
                default:
                    statusMessage = Resources.SuccessMessage; break;
            }
            return statusMessage;
        }
    }
}
