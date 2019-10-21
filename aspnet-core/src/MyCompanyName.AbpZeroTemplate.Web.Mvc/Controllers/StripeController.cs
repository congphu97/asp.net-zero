using System;
using System.Threading.Tasks;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Mvc;
using MyCompanyName.AbpZeroTemplate.MultiTenancy.Payments;
using MyCompanyName.AbpZeroTemplate.MultiTenancy.Payments.Stripe;
using MyCompanyName.AbpZeroTemplate.MultiTenancy.Payments.Stripe.Dto;
using MyCompanyName.AbpZeroTemplate.Web.Models.Stripe;

namespace MyCompanyName.AbpZeroTemplate.Web.Controllers
{
    public class StripeController : StripeControllerBase
    {
        private readonly StripePaymentGatewayConfiguration _stripeConfiguration;
        private readonly IStripePaymentAppService _stripePaymentAppService;
        private readonly IPaymentAppService _paymentAppService;

        public StripeController(
            StripeGatewayManager stripeGatewayManager,
            StripePaymentGatewayConfiguration stripeConfiguration,
            IStripePaymentAppService stripePaymentAppService,
            IPaymentAppService paymentAppService)
            : base(stripeGatewayManager, stripeConfiguration)
        {
            _stripeConfiguration = stripeConfiguration;
            _stripePaymentAppService = stripePaymentAppService;
            _paymentAppService = paymentAppService;
        }

        public async Task<ActionResult> Purchase(long paymentId, bool isUpgrade = false)
        {
            var payment = await _paymentAppService.GetPaymentAsync(paymentId);
            if (payment.Status != SubscriptionPaymentStatus.NotPaid)
            {
                throw new ApplicationException("This payment is processed before");
            }

            var model = new StripePurchaseViewModel
            {
                PaymentId = payment.Id,
                Amount = payment.Amount,
                Description = payment.Description,
                IsRecurring = payment.IsRecurring,
                Configuration = _stripeConfiguration,
                UpdateSubscription = isUpgrade
            };

            if (payment.IsRecurring)
            {
                if (isUpgrade)
                {
                    return View("UpdateSubscription", model);
                }

                return View("Subscribe", model);
            }

            return View(model);
        }

        [HttpPost]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ActionResult> ConfirmPayment(long paymentId, string stripeToken)
        {
            try
            {
                await _stripePaymentAppService.ConfirmPayment(
                    new StripeConfirmPaymentInput
                    {
                        PaymentId = paymentId,
                        StripeToken = stripeToken
                    });

                return Redirect(await GetSuccessUrlAsync(paymentId));
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);

                return Redirect(await GetErrorUrlAsync(paymentId));
            }
        }

        [HttpPost]
        [UnitOfWork(IsDisabled = true)]
        public async Task<ActionResult> CreateSubscription(long paymentId, string stripeToken)
        {
            try
            {
                await _stripePaymentAppService.CreateSubscription(new StripeCreateSubscriptionInput
                {
                    PaymentId = paymentId,
                    StripeToken = stripeToken
                });

                var returnUrl = await GetSuccessUrlAsync(paymentId);
                return Redirect(returnUrl);
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);

                var returnUrl = await GetErrorUrlAsync(paymentId);
                return Redirect(returnUrl);
            }
        }

        [HttpPost]
        [UnitOfWork(IsDisabled = true)]
        public virtual async Task<IActionResult> UpdateSubscription(long paymentId)
        {
            try
            {
                await _stripePaymentAppService.UpdateSubscription(
                    new StripeUpdateSubscriptionInput
                    {
                        PaymentId = paymentId
                    });

                var returnUrl = await GetSuccessUrlAsync(paymentId);
                return Redirect(returnUrl);
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);

                var returnUrl = await GetErrorUrlAsync(paymentId);
                return Redirect(returnUrl);
            }
        }

        private async Task<string> GetSuccessUrlAsync(long paymentId)
        {
            var payment = await _paymentAppService.GetPaymentAsync(paymentId);
            return payment.SuccessUrl + (payment.SuccessUrl.Contains("?") ? "&" : "?") + "paymentId=" + paymentId;
        }

        private async Task<string> GetErrorUrlAsync(long paymentId)
        {
            var payment = await _paymentAppService.GetPaymentAsync(paymentId);
            return payment.ErrorUrl + (payment.ErrorUrl.Contains("?") ? "&" : "?") + "paymentId=" + paymentId;
        }
    }
}