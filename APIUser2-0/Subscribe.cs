using System;
using System.Collections.Generic;

namespace APIUser2_0
{
    public partial class Subscribe
    {
        public Guid Subscriber { get; set; }
        public Guid Subscribeto { get; set; }

        public Users SubscriberNavigation { get; set; }
        public Users SubscribetoNavigation { get; set; }
    }
}
