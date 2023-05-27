using Cdr.ErrorManagement;
using System.ComponentModel.DataAnnotations;

namespace Cdr.SharedLibrary
{
    public class BetweenDatesDTO : IValidatableObject
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateFrom == DateTime.MinValue)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_DATE);
            }
            if (DateTo == DateTime.MinValue)
            {
                yield return new ValidationResult(ErrorManager.ErrorCodes.INVALID_DATE);
            }
        }
    }
}
