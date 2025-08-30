using System;

namespace Domain.Entities
{
    public class Opportunity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public bool IsOpen { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;
    }
}