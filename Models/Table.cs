using System.ComponentModel.DataAnnotations;

namespace TableManagement.Models
{
    public class Table
    {
        public int Id { get; set; }

        public required string TableCode { get; set; }

        public string Status { get; set; } = "ว่าง"; // Default status is "ว่าง" (available)

        public string Zone { get; set; } = string.Empty; // โซนของโต๊ะ เช่น A, B, C

        public bool IsOccupied { get; set;}
        
    }
}