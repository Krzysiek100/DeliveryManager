using System;

namespace API.DTOs
{
    public class PackageToReturnDTO
    {
        public int Id { get; set; }
        public string SenderAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliverManUserName { get; set; }
        public int DeliveryManPhoneNumber { get; set; }
        public DateTime DateSent { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime? CurrentStatusDate { get; set; }

    }
}