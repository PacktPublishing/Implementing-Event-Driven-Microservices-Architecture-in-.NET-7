using Polly;

namespace PollyTester
{
    public class ServiceBusinessLogic
    {
        public readonly AsyncPolicy retryPolicy;
        string _returnMessage = "OK";

        public ServiceBusinessLogic()
        {
            retryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(3, retryAttempt =>
            {
                return TimeSpan.FromSeconds(retryAttempt++);
            }, (e, t) =>
            {
                Console.WriteLine($"Exception caught for retry: {e.Message}");
                // Add code to perform additional operations if needed
            });



        }

        public async Task<string> TestMethod(string result)
        {
            return await retryPolicy.ExecuteAsync<string>(async () =>
             {
                 return await Task.Run(async () =>
                 {
                     try
                     {
                         // Run some nonsense code here to get a failure
                         var rdm = new Random();
                         if (rdm.NextInt64() % 2 == 0)
                         {
                             throw new InvalidOperationException("Random exception thrown.");
                         }

                         if (string.IsNullOrEmpty(result.Trim())) throw new ArgumentNullException(paramName: nameof(result), message: "You forgot to add a value.");
                         _returnMessage = $"String values can be anything you like: {result}";

                         return PolicyResult<string>.Successful(_returnMessage, null).Result;
                     }
                     catch (TimeoutException ex)
                     {
                         Console.WriteLine("Handled");
                         return PolicyResult<string>.Failure(ex, ExceptionType.HandledByThisPolicy, null).Result;
                     }
                 });

             });

        }


    }
}