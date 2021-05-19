using System;
using API.Entities;

namespace API.DTOs
{
    public class PackageStatusToAddDTO
    {
        public string Name { get; set; }
        public DateTime StatusDate { get; set; }
        public int PackageId { get; set; }
        public PackageStatusToAddDTO(StatusName status)
        {
            switch(status)
            {
                case(StatusName.Send):
                    Name = "Wysłano.";
                    break;
                case(StatusName.InTransport):
                    Name = "W transporcie.";
                    break;
                case(StatusName.ToBeDelivered):
                    Name = "Wydano do doręczenia.";
                    break;
                case(StatusName.Delivered):
                    Name = "Doręczono.";
                    break;
            }
            
            StatusDate = DateTime.Now;
        }
    }
}