using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signarutes.Domain.Contracts.models
{
    public enum SignatureRequestStatus
    {
        New,
        Failed,
        Signed,
        Rejected,
        Expired
    }
}
