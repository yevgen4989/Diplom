using System;
using System.Collections.Generic;

namespace Entities
{
    public class Currency : BaseEntity
    {
        public string Code { get; set; }
        public double Value { get; set; }
        
        
        public ICollection<UserOption> UserOptions { get; set; }
    }
}