﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
namespace MKioskService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MHealthKioskService" in code, svc and config file together.
    public class MHealthKioskService : IMHealthKioskService
    {
        public String AddVisit(String DeviceInfo, String NationalID, String MobileNumber, String Systolic, String Diastolic, String SPO2Percentage, String SPO2PulseRate)
        {
            try
            {
                DatabaseDataContext db = new DatabaseDataContext();
                VisitDescribtion newVisit = new VisitDescribtion();
                Random random = new Random();
                int generatedCode = random.Next(10000, 100000);
                newVisit.Confirmed = false;
                newVisit.KioskSerial = DeviceInfo;
                newVisit.MobileNumber = MobileNumber;
                newVisit.NationalityCode = NationalID;
                newVisit.NIBPDiastol = Diastolic;
                newVisit.NIBPSystol = Systolic;
                newVisit.SPO2 = SPO2Percentage;
                newVisit.RR = SPO2PulseRate;
                newVisit.SendSMS = false;
                newVisit.GeneratedCode = generatedCode.ToString();
                db.VisitDescribtions.InsertOnSubmit(newVisit);
                db.SubmitChanges();

                string json = "{\"mobile\":\"" + MobileNumber + "\"," +
                                  "\"nibp_sys\":\"" + Systolic + "\"," +
                                  "\"nibp_dia\":\"" + Diastolic + "\"," +
                                  "\"spo2\":\"" + SPO2Percentage + "\"," +
                                  "\"heart_rate\":\"" + SPO2PulseRate + "\"," +
                                  "\"national_id\":\"" + NationalID + "\"," +
                                  "\"device_id\":\"" + DeviceInfo + "\"" + "}";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["data"] = json;
                    var response = wb.UploadValues("http://5.200.78.14:80/api/v1/kiosk_services", "POST", data);
                    return response.ToString();
                }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex.Message;
            }
        }
    }
}
