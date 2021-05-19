using System;

namespace API.DTOs
{
    public class PackageStatusToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StatusDate { get; set; }
        public int PackageId { get; set; }
    }
}