using System;

namespace OpenLearnAnswerFetcher.Business
{
    public class OrderAttribute:Attribute
    {
        public OrderAttribute(int orderNum)
        {
            OrderNum = orderNum;
        }

        public int OrderNum { get; set; }
    }
}