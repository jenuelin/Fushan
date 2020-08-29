using System;

namespace DataServices.Model
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid MemberId { get; set; }
    }
}
