using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TableManagement.Models
{
    [Index(nameof(ReservationCode), IsUnique = true)]
    public class Reservation
    {
        public int Id { get; set; }

        
        public string ReservationCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณากรอกชื่อผู้จอง")]
        [RegularExpression(@"^[ก-๙a-zA-Z\s]+$", ErrorMessage = "ห้ามใส่ตัวเลขในชื่อ")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "กรุณากรอกเบอร์โทรศัพท์")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "รูปแบบเบอร์โทรศัพท์ไม่ถูกต้อง")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "กรุณาเลือกวันที่จอง")]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }

        public string Status { get; set; } = "จองแล้ว";

        [ForeignKey("Table")]
        public int TableId { get; set; }
        public Table? Table { get; set; }
    }
}
