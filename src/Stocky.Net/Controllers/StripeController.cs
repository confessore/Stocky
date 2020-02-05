using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stocky.Net.Models;
using Stocky.Net.Services.Interfaces;
using Stripe;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Stocky.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        readonly UserManager<ApplicationUser> userManager;
        readonly IEmailSender emailSender;
        readonly IDiscordService discordService;

        public StripeController(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IDiscordService discordService)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.discordService = discordService;
        }

        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], Environment.GetEnvironmentVariable("StockyStripeSSK"));

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    await HandlePaymentIntentSucceeded(paymentIntent);
                }
                /*else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    HandlePaymentMethodAttached(paymentMethod);
                }*/
                // ... handle other event types
                else
                {
                    // Unexpected event type
                    return BadRequest();
                }
                return Ok();
            }
            catch (StripeException)
            {
                return BadRequest();
            }
        }

        private async Task HandlePaymentIntentSucceeded(PaymentIntent paymentIntent)
        {
            var user = await userManager.GetUserAsync(User);
            var invite = await discordService.CreateNewInviteAsync();
            await emailSender.SendEmailAsync(user.Email, "Your Discord Invitation", invite);
        }
    }
}