using System;
using System.Collections.Generic;
using System.Text;

namespace EvenntBus.Messages.Events
{
    public class PaymentFailedEvent : BaseIntegrationEvent
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string Reason { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
