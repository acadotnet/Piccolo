using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HashidsNet;
using Piccolo.Models;

namespace Piccolo.Controllers
{
    [RoutePrefix("s")]
    public class ShortUrlController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly Hashids _hashids;

        public ShortUrlController()
        {
            _context = new ApplicationDbContext();
            _hashids = new Hashids("PiccoloUrl", 4);
        }

        [Route("{hash}", Name = "ShortUrl")]
        public ActionResult Index(string hash)
        {
            var id = _hashids.Decode(hash)[0];

            var url = _context.ShortUrls.FirstOrDefault(s => s.Id == id);
            if (url == null)
            {
                return HttpNotFound();
            }

            url.LastLookupDate = DateTime.UtcNow;
            url.LookupCount = url.LookupCount.HasValue
                ? url.LookupCount.Value + 1
                : 1;

            _context.SaveChanges();

            return Redirect(url.LongUrl);
        }
    }
}