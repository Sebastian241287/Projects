using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceCHAT
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        //string GetData(int value);
        [WebGet(UriTemplate = "Login/{username}/{geslo}", ResponseFormat = WebMessageFormat.Json)]
        int Login(string username, string geslo);

        [OperationContract]
        [WebInvoke(UriTemplate = "Send/{username}/{message}")]
        //CompositeType GetDataUsingDataContract(CompositeType composite);
        void Send(string username, string message);


        [OperationContract]
        [WebGet(UriTemplate = "Messages", ResponseFormat = WebMessageFormat.Json)]
        string Messages();

        [OperationContract]
        [WebGet(UriTemplate = "Messages/{id}", ResponseFormat = WebMessageFormat.Json)]
        string Messages2(string id);
        // TODO: Add your service operations here

        [OperationContract]
        [WebGet(UriTemplate = "Id", ResponseFormat = WebMessageFormat.Json)]
        string Id();
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
