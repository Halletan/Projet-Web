using Polly;
using Polly.Retry;

namespace SpaceAdventures.MVC.Policies
{
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }

        public ClientPolicy()
        {
            // As indicated by his name, this will immediately launch retries after each failure

            ImmediateHttpRetry =
                Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode).RetryAsync(5);

            // Here we wait a bit we retry again

            LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3));

            // Exponential BackOff, It will increase the time after each failure, we use it foe real scenarios
            ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }
    }
}
