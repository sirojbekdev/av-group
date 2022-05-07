using AspNetCoreHero.ToastNotification.Abstractions;
using AvGroup.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;

namespace AvGroup
{
    public class PoolFormController : Controller
    {
        private readonly INotyfService _notyf;
        public PoolFormController(INotyfService notyf)
        {
            _notyf = notyf;
        }
        // GET: PoolFormController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PoolFormController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoolFormController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PoolForm poolForm)
        {
            if (ModelState.IsValid)
            {
                var password = "Joris1998";
                var MyEmail = "joris5969@gmail.com";
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Hello "+ poolForm.FirstName + " " + poolForm.LastName);

                var estimate = poolForm.GetEstimate();
                stringBuilder.AppendLine("Estimate: " + estimate.ToString() + "$");
                stringBuilder.AppendLine("Basement: " + poolForm.GetBasement().ToString());
                stringBuilder.AppendLine("Pool type: " + poolForm.PoolType);
                stringBuilder.AppendLine("Majolica: " + poolForm.Majolica);
                stringBuilder.AppendLine("With waterheating: " + poolForm.Waterheating);
                stringBuilder.AppendLine("With Cobra: " + poolForm.Cobra);
                stringBuilder.AppendLine("With lighting: " + poolForm.Lighting);
                stringBuilder.AppendLine("With waterslides: " + poolForm.Waterslides);
                stringBuilder.AppendLine("Number of stairs: " + poolForm.StairsNumber);
                stringBuilder.AppendLine("Number of Geysers: " + poolForm.GeyserNumber);
                if (poolForm.ContactMe)
                {
                    try
                    {
                        using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                        {
                            stringBuilder.AppendLine("Phone Number: " + poolForm.PhoneNumber);
                            stringBuilder.AppendLine("Email: " + poolForm.Email);
                            client.EnableSsl = true;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Credentials = new NetworkCredential(MyEmail, password);
                            MailMessage message = new MailMessage();
                            message.From = new MailAddress(MyEmail);
                            message.To.Add(new MailAddress(poolForm.Email));
                            message.To.Add(addresses: "sirojbekdev@gmail.com");
                            message.Subject = "Swimming Pool Estimates";
                            message.Body = stringBuilder.ToString();
                            client.Send(message);
                            _notyf.Success(estimate.ToString(), 200);
                        }
                        ModelState.Clear();
                        return View();
                    }
                    catch (Exception)
                    {
                        //ModelState.Clear();
                        return View();
                    }
                }
                else
                {
                    _notyf.Success(estimate.ToString()+ "$", 200);
                    ModelState.Clear();
                    return View();
                }
            }
            //ModelState.Clear();
            return View();
        }
       
    }
}
