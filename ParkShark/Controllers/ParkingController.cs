using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkShark.Data;
using ParkShark.Models;
using ParkShark.Models.ViewModels;

namespace ParkShark.Controllers
{
    public class ParkingController : Controller
    {
        private readonly MysqlContext _context;

        public ParkingController(MysqlContext c)
        {
            _context = c;
        }

        public IActionResult Index(string search)
        {
            var parking = _context.Parking.Include(x => x.TransportationType).ToList();

            if (!String.IsNullOrEmpty(search))
            {
                parking = _context.Parking.Where(x => x.LicensePlate.ToLower().Contains(search)).ToList();
            }

            return View(parking);
        }

        public IActionResult Create()
        {
            ViewBag.TransportationTypes = _context.TransportationTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] ParkingForm ParkingForm)
        {
            var transportationType = _context.TransportationTypes.FirstOrDefault(x => x.Id == ParkingForm.TransportationType);

            var Parking = new Parking()
            {
                TransportationType = transportationType,
                LicensePlate = ParkingForm.LicensePlate,
                TimeEntry = ParkingForm.TimeEntry
            };

            _context.Parking.Add(Parking);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.TransportationTypes = _context.TransportationTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            var parking = _context.Parking.FirstOrDefault(x => x.Id == id);

            return View(parking);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Parking parking)
        {
            _context.Parking.Update(parking);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var parking = _context.Parking.FirstOrDefault(x => x.Id == id);

            _context.Parking.Remove(parking);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
