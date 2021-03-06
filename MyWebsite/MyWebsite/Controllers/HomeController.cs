﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Models;
using MyWebsite.Models.Databases;
using MyWebsite.ViewModels;
using System;
using System.Globalization;

namespace MyWebsite.Controllers
{
	public class HomeController : ExtendedController
	{
		public HomeController(DatabaseContext context) : base(context) { }

		[Route("/{id?}")]
		public IActionResult Index(string id = "")
		{
			if (!string.IsNullOrWhiteSpace(id) && Database.ShortLinks.Find(id) is ShortLinkModel link)
				return Redirect(link.Uri.OriginalString);
			else
				return View();
		}

		[Route("Contacts")]
		public IActionResult Contacts() =>
			View();

		[Route("Projects")]
		public IActionResult Projects() =>
			View(new ProjectsViewModel(Database));

		[Route("Construction")]
		public IActionResult Construction() =>
			View();

		[Route("Error")]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() =>
			View();

		[Route("GetError")]
		public IActionResult GetError(int errorCode = 404) =>
			StatusCode(errorCode);

		[Route("SwitchLanguage")]
		public IActionResult SwitchLanguage()
		{
			Response.Cookies.Append(
			CookieRequestCultureProvider.DefaultCookieName,
			CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(
				CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpperInvariant() == "RU" ?
				"en-US" : "ru"
				)),
			new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

			return Redirect(Extensions.CheckNullOrWhitespace(Request.Headers["Referer"], "/"));
		}

		[Route("ComicSans")]
		public IActionResult ComicSans()
		{
			if (Request.Cookies.ContainsKey("useComicSans"))
				Response.Cookies.Delete("useComicSans");
			else
				Response.Cookies.Append("useComicSans", "true");

			return Redirect(Extensions.CheckNullOrWhitespace(Request.Headers["Referer"], "/"));
		}

		[Route("Wishlist")]
		public IActionResult Wishlist() =>
			View(new WishlistViewModel(Database, CultureInfo.CurrentUICulture));
	}
}