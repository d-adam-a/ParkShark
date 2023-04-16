using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkShark.Data;
using ParkShark.Models;
using ParkShark.Models.ViewModels;

namespace ParkShark.Controllers
{
    [Authorize]
    public class TransportationTypeController : Controller
    {
        private readonly MysqlContext _context;

        public TransportationTypeController(MysqlContext c)
        {
            _context = c;
        }

        public IActionResult Index()
        {
            var transportationType = _context.TransportationTypes.ToList();

            return View(transportationType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] TransportationType TransportationType)
        {
            var transportationType = new TransportationType()
            {
                Name = TransportationType.Name,
                HourlyRate = TransportationType.HourlyRate
            };

            _context.TransportationTypes.Add(transportationType);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var transportationType = _context.TransportationTypes.FirstOrDefault(x => x.Id == id);

            return View(transportationType);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] TransportationType TransportationType)
        {
            _context.TransportationTypes.Update(TransportationType);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var transportationType = _context.TransportationTypes.FirstOrDefault(x => x.Id == id);

            _context.TransportationTypes.Remove(transportationType);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
