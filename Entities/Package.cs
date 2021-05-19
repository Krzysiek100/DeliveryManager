using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class Package
    {
        public int Id { get; set; }
        public string SenderAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public int DeliveryManId { get; set; }
        public AppUser DeliveryMan { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime? DateDelivered { get; set; }
        public ICollection<PackageStatus> Statuses { get; set;}
    }
}