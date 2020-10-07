using Core.REST.Api.Models;
using FluentValidation;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.REST.Api.Validators
{
    public class RequestValidator : AbstractValidator<TransactionRequest>
    {
        public RequestValidator()
        {
            RuleFor(x => x.MerchantInfo.TransactionType)
                .NotEmpty()
                .WithMessage("Transaction Type is Required");

            RuleFor(x => x.MerchantInfo.MerchantKey)
                 .NotEmpty()
                 .WithMessage("Merchant Key is Required");

            RuleFor(x => x.MerchantInfo.ProcessorId)
                .NotEmpty()
                .WithMessage("Processor Id is Required");

            RuleFor(x => x.MerchantInfo.IpAddress)
               .NotEmpty()
               .Must(ValidateIPv4)
               .WithMessage("Ip Address is Required")
               .Length(1, 16)
               .WithMessage("Ip Address allow only 16 characters");

            RuleFor(x => x.OrderIdIsUnique)
                .NotEmpty()
                .WithMessage("OrderIdIsUnique is Required");

            RuleFor(x => x.AutoGenerateorderid)
                .NotEmpty()
                .WithMessage("AutoGenerateorderid is Required");

            RuleFor(x => x.TransactionAmount)
               .NotEmpty()
               .WithMessage("Transaction Amount is Required");

            RuleFor(x => x.CardNumber)
               .Length(13, 16)
               .WithMessage("Card Number allow only 16 characters")
               .Must(IsWholeNumber)
               .WithMessage("Invalid Card Number")
               .When(x => x.CardNumber != null);

            RuleFor(x => x.OwnerName)
              .Length(1, 750)
              .WithMessage("Owner Name allow only 750 characters")
              .When(x => x.OwnerName != null);

            RuleFor(x => x.OwnerStreet)
              .Length(1, 250)
              .WithMessage("Owner Street allow only 250 characters")
              .When(x => x.OwnerStreet != null);

            RuleFor(x => x.OwnerStreet2)
              .Length(1, 250)
              .WithMessage("Owner Street2 allow only 250 characters")
              .When(x => x.OwnerStreet2 != null);

            RuleFor(x => x.OwnerCity)
               .Length(1, 100)
               .WithMessage("Owner City allow only 100 characters")
               .When(x => x.OwnerCity != null);

            RuleFor(x => x.OwnerState)
              .Length(1, 100)
              .WithMessage("Owner State allow only 100 characters")
              .When(x => x.OwnerState != null);

            RuleFor(x => x.OwnerZip)
              .Length(1, 10)
              .WithMessage("Owner Zip allow only 10 characters")
              .When(x => x.OwnerZip != null);

            RuleFor(x => x.OwnerCountry)
              .Length(1, 200)
              .WithMessage("Owner Country allow only 200 characters")
              .When(x => x.OwnerCountry != null);

            RuleFor(x => x.OwnerEmail)
              .Length(1, 300)
              .WithMessage("Owner Email allow only 300 characters")
              .EmailAddress()
              .WithMessage("Invalid Owner Email")
              .When(x => x.OwnerEmail != null);

            RuleFor(x => x.OwnerPhone)
              .Length(1, 25)
              .WithMessage("Owner Phone allow only 25 characters")
              .When(x => x.OwnerPhone != null);

            RuleFor(x => x.CloseDate)
               .Must(BeAValidDate)
               .WithMessage("Close Date an Invalid Date Format")
               .When(x => x.CloseDate != null);

            RuleFor(x => x.RecurringStartDate)
               .Must(BeAValidDate).WithMessage("Recurring Start Date an Invalid Date Format")
               .When(x => x.RecurringStartDate != null);

            RuleFor(x => x.RecurringEndDate)
               .Must(BeAValidDate).WithMessage("Recurring End Date an Invalid Date Format")
               .When(x => x.RecurringEndDate != null);

            RuleFor(x => x.CustomerRefNo)
               .Length(1, 75)
               .WithMessage("Customer Ref No allow only 75 characters")
               .When(x => x.CustomerRefNo != null);

            RuleFor(x => x.ShippingZip)
               .Length(1, 9)
               .WithMessage("Shipping Zip allow only 9 characters")
               .When(x => x.ShippingZip != null);
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
        public bool IsWholeNumber(String strNumber)
        {
            if (strNumber != null)
            {
                Regex objNotWholePattern = new Regex("[^0-9]");
                return !objNotWholePattern.IsMatch(strNumber);
            }
            {
                return false;
            }
        }

        public bool ValidateIPv4(string ipString)
        {
            if (string.IsNullOrEmpty(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
    }
}
