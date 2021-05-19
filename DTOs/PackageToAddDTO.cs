using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PackageToAddDTO
    {
        [Required]
        public string SenderAddress { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public int DeliveryManId { get; set; }
        public DateTime DateSent { get; set; }
        public PackageToAddDTO()
        {
            DateSent = DateTime.Now;
        }
    }
}