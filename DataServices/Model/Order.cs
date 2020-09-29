using System;

namespace DataServices.Model
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}