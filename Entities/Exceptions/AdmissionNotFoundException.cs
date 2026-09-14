using System;
namespace Entities.Exceptions;

public  class AdmissionNotFoundException : NotFoundException
{
   public AdmissionNotFoundException(Guid admissionId): base($"Admission with Id : {admissionId} does not exist in the database.") { }
}
