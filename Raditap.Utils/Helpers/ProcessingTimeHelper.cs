using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Raditap.Utilities.Helpers
{
    public class ProcessingTimeHelper
    {
        private readonly Stopwatch _stopwatch;
        private string _actionName;
        private string _objectName;
        private MeasureOn _measureOn;
        private readonly ILogger<ProcessingTimeHelper> _logger;

        public ProcessingTimeHelper(ILogger<ProcessingTimeHelper> logger)
        {
            _stopwatch = new Stopwatch();
            _logger = logger;
        }

        public void StartLogging(MeasureOn measureOn, string actionName, string objectName)
        {
            _stopwatch.Reset();

            _measureOn = measureOn;
            _actionName = actionName;
            _objectName = objectName;

            _stopwatch.Start();
        }

        public void StopLogging()
        {
            _stopwatch.Stop();
            _logger.LogWarning("Processing time: {MeasureOn} {ActionName} {ObjectName} {ElapsedTime} seconds",
                               _measureOn.ToString(),
                               _actionName,
                               _objectName,
                               _stopwatch.Elapsed.TotalSeconds);
        }
    }

    public enum MeasureOn
    {
        Database,
        ExternalService,
        Jwt
    }
}