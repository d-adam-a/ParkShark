using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Notice;
using ParkShark.Data;
using ParkShark.Migrations;
using ParkShark.Models;
using ParkShark.Models.ViewModels;
using DetailParking = ParkShark.Models.DetailParking;
using Parking = ParkShark.Models.Parking;

namespace ParkShark.Controllers
{
    [Authorize]
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

            if (String.IsNullOrEmpty(search))
            {
                return View(parking);
            }
            else if (!String.IsNullOrEmpty(search))
            {
                parking = _context.Parking.Where(x => x.LicensePlate.ToLower().Contains(search)).ToList();
                return View(parking);
            }
            return View();
            
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
            var parking = _context.Parking.Include(x => x.TransportationType).FirstOrDefault(x => x.Id == id);

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

        public IActionResult Detail(int id, string status)
        {
            var parking = _context.Parking.Include(x => x.TransportationType).FirstOrDefault(x => x.Id == id);

            int hourlyRate = parking.TransportationType.HourlyRate;
            int parkingFee = hourlyRate * (int)(DateTime.Now - parking.TimeEntry).TotalHours;
            if(status == "Paid")
            {
                parkingFee = _context.DetailParking
                    .Where(x => x.Parking.Id == id)
                    .Select(x => x.ParkingFee)
                    .FirstOrDefault();
            }

            var detailView = new DetailParkingView()
            {
                ParkingId = parking.Id,
                TransportationTypeName = parking.TransportationType.Name,
                LicensePlate = parking.LicensePlate,
                TimeEntry = parking.TimeEntry,
                HourlyRate = hourlyRate,
                ParkingFee = parkingFee,
                Status = status
            };

            return View(detailView);
        }


        [HttpPost]
        public IActionResult Detail([FromForm] DetailParkingForm DetailParkingForm)
        {
            var detailParking = _context.DetailParking.FirstOrDefault(x => x.Parking.Id == DetailParkingForm.Parking);
            var parking = _context.Parking.FirstOrDefault(x => x.Id == DetailParkingForm.Parking);

            var DetailParking = new DetailParking()
            {
                Parking = parking,
                HourlyRate = DetailParkingForm.HourlyRate,
                ParkingFee = DetailParkingForm.ParkingFee,
                Status = DetailParkingForm.Status
            };

            if (detailParking == null)
            {
                _context.DetailParking.Add(DetailParking);
            }
            else
            {
                detailParking.HourlyRate = DetailParkingForm.HourlyRate;
                detailParking.ParkingFee = DetailParkingForm.ParkingFee;
                detailParking.Status = DetailParkingForm.Status;
                _context.DetailParking.Update(detailParking);
            }

            _context.SaveChanges();

            return RedirectToAction("Detail", new { id = DetailParkingForm.Parking, status = DetailParkingForm.Status });
        }

    }
}