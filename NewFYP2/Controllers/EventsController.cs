using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewFYP2.Data;
using NewFYP2.Models;
using NuGet.Protocol.Plugins;

namespace NewFYP2.Controllers
{
    [Authorize(Roles = "User")]
    public class EventsController : Controller
    {

        ////Global Declarations (For Sending Email)
        //string mailBody = "You can type anything here";
        //string mailTitle = "Email with Final testing";
        //string mailSubject = "Email Testing";
        //string fromEmail = "lookhai8888@gmail.com";
        //string mailPassword = "efnlfrlyisfzsiyd";
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult AboutUs()
        {

            return View();
        }

        public IActionResult FAQ()
        {

            return View();
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewEventTable.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NewEventTable == null)
            {
                return NotFound();
            }

            var @event = await _context.NewEventTable
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        //this in the end will not be here ...... will be in user part when they joined an event
        public async Task <IActionResult> Send(int id, [Bind("EventID,EventName,Date,Time,Venue,OrganizerName,OrganizerEmail,OrganizerPhoneNo,EventDescription")] Event @event)
        {
            var eventObject = await _context.NewEventTable.FindAsync(id);
            //METHOD 1
            //=============
            //try
            //{
            //    //Mail Message
            //    MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress("zikeenyin@gmail.com"));

            //    //Mail content
            //    message.Subject = mailSubject;
            //    message.Body = mailBody;

            //    //server details
            //    SmtpClient smtp = new SmtpClient();
            //    smtp.Host = "smtp.gmail.com";
            //    smtp.Port = 587;
            //    smtp.EnableSsl = true;
            //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //    //credentials
            //    System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
            //    credential.UserName = fromEmail;
            //    credential.Password = mailPassword;
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Credentials = credential;

            //    //send Email
            //    smtp.Send(message);
            //    ViewBag.message = "Email sent";
            //}
            //catch(Exception ex)
            //{
            //    ViewBag.message = "Email Error";
            //}



            //METHOD 2
            //=============
            //try
            //{
            //var senderEmail = new MailAddress("lookhai8888@gmail.com", "Keen");
            //var receiverEmail = new MailAddress("zikeenyin@gmail.com", "Receiver");
            //var password = "efnlfrlyisfzsiyd";
            //string sub = "Here is the demo gmail";
            //string body = "You can text thing in here";
            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = true,
            //    Credentials = new NetworkCredential(senderEmail.Address, password)
            //};
            //using (var mess = new MailMessage(senderEmail, receiverEmail)
            //{
            //    Subject = sub,
            //    Body = body
            //})
            //{
            //    smtp.Send(mess);

            //}
            //ViewBag.message = "Email Sent";

            //}
            //catch (Exception)
            //{
            //    ViewBag.message = "Some Error";
            //}


            //METHOD 3
            //=============
            try
            {
                using (MailMessage mm = new MailMessage("lookhai8888@gmail.com", "zikeenyin@gmail.com"))
                {
                    mm.Subject = "Here is your event details";
                    mm.Body = "Your Event will be on here:-" + "\n\n\n" + "Event ID: " + eventObject.EventID
                                                               + "\n" + "Event Name: " + eventObject.EventName
                                                               + "\n" + "Date: " + eventObject.Date
                                                               + "\n" + "Time: " + eventObject.Time
                                                               + "\n" + "Venue: " + eventObject.Venue
                                                               + "\n" + "Organizer Name: " + eventObject.OrganizerName
                                                               + "\n" + "Organizer Email: " + eventObject.OrganizerEmail
                                                               + "\n" + "Organizer Phone No: " + eventObject.OrganizerPhoneNo
                                                               + "\n" + "Event Description: " + eventObject.EventDescription;

                    //mm.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;


                        NetworkCredential cred = new NetworkCredential("lookhai8888@gmail.com", "efnlfrlyisfzsiyd");
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = cred;
                        smtp.Port = 587;


                        smtp.Send(mm);

                        ViewBag.message = "Email Sent Successfully";
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.message = "Email Send Error";

            }
            return RedirectToAction("Index");

        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,EventName,Date,Time,Venue,OrganizerName,OrganizerEmail,OrganizerPhoneNo,EventDescription")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NewEventTable == null)
            {
                return NotFound();
            }

            var @event = await _context.NewEventTable.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,EventName,Date,Time,Venue,OrganizerName,OrganizerEmail,OrganizerPhoneNo,EventDescription")] Event @event)
        {
            if (id != @event.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NewEventTable == null)
            {
                return NotFound();
            }

            var @event = await _context.NewEventTable
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NewEventTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NewEventTable'  is null.");
            }
            var @event = await _context.NewEventTable.FindAsync(id);
            if (@event != null)
            {
                _context.NewEventTable.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.NewEventTable.Any(e => e.EventID == id);
        }
    }
}
