using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBus.BusinessLogic;
using ServiceBus.Common.Sender;
using ServiceBus.DataContract;

namespace ServiceBus.WebClient.Controllers
{
    public class LicenceController : Controller
    {
        private LicenceServices licenceServices { get; set; }
        private ITopicSender topicSender { get; set; }

        public LicenceController(LicenceServices licenceServices, ITopicSender topicSender)
        {
            this.licenceServices = licenceServices;
            this.topicSender = topicSender;
        }

        // GET: Licence
        public ActionResult Index()
        {
            var list = licenceServices.GetLicences();
            return View(list);
        }

        // GET: Licence/Create
        public ActionResult Create()
        {
            var model = new Licence();
            return View(model);
        }

        // POST: Licence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Licence model)
        {
            try
            {
                model.CreateDateTime = DateTime.Now;
                topicSender.SendMessagesAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}