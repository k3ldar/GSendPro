namespace GSendService.Models
{
    public class MachineServiceViewModel
    {
        public MachineServiceViewModel(List<DateTime> serviceTimes)
        {
            ServiceTimes = serviceTimes ?? throw new ArgumentNullException(nameof(serviceTimes));
        }

        public List<DateTime> ServiceTimes { get; set; }
    }
}
