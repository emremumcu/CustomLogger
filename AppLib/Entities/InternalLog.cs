namespace AppLib.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InternalLogs")]
    public class InternalLog: BaseEntity
    {
        public string LogLevel { get; set; }
        public string EventId { get; set; }
        public string State { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerExceptionMessage { get; set; }
    }
}
