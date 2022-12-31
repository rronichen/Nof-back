using Nof.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nof.Models
{
    public class HaircutRecored
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public DateTime AppointmentTimeCreated { get; set; }

        public virtual User User  { get; set; }
}
}
