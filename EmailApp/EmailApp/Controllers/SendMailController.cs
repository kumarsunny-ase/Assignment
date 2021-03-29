using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using EmailApp.Models;
using System.IO;

namespace EmailApp.Controllers
{
    public class SendMailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Email em)
        {
            string from = em.From;
            string to = em.To;

            string cc = em.Cc;
            string bcc = em.Bcc;
            string subject = em.Subject;
            string body = em.Body;
            MailMessage mm = new MailMessage();
            mm.To.Add(to);
            mm.CC.Add(cc);
            mm.Bcc.Add(bcc);
            mm.Subject = subject;
            mm.Body = body;
            mm.From = new MailAddress("beingh456@gmail.com");
            if (em.Attachment.Length > 0)
            {
                string fileName = Path.GetFileName(em.Attachment.FileName);
                mm.Attachments.Add(new Attachment(em.Attachment.OpenReadStream(), fileName));
            }
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new System.Net.NetworkCredential("beingh456@gmail.com", "being12345");
            smtp.Send(mm);
            // mm.Attachments.Add(new Attachment("C:\\file.zip"));
            ViewBag.message = "The Mail Has Been Sent Successfully.";
            return View();
        }
    }


    //[HttpPost]
    //    public IActionResult Index(EmailModel model)
    //    {
    //        using (MailMessage mm = new MailMessage(model.Email, model.To))
    //        {
    //            mm.Subject = model.Subject;
    //            mm.Body = model.Body;
    //            if (model.Attachment.Length > 0)
    //            {
    //                string fileName = Path.GetFileName(model.Attachment.FileName);
    //                mm.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), fileName));
    //            }
    //            mm.IsBodyHtml = false;
    //            using (SmtpClient smtp = new SmtpClient())
    //            {
    //                smtp.Host = "smtp.gmail.com";
    //                smtp.EnableSsl = true;
    //                NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
    //                smtp.UseDefaultCredentials = true;
    //                smtp.Credentials = NetworkCred;
    //                smtp.Port = 587;
    //                smtp.Send(mm);
    //                ViewBag.Message = "Email sent.";
    //            }
    //        }

    //        return View();
    //    }
    //}
}

  