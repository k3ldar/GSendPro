

using System.Text.Json.Serialization;

namespace GSendShared
{
    [JsonConverter(typeof(Converters.JsonConverterJobExecution))]
    public interface IJobExecution
    {
        long Id { get; }

        IMachine Machine { get; }

        IJobProfile JobProfile { get; }

        IToolProfile ToolProfile { get; }

        DateTime StartDateTime { get; }

        DateTime FinishDateTime { get; }

        JobExecutionStatus Status { get; }

        bool Simulation { get; }

        void Start(bool simulation);

        void Finish();
    }
}
