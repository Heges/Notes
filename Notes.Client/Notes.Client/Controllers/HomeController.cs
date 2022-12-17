using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Notes.Client.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetNotes()
        {
            var requestClient = _httpClientFactory.CreateClient();

            var token = await GetToken();

            requestClient.SetBearerToken(token.AccessToken);

            var responce = await requestClient.GetAsync("http://localhost:1321/api/note");

            if (!responce.IsSuccessStatusCode)
            {
                ViewBag.Message = responce.StatusCode.ToString();
                return View();
            }

            var message = await responce.Content.ReadAsStringAsync();
            var noteList = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<Note>>>(message);
            ViewBag.Message = message;
            return View(noteList["notes"]);
        }
        
        public async Task<IActionResult> GetNote(Guid id)
        {
            var requestClient = _httpClientFactory.CreateClient();

            var token = await GetToken();

            requestClient.SetBearerToken(token.AccessToken);

            var responce = await requestClient.GetAsync($"http://localhost:1321/api/note/{id}");

            if (!responce.IsSuccessStatusCode)
            {
                ViewBag.Message = responce.StatusCode.ToString();
                return View();
            }

            var message = await responce.Content.ReadAsStringAsync();
            var noteList = JsonConvert.DeserializeObject<Note>(message);
            ViewBag.Message = message;
            return View(noteList);
        }

        [HttpGet]
        public IActionResult CreateNote()
        {
            return View(new Note());
        }

        [HttpGet]
        public IActionResult EditNote()
        {
            return View(new Note());
        }

        [HttpPost]
        public async Task<IActionResult> EditNote(Note note)
        {
            var requestClient = _httpClientFactory.CreateClient();

            var token = await GetToken();

            requestClient.SetBearerToken(token.AccessToken);

            var content = JsonConvert.SerializeObject(note);
            var responce = await requestClient.PutAsync($"http://localhost:1321/api/note", 
                new StringContent(content, Encoding.UTF8, "application/json"));

            if (!responce.IsSuccessStatusCode)
            {
                ViewBag.Message = responce.StatusCode.ToString();
                return View();
            }
            ViewBag.Message = await responce.Content.ReadAsStringAsync();
            return Redirect($"~/Home/GetNote/{note.Id}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(Note note)
        {
            var requestClient = _httpClientFactory.CreateClient();

            var token = await GetToken();

            requestClient.SetBearerToken(token.AccessToken);

            var json = JsonConvert.SerializeObject(note);
            var responce = await requestClient.PostAsync($"http://localhost:1321/api/note", 
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!responce.IsSuccessStatusCode)
            {
                ViewBag.Message = responce.StatusCode.ToString();
                return View();
            }
            var id = await responce.Content.ReadAsStringAsync();
            return Redirect("~/Home/GetNotes");
        }

        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var requestClient = _httpClientFactory.CreateClient();

            var token = await GetToken();

            requestClient.SetBearerToken(token.AccessToken);

            var responce = await requestClient.DeleteAsync($"http://localhost:1321/api/note/{id}");

            if (!responce.IsSuccessStatusCode)
            {
                ViewBag.Message = responce.StatusCode.ToString();
                return View();
            }

            ViewBag.Message = await responce.Content.ReadAsStringAsync();
            return Redirect("~/Home/GetNotes");
        }

        private async Task<TokenResponse> GetToken()
        {
            var authClient = _httpClientFactory.CreateClient();

            var discoverDocument = await authClient.GetDiscoveryDocumentAsync("https://localhost:5001/");

            return await authClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoverDocument.TokenEndpoint,
                    ClientId = "client_id",
                    ClientSecret = "client_secret",
                    Scope = "NotesWebAPI",
                });
        }
    }
}
