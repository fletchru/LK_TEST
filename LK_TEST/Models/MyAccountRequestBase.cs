using System;

namespace LK_TEST.Models
{
    public abstract class MyAccountRequestBase
    {
        public Guid UserId { get; set; }

        public Guid RequestId { get; set; }
    }
}
