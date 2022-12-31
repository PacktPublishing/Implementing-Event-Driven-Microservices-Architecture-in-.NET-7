using CloudNative.CloudEvents;
using MTAEDA.Core.Infrastructure.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel;

namespace MTAEDA.Equipment.RetryFailback
{
    public class IotHubConnectivityWorker: BackgroundWorker
    {
        IEventWriterProvider _producer;
        ILogger _logger;
        public IotHubConnectivityWorker(IEventWriterProvider producer, ILogger logger)
        {
            _producer = producer;
            _logger = logger;
        }
        protected override void OnDoWork(DoWorkEventArgs e)
        {
           while(true)
            {
                try
                {
                    var baseDirectory = e.Argument?.ToString() ?? Environment.CurrentDirectory;
                    if(!Directory.Exists(baseDirectory)) throw new IOException($"Directory does not exist: {baseDirectory}");

                    foreach(var dir in Directory.GetDirectories(baseDirectory))
                    {
                        // Each directory will be a domain
                        var domainName = Path.GetDirectoryName(dir);
                        foreach(var action in Directory.GetDirectories(dir)) {
                            // Each directory will be an action
                            var actionName = Path.GetDirectoryName(action);
                            foreach(var file in Directory.GetFiles(action, "*.json"))
                            {
                                // Attempt to deserialize each JSON file and send it along
                                var evt = JsonConvert.DeserializeObject<CloudEvent>(file);
                                evt.Type = actionName;
                                evt.Source = new Uri($"http://{Environment.MachineName}.RetryFailbackService/{domainName}/{actionName}");
                                _producer.Send(evt);
                            }
                        }
                    }


                } catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }
                Thread.Sleep(2000);
            }
        }
    }
}
