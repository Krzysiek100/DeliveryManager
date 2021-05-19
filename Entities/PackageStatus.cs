using System;

namespace API.Entities
{
    public enum StatusName 
    {
        Send,
        InTransport,
        ToBeDelivered,
        Delivered
    }
    public class PackageStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StatusDate { get; set; }
        public int PackageId { get; set; }
        public Package Package { get; set; }
        
    }
}