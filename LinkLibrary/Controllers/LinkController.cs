using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using AutoMapper;
using LinkLibrary.Entities;
using LinkLibrary.Models;
using LinkLibrary.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using LinkLibrary.Helpers;
using Microsoft.AspNetCore.Mvc.Routing;
//using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LinkLibrary.Controllers
{
    [Route("links")]
    public class LinkController : Controller
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;

        private ILinkLibraryRepository _linkLibraryRepository;
        public LinkController(ILinkLibraryRepository linkLibraryRepository, UserManager<IdentityUser<int>> userManager,
            SignInManager<IdentityUser<int>> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _linkLibraryRepository = linkLibraryRepository;
        }
        [Route("{userId}/Add")]
        public IActionResult Add(int userId)
        {
            var u = Convert.ToInt32(_userManager.GetUserId(HttpContext.User));
            if (u != userId) return Redirect("http://localhost:52690/login");
            
            return View();
        }

        [HttpPost("{userId}/Add")]
        public IActionResult Add(int userId, LinkToAddDto linkToAddDto)
        {
            
            var u = Convert.ToInt32(_userManager.GetUserId(HttpContext.User));
            if (u != userId) return Redirect("http://localhost:52690/login");
            
            

            var result = Mapper.Map<Link>(linkToAddDto);
            _linkLibraryRepository.AddLink(userId, result);
            _linkLibraryRepository.Save();

            var toReturn = Mapper.Map<LinkToAddDto>(result);

            return Redirect("http://localhost:52690/links/" + userId);
        }


        [HttpGet("{userId}", Name ="LinkList")]
        public IActionResult GetLinks(int userId, string sort="id", int page=1, int pageSize=5)
        {
            
            var u = Convert.ToInt32(_userManager.GetUserId(HttpContext.User));
            if (u != userId) return Redirect("http://localhost:52690/login");
            
            var links = _linkLibraryRepository.GetLinks(userId);
           
            var linksToReturn = Mapper.Map<IEnumerable<LinkViewDto>>(links);
            foreach (var link in linksToReturn)
            {
                link.Title = GetDetails(link.Address).Title;
                link.Provider = GetDetails(link.Address).Provider;
                link.ThumbnailUrl = GetDetails(link.Address).ThumbnailUrl;
                link.AuthorName = GetDetails(link.Address).AuthorName;
                link.Duration = GetDetails(link.Address).Duration;
                link.Likes = GetDetails(link.Address).Likes;
                link.UserId = userId;
            }

            var result = linksToReturn.AsQueryable<LinkViewDto>().ApplySort(sort).ToList();
            var totalResult = result.Count();
            var totalPages = (int)Math.Ceiling((double)totalResult / pageSize);

            var urlHelper = new UrlHelper(Url.ActionContext);
            var prevLink = page > 1 ? urlHelper.Link("LinkList",
                new
                {
                    page = page - 1,
                    pageSize,
                    sort
                }) : "";
            var nextLink = page < totalPages ? urlHelper.Link("LinkList",
                new
                {
                    page = page + 1,
                    pageSize,
                    sort
                }) : "";

            var paginationHeader = new
            {
                currentPage = page,
                pageSize,
                totalResult,
                totalPages,
                previousPageLink = prevLink,
                nextPageLink = nextLink
            };
            HttpContext.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            // return new JsonResult(linksToReturn.AsQueryable<LinkViewDto>().ApplySort(sort).ToList());
            return View(result.Skip(pageSize * (page - 1)).Take(pageSize).ToList());
        }

       // [HttpGet("{userId}/Edit/{linkId}")]
        public IActionResult GetLink(int userId, int linkId)
        {
            
            var u = Convert.ToInt32(_userManager.GetUserId(HttpContext.User));
            if (u != userId) return Redirect("http://localhost:52690/login");
            
            var link = _linkLibraryRepository.GetLink(userId, linkId);
            
            var linkToReturn = Mapper.Map<LinkViewDto>(link);
            linkToReturn.Title = GetDetails(link.Address).Title;
            linkToReturn.Provider = GetDetails(link.Address).Provider;
            linkToReturn.ThumbnailUrl = GetDetails(link.Address).ThumbnailUrl;
            linkToReturn.AuthorName = GetDetails(link.Address).AuthorName;
            linkToReturn.Duration = GetDetails(link.Address).Duration;
            linkToReturn.Likes = GetDetails(link.Address).Likes;
            
            return View(new JsonResult(linkToReturn));
        }

        [HttpGet("{userId}/Edit/{linkId}")]
        public IActionResult UpdateLink()
        {
            return View();
        }

        [HttpPost("{userId}/Edit/{linkId}")]
        public IActionResult UpdateLink(int userId, int linkId, LinkToAddDto linkPatchDocument)
        {
            
            var u = Convert.ToInt32(_userManager.GetUserId(HttpContext.User));
            if (u != userId) return Redirect("http://localhost:52690/login");
            
            var link = _linkLibraryRepository.GetLink(userId, linkId);
            if (link == null) return NotFound();

            link.Address = linkPatchDocument.Address;
            
            if(!_linkLibraryRepository.Save()) return StatusCode(500, "A problem happened while handling your request.");
            return Redirect("http://localhost:52690/links/" + userId);
        }

        [HttpGet("{userId}/Delete/{linkId}")]
        public IActionResult DeleteLink(int userId, int linkId)
        {
            
            var u = Convert.ToInt32(_userManager.GetUserId(HttpContext.User));
            if (u != userId) return Redirect("http://localhost:52690/login");
            
            var link = _linkLibraryRepository.GetLink(userId, linkId);
            if (link == null) return NotFound();

            _linkLibraryRepository.DeleteLink(linkId);
            if(!_linkLibraryRepository.Save()) return StatusCode(500, "A problem happened while handling your request.");
            return Redirect("http://localhost:52690/links/"+userId);
        }
        [Route("{userId}/Delete/{linkId}")]
        public IActionResult DeleteLink()
        {
            return View();
        }

        public LinkDetailsDto GetDetails(string url)
        {
            IGetDetails hosting = RepositoryFactory.GetHosting(url);

            var toReturn = new LinkDetailsDto();

            toReturn.Title = hosting.GetTitle();
            toReturn.Provider = hosting.GetProvider();
            toReturn.ThumbnailUrl = hosting.GetThumbnailUrl();
            toReturn.AuthorName = hosting.GetUserName();
            toReturn.Duration = hosting.GetDuration();
            toReturn.Likes = hosting.GetLikes();
            
            return toReturn;
        }
    }
}
