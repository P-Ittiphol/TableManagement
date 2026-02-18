using TableManagement.Models;

namespace TableManagement.ViewModels
{
    public class TableManageViewModel
    {
        public List<Table>? Tables { get; set; } = new();
        public Table? Table { get; set; }
        public Reservation? Reservation { get; set; }
    }
}
