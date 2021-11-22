using System;
using System.Collections.Generic;
using System.Text;

namespace Riddha.CourierMS.Model
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public string DocumentTypeName { get; set; }
        public string RecipientOrSenderName { get; set; }
        public string SendOrReceiveAddress { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string TrackingNo { get; set; }
    }


    public enum TransactionType
    {
        Send,
        Receive

    }
}
