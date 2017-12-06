using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoanAmortization.Models;

namespace LoanAmortization.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult LoanApp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoanApp(SearchFormView loan)
        {
            List<LoanPaymentView> view = new List<LoanPaymentView>();
            if (ModelState.IsValid)
            {
                view = GetLoanViewData(loan);
            }
            ViewBag.LoanData = view;
            ViewBag.Principal = loan.LoanPrincipal;
            ViewBag.Interest = view.Last().CummulativeInterestAmount;
            ViewBag.Payment = view.Last().ScheduledPayment;
            ViewBag.PaymentNumber = view.Last().PaymentNo;

            return View();
        }

        public ViewResult LoanData(List<LoanPaymentView> LoanData)
        {
            List <LoanPaymentView> data= new List<LoanPaymentView>();
            if (LoanData.Any())
            {
                data = LoanData;
            }
            return View(data);
        }


        private List<LoanPaymentView> GetLoanViewData(SearchFormView loan)
        {
            List<LoanPaymentView> listloan = new List<LoanPaymentView>();
            int TotalNumberofPayments = loan.NoOfPaymentYears * loan.NoOfYearlyInstallmentalPayments;
            double scheduledpayment = GetScheduledPayment(TotalNumberofPayments,loan.LoanPrincipal,loan.InterestRate);
            double startingbalance; double cummulativeInterest=0;
            DateTime lastpaymenttime=DateTime.Parse(loan.PaymentStartDate);
            startingbalance = loan.LoanPrincipal;
            
            for (int i = 1; i <= TotalNumberofPayments; i++)
            {
                LoanPaymentView payment = new LoanPaymentView();
                payment = GetPayment(startingbalance,loan.InterestRate, scheduledpayment);
                payment.PaymentNo = i;
                payment.ScheduledPayment = scheduledpayment;
                cummulativeInterest += payment.InterestAmount;
                payment.CummulativeInterestAmount = cummulativeInterest;
                payment.paymentDate = lastpaymenttime.AddMonths(1).ToShortDateString();
                startingbalance = payment.EndingBalance;
                lastpaymenttime = DateTime.Parse(payment.paymentDate);
                listloan.Add(payment);
                 
            }

            return listloan;
        }

        private LoanPaymentView GetPayment(double startingbalance, double InterestRate, double scheduledpayment)
        {
            LoanPaymentView payment = new LoanPaymentView();
           // payment.ScheduledPayment = scheduledpayment;
            payment.BeginningBalance = startingbalance;
            
            double firstmul = 100 * 12;
            double monthlyInterest = startingbalance * InterestRate / firstmul;
            payment.InterestAmount = Math.Round(monthlyInterest,2);
            payment.PrincipalAmount = Math.Round((scheduledpayment - monthlyInterest),2);
            payment.EndingBalance = Math.Round(startingbalance - payment.PrincipalAmount, 2);
            return payment;
        }

        private double GetScheduledPayment(int totalNumberofPayments, double loanPrincipal, double interestRate)
        {
            double firstmul = 100 * 12;
            double intRate = interestRate / firstmul;
            double monthly = (loanPrincipal * (Math.Pow((1 + intRate), totalNumberofPayments)) *
                intRate / (Math.Pow((1 + intRate), totalNumberofPayments) - 1));
            monthly= Math.Round(monthly, 2); ;
            return monthly;
        }
    }
}