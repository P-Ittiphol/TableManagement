using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableManagement.Data;
using TableManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using TableManagement.ViewModels;

namespace TableManagement.Controllers
{
    public class TableController : Controller
    {
        private readonly AppDbContext _context;

        public TableController(AppDbContext context)
        {
            _context = context;
        }


        // GET: Table
        public async Task<IActionResult> Index(string? zone, int? tableId)
        {
            var query = _context.Tables.AsQueryable();

            if (!string.IsNullOrEmpty(zone))
            {
                query = query.Where(t => t.Zone == zone);
            }

            var tables = await query
                .OrderBy(t => t.TableCode)
                .ToListAsync();

            Table? selectedTable = null;
            Reservation? reservation = null;

            if (tableId != null) 
            {
                selectedTable = await _context.Tables
                    .FirstOrDefaultAsync(t => t.Id == tableId);

                reservation = await _context.Reservations
                    .Where(r => r.TableId == tableId)
                    .OrderByDescending(r => r.ReservationDate)
                    .FirstOrDefaultAsync();
            }

            var viewModel = new TableManageViewModel
            {
                Tables = tables,
                Table = selectedTable,
                Reservation = reservation
            };

            return View(viewModel);
        }

        // GET: โหลดฟอร์มเข้า right panel
        public IActionResult Create()
        {
            return PartialView("_CreatePartial");
        }

        // POST: บันทึก
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Table table)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreatePartial", table);

            table.Status = "ว่าง";
            _context.Tables.Add(table);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        // GET: Table/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var table = await _context.Tables
                .FirstOrDefaultAsync(t => t.Id == id);

            if (table == null) return NotFound();

            var reservation = await _context.Reservations
                .Where(r => r.TableId == id)
                .OrderByDescending(r => r.ReservationDate)
                .FirstOrDefaultAsync();

            var viewModel = new TableManageViewModel
            {
                Table = table,
                Reservation = reservation
            };


            // 🔹 ดึง TableCode ทั้งหมด
            var tableCodes = await _context.Tables
                .Select(t => t.TableCode)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();

            ViewBag.TableCodeList = new SelectList(
                tableCodes,
                table.TableCode // ให้ select ค่าเดิมอัตโนมัติ
            );

            // 🔹 ดึง Zone ทั้งหมด
            ViewBag.Zones = await _context.Tables
                .Select(t => t.Zone)
                .Distinct()
                .ToListAsync();

            // 🔥 ดึง Status จากฐานข้อมูลจริง
            var statusList = await _context.Tables
                .Select(t => t.Status)
                .Where(s => s != null)
                .Distinct()
                .ToListAsync();

            ViewBag.StatusList = new SelectList(statusList, table.Status);

            return PartialView("_EditPartial", table);
        }

        // POST: Table/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Table table)
        {
            if (id != table.Id) return NotFound();

            if (!ModelState.IsValid)
                return PartialView("_EditPartial", table);

            var existingTable = await _context.Tables
                .FirstOrDefaultAsync(t => t.Id == id);

            if (existingTable == null) return NotFound();

            if (table.Status == "ว่าง")
            {
                var reservations = await _context.Reservations
                    .Where(r => r.TableId == id)
                    .ToListAsync();

                if (reservations.Any())
                {
                    _context.Reservations.RemoveRange(reservations);
                }
            }

            existingTable.TableCode = table.TableCode;
            existingTable.Zone = table.Zone;
            existingTable.Status = table.Status;

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // GET: Table/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var table = await _context.Tables.FindAsync(id);
            if (table == null) return NotFound();

            return PartialView("_DeletePartial", table);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) return NotFound();

            // 🔴 ตรวจสอบสถานะก่อนลบ
            if (table.Status != "ว่าง")
            {
                return Json(new
                {
                    success = false,
                    message = "ไม่สามารถลบโต๊ะได้ เนื่องจากมีการจองแล้ว"
                });
            }

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
