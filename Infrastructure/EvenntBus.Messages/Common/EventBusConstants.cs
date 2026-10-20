using System;
using System.Collections.Generic;
using System.Text;

namespace EvenntBus.Messages.Common
{
    public class EventBusConstants
    {
        public const string BasketCheckoutQueue = "basketcheckout-queue";
        public const string OrderCreatedQueue = "ordercreated-queue";
        public const string PaymentCompletedQueue = "paymentcompleted-queue";
        public const string PaymentFailedQueue = "paymentfailed-queue";
    }
}
