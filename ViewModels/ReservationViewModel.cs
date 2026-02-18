using TableManagement.Models;
using System.Collections.Generic;

namespace TableManagement.ViewModels
{

    public class ReservationViewModel
    {
        public List<Table> Tables { get; set; } = new();

        public Reservation Reservation { get; set; } = new();
    }

}
