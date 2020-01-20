using System;
using System.Collections.Generic;

namespace APIUser2_0
{
    public partial class Users
    {
        public Users()
        {
            SubscribeSubscriberNavigation = new HashSet<Subscribe>();
            SubscribeSubscribetoNavigation = new HashSet<Subscribe>();
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string Bio { get; set; }

        public ICollection<Subscribe> SubscribeSubscriberNavigation { get; set; }
        public ICollection<Subscribe> SubscribeSubscribetoNavigation { get; set; }
    }
}
