using System;

namespace GSendService.Models
{
    public class MachineServiceViewModel
    {
        public MachineServiceViewModel(long id, bool maintainServiceSchedule, int serviceWeeks, int spindleHours,
            DateTime nextService, TimeSpan remainingSpindle)
        {
            Id = id;
            MaintainServiceSchedule = maintainServiceSchedule;
            ServiceWeeks = serviceWeeks;
            SpindleHours = spindleHours;
            NextService = nextService;
            RemainingSpindle = remainingSpindle;
        }

        public long Id { get; set; }

        public bool MaintainServiceSchedule { get; set; }

        public int ServiceWeeks { get; set; }

        public int SpindleHours { get; set; }

        public DateTime NextService { get; }

        public TimeSpan RemainingSpindle { get; }
    }
}
