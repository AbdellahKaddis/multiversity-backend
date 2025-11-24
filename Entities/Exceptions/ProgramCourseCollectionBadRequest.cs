using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions;

public sealed class ProgramCourseCollectionBadRequest : BadRequestException
{
    public ProgramCourseCollectionBadRequest()
    : base("Program course collection sent from a client is null.")
    {
    }
}
