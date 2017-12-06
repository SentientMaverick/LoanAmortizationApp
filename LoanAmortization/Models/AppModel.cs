using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LoanAmortization.Models
{
    public class SearchFormView
    {
        [Key]
        public int formid { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Loan Amount")]
        public int LoanPrincipal { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Loan Period in Years")]
        public int NoOfPaymentYears { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "No of Payments per year")]
        public int NoOfYearlyInstallmentalPayments { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Annual Interest Rate")]
        public int InterestRate{ get; set; }
        [Display(Name = "Start Date Of Loan")]
        public string PaymentStartDate { get; set; }
    }

    public class LoanPaymentView
    {
        [Key]
        public int PaymentNo { get; set; }
        [Display(Name = "Start Balance")]
        public double BeginningBalance { get; set; }
        public double ScheduledPayment { get; set; }
        [Display(Name = "Principal")]
        public double PrincipalAmount { get; set; }
        [Display(Name = "End Balance")]
        public double EndingBalance { get; set; }
        [Display(Name = "Interest")]
        public double InterestAmount { get; set; }

        public string paymentDate { get; set; }

        public double CummulativeInterestAmount { get; set; }
    }

}