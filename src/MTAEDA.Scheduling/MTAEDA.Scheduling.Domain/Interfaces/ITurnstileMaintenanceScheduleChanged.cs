namespace MTAEDA.Scheduling.Domain.Interfaces
{
    public interface ITurnstileMaintenanceScheduleInfo
    {
        public int MaintenanceScheduleId { get; set; }
        public string CronExpression { get; set; } 
        int TurnstileId { get; }

    }
}
