using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceOrientatedArchitecture
{
    [ServiceContract]
    public class ServerFault
    {
        public ServerFault(string reason)
        {
            Reason = new FaultReason(reason);
        }
        private FaultReason fReason;
        public FaultReason Reason
        {
            get { return fReason; }
            set { fReason = value; }
        }
    }
}
