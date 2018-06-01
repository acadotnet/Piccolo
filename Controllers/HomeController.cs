using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HashidsNet;
using Microsoft.AspNet.Identity;
using Piccolo.Models;
using Piccolo.ViewModels;

namespace Piccolo.Controllers
{
    [RoutePrefix("")]
    [Authorize]
    public class HomeController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly Hashids _hashids;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _hashids = new Hashids("PiccoloUrl", 4);
        }

        [AllowAnonymous]
        [Route("", Name = "About")]
        public ActionResult About()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("CreateUrl");
            }

            return View();
        }

        [Route("Create" , Name = "CreateUrl")]
        public ActionResult Create()
        {
            var absoluteUrl = Request.Url.AbsoluteUri;
            var userId = User.Identity.GetUserId();

            var vm = new UrlsViewModel
            {
                NewUrls = new List<PiccoloShortUrl>(),
                PopularUrls = new List<PiccoloShortUrl>(),
                RecentUrls = new List<PiccoloShortUrl>()
            };

            var newUrls = _context.ShortUrls.Where(u => u.ApplicationUserId == userId).OrderByDescending(u => u.CreateDate).Take(5).ToList();
            foreach (var u in newUrls)
            {
                var hash = _hashids.Encode(u.Id);
                var route = Url.RouteUrl("ShortUrl", new { hash = hash });
                vm.NewUrls.Add(PiccoloShortUrl.From(absoluteUrl, route, u.CreateDateHumanized, u.LookupCount ?? 0));
            }

            var popularUrls = _context.ShortUrls.Where(u => u.ApplicationUserId == userId && u.LookupCount.HasValue).OrderByDescending(u => u.LookupCount).Take(5).ToList();
            foreach (var u in popularUrls)
            {
                var hash = _hashids.Encode(u.Id);
                var route = Url.RouteUrl("ShortUrl", new { hash = hash });
                vm.PopularUrls.Add(PiccoloShortUrl.From(absoluteUrl, route, u.CreateDateHumanized, u.LookupCount ?? 0));
            }

            var recentUrls = _context.ShortUrls.Where(u => u.ApplicationUserId == userId && u.LastLookupDate.HasValue).OrderByDescending(u => u.LastLookupDate).Take(5).ToList();
            foreach (var u in recentUrls)
            {
                var hash = _hashids.Encode(u.Id);
                var route = Url.RouteUrl("ShortUrl", new { hash = hash });
                vm.RecentUrls.Add(PiccoloShortUrl.From(absoluteUrl, route, u.LastLookupDateHumanized, u.LookupCount ?? 0));
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateUrl", Name = "CreateUrlPost")]
        public ActionResult Create(string UrlToPiccolo)
        {

            var shortUrl = new ShortUrl
            {
                LongUrl = UrlToPiccolo,
                CreateDate = DateTime.UtcNow,
                ApplicationUserId = User.Identity.GetUserId()
            };

            _context.ShortUrls.Add(shortUrl);
            _context.SaveChanges();

            var hash = _hashids.Encode(shortUrl.Id);

            var absoluteUrl = Request.Url.AbsoluteUri;
            var route = Url.RouteUrl("ShortUrl", new { hash = hash });
            var uri = new Uri(new Uri(absoluteUrl), route);
            TempData["PiccoloUrl"] = uri.ToString();

            return RedirectToRoute("CreateUrl");
        }
    }
}