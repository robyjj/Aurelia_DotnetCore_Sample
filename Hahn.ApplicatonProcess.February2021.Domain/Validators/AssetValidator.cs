
using System;
using FluentValidation;

using Hahn.ApplicatonProcess.February2021.Models;


namespace Hahn.ApplicatonProcess.February2021.Domain.Validators
{
    public class AssetValidator : AbstractValidator<Asset>
    {
        public AssetValidator()
        {
            RuleFor(model => model.AssetName).NotEmpty().MinimumLength(5);
            RuleFor(model => model.EMailAdressOfDepartment).NotEmpty().EmailAddress();
            RuleFor(model => model.Broken).NotNull();
            RuleFor(model => model.Department).NotNull().IsInEnum().WithMessage("Invalid Department");
            RuleFor(model => model.PurchaseDate).NotNull().Must(NotOlderThanOneYear).WithMessage("Purchase Date is older than a year.");
        }
        private bool NotOlderThanOneYear(DateTime dateTime)
        {
            if ((DateTime.UtcNow - dateTime).TotalDays > 365)
            {
                return false;
            }
            return true;
        }
    }
}
