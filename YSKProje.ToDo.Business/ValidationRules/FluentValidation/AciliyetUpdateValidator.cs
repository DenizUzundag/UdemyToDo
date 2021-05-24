using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.AciliyetDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
    public class AciliyetUpdateValidatorAccessViolationException: AbstractValidator<AciliyetUpdateDto>
    {
        public AciliyetUpdateValidatorAccessViolationException()
        {
            RuleFor(I => I.Tanim).NotNull().WithMessage("Tanım alanı boş geçilemez");
        }
    }
}
