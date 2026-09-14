using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures;

public class AdmissionParameters : RequestParameters
{
    public Guid? FacultyId { get; set; }
    public Guid? programId { get; set; }
}
