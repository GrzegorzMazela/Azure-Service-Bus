using ServiceBus.DataContract;
using System;
using System.Collections.Generic;

namespace ServiceBus.BusinessLogic
{
    public class LicenceServices
    {
        private static List<Licence> Licences = new List<Licence>();

        public IEnumerable<Licence> GetLicences()
        {
            return Licences;
        }

        public void AddLicence(Licence licence)
        {
            Licences.Add(licence);
        }
    }
}
