using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PMS.Web.UI.Responses
{
    public class ResponseBase
    {
        //[DataMember]
        //public ServiceResultType ResultType = ServiceResultType.Success;

        [DataMember]
        public string ErrorMessage;

        public int Total { get; set; }
    }
}
