using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Elevinfo
{
    [ServiceContract]
    public interface IElev
    {

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetElevInfo", Method = "POST")]
        ElevInfo GetElevInfo(long id);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetMentors", Method = "POST")]
        Mentor[] GetMentors(long id);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void GetOptions();

        /*        [OperationContract]
                CompositeType GetDataUsingDataContract(CompositeType composite);
                */
        // TODO: Add your service operations here
    }


    [DataContract]
    public class ElevInfo
    {
        [DataMember]
        public string fornamn { get; set; }
        [DataMember]
        public string efternamn { get; set; }

        [DataMember]
        public string enhet { get; set; }
        [DataMember]
        public string enhetId { get; set; }

        [DataMember]
        public string skolform { get; set; }
        [DataMember]
        public string skolformId { get; set; }

        [DataMember]
        public string avdelning { get; set; }
        [DataMember]
        public string avdelningId { get; set; }

        [DataMember]
        public string klass { get; set; }
        [DataMember]
        public string klassId { get; set; }

        [DataMember]
        public Grupp[] grupper { get; set; }

        [DataMember]
        public string klassarskurs { get; set; }
    }

    [DataContract]
    public class Grupp
    {
        [DataMember]
        public string namn { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string period { get; set; }
    }

    [DataContract]
    public class Mentor
    {
        [DataMember]
        public string fornamn { get; set; }
        [DataMember]
        public string efternamn { get; set; }
        [DataMember]
        public string mail { get; set; }
    }
}
