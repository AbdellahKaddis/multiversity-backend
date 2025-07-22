using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class DuplicateValueException : BadRequestException
    {
        public DuplicateValueException(IEnumerable<string> duplicateFields)
        : base($"Duplicate values found for: {string.Join(", ", duplicateFields)}")
        {
        }
    }
}
