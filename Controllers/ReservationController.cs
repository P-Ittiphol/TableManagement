using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TableManagement.Data;
using TableManagement.Models;
using TableManagement.ViewModels;

namespace TableManagement.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ReservationViewModel{
                Tables = await _context.Tables
                    .OrderBy(t => t.Zone)
                    .ThenBy(t => t.TableCode)
                    .ToListAsync(),

                Reservation = new Reservation()
            };
                
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel model)
        {
            var today = DateTime.UtcNow.Date;

             // 1️⃣ ตรวจสอบข้อมูลที่ผู้ใช้กรอกเข้ามา

            if (!ModelState.IsValid)
            {
                model.Tables = await _context.Tables.ToListAsync();
                return View("Index", model);
            }

            model.Reservation.ReservationDate = today;

             // 2️⃣ ตรวจว่าโต๊ะนี้ถูกจองในวันที่เลือกแล้วหรือยัง

            var isBooked = await _context.Reservations
            .AnyAsync(r => r.TableId == model.Reservation.TableId
                && r.ReservationDate.Date == today);

            if (isBooked)
            {
                ModelState.AddModelError("", "โต๊ะนี้ถูกจองในวันที่เลือกแล้ว");
                
                model.Tables = await _context.Tables.ToListAsync();
                return View("Index", model);
            }

            var table = await _context.Tables
                .FirstOrDefaultAsync(t => t.Id == model.Reservation.TableId);

            if (table == null || table.Status != "ว่าง")
            {
                return RedirectToAction(nameof(Index));
            }

            string zone = table.Zone;

            var countToday = await _context.Reservations
                .Join(_context.Tables,
                    r => r.TableId,
                    t => t.Id,
                    (r, t) => new { r, t})
                .Where(x => x.r.ReservationDate.Date == today && x.t.Zone == zone)
                .CountAsync();
            
            int runningNumber = countToday + 1;

            string dayMonth = DateTime.Now.ToString("ddMM");

            string reservationCode = 
                $"{dayMonth}{zone}{runningNumber.ToString("D3")}";

            model.Reservation.ReservationCode = reservationCode;

            // 4️⃣ บันทึกการจอง
            _context.Reservations.Add(model.Reservation);

            // 🔥 เปลี่ยนสถานะโต๊ะ
            table.Status = "มีคนจองแล้ว";

            await _context.SaveChangesAsync();

            TempData["Success"] = "จองโต๊ะสำเร็จแล้ว";

            return RedirectToAction(nameof(Index));
        }
    }
}
