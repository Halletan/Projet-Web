using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace SpaceAdventures.Application.Common.RetryPolicies
{
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }
        public AsyncCircuitBreakerPolicy<HttpResponseMessage> CircuitBreakerRetry { get; }  

        public ClientPolicy()
        {
            // As indicated by his name, this will immediately launch retries after each failure

            ImmediateHttpRetry =
                Policy
                    .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                    .RetryAsync(5);

            // Here we wait a bit and we retry again
            LinearHttpRetry = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3));

            // Exponential BackOff, It will increase the time after each failure, we use it for real scenarios
            ExponentialHttpRetry = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            // Circuit Breaker : When a problem is detected the circuit breaker moves to the open state,
            // blocking all requests for specified period.
            // After that period elapses the circuit breaker moves to a half-open state where the first request is treated as a test request.
            // If this request succeeds the circuit closes and normal operation resumes,
            // but if it fails the circuit moves back to open and remains there for a specified period before once again moving to half-open.

            CircuitBreakerRetry = Policy
                .HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

        }
    }
}
