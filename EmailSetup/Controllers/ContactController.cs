using EmailSetup.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

public class ContactController : Controller
{
    // GET: Contact
    public ActionResult Index()
    {
        return View();
    }

    // POST: Contact
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Index(ContactModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);
                ViewBag.Message = "Your message has been sent successfully.";
                ModelState.Clear();
            }
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error sending email: {ex.Message}";
        }

        return View();
    }

    private void SendEmail(ContactModel model)
    {
        string toEmail = "najeebbrohi9477@gmail.com"; // Replace with your email address

        MailMessage mail = new MailMessage();
        mail.To.Add(toEmail);
        mail.From = new MailAddress(model.Email);
        mail.Subject = model.Subject;
        mail.Body = $"From: {model.Name} ({model.Email})\n\n{model.Message}";

        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
        {
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("najeebbrohi9477@gmail.com", "lotn kimm znro yzcg");
            smtp.Port = 587;

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                // Handle exception (log it, display an error message, etc.)
                throw new Exception($"Error sending email: {ex.Message}", ex);
            }
        }
    }
}
