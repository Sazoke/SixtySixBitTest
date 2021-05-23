using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using SixtySixBitTest.Data;
using SixtySixBitTest.Models;
using SixtySixBitTest.Repositories;

namespace SixtySixBitTest.Controllers
{
    public class FootballerController : Controller
    {
        private readonly IFootballRepository _repository;
        public FootballerController(FootballerContext context)
        {
            _repository = new FootBallRepository(context);
        }
        
        public IActionResult Index([FromRoute] int id = -1)
        {
            var newModel = new FootballModel();
            ViewBag.Commands = _repository.GetCommands();
            newModel.Footballer = id != -1 ? _repository.GetFootballer(id) : new Footballer() {Id = -1};
            newModel.Commands = _repository.GetCommands();
            return View(newModel);
        }

        [HttpPost]
        public IActionResult SetFootballer([FromForm] FootballModel footballModel)
        {
            if (string.IsNullOrEmpty(footballModel.Footballer.FirstName) || 
                !Regex.IsMatch(footballModel.Footballer.FirstName, @"^[a-zA-Z]+$"))
                ModelState.AddModelError("FirstName", "Write correct Firstname");
            if (string.IsNullOrEmpty(footballModel.Footballer.Lastname) ||
                !Regex.IsMatch(footballModel.Footballer.Lastname, @"^[a-zA-Z]+$"))
                ModelState.AddModelError("SecondName", "Write correct Lastname");
            if (footballModel.Footballer.Gender == Gender.NaN)
                ModelState.AddModelError("Gender", "Choose gender");
            if (footballModel.Footballer.Country == Country.NaN)
                ModelState.AddModelError("Country", "Choose country");
            if (string.IsNullOrEmpty(footballModel.WrittenCommand) &&
                string.IsNullOrEmpty(footballModel.SelectedCommand) ||
                !string.IsNullOrEmpty(footballModel.WrittenCommand) &&
                !string.IsNullOrEmpty(footballModel.SelectedCommand))
                ModelState.AddModelError("Command", "Choose or write command");
            else if (string.IsNullOrEmpty(footballModel.WrittenCommand))
                footballModel.Footballer.Command = footballModel.SelectedCommand;
            else
                footballModel.Footballer.Command = footballModel.WrittenCommand;
            if(footballModel.Footballer.BirthDateTime > DateTime.Now || footballModel.Footballer.BirthDateTime < DateTime.Now - new TimeSpan(365 * 24,0,0))
                ModelState.AddModelError("Birthdate", "Choose correct birthdate");
            footballModel.Commands = _repository.GetCommands();
            if (!ModelState.IsValid) return View("Index", footballModel);
            _repository.AddOrUpdateFootballer(footballModel.Footballer);
            Redirect("Footballer/Index");
            return View();
        }

        public IActionResult GetFootballers()
        {
            return View(_repository.GetFootballers());
        }
    }
}