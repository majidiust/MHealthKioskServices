using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MKioskService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMHealthKioskService" in both code and config file together.
    [ServiceContract]
    public interface IMHealthKioskService
    {
        [OperationContract]
        String AddVisit(String DeviceInfo, String NationalID, String MobileNumber, String Systolic, String Diastolic, String SPO2Percentage, String SPO2PulseRate);
    }
}
