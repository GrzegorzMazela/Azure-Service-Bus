using System;
using System.ComponentModel;

namespace ServiceBus.DataContract
{
    public class Licence
    {
        [DisplayName("Licence Number")]
        public string LicenceNumber { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}