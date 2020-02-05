using System;

namespace Stocky.Net.Models
{
    public class ApplicationSubscription
    {
        public string Id { get; set; }
        public DateTime? GoodUntil { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
