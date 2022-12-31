using Nof.Models;
using Nof.Provider;
using System.Collections.Generic;

namespace Nof.Services
{
    public interface IHaircutService
    {
        List<HaircutRecored> GetAllAppointments();
        HaircutRecored GetAppointmentByUserId(int UserId);
        bool DeleteAppointment(int userId);
        int AddOrChangeAppointment(HaircutRecored appointment);
    }
    public class HaircutService: IHaircutService
    {
        private readonly IHaircutProvider _haircutProvider;

        public HaircutService(IHaircutProvider haircutProvider)
        {
            _haircutProvider = haircutProvider;
        }

        public List<HaircutRecored> GetAllAppointments()
        {
            return _haircutProvider.GetAllAppointments();
        }

        public HaircutRecored GetAppointmentByUserId(int UserId)
        {
            return _haircutProvider.GetAppointmentByUserId(UserId);
        }

        public bool DeleteAppointment(int userId)
        {
            return _haircutProvider.DeleteAppointment(userId);
        }

        public int AddOrChangeAppointment(HaircutRecored appointment)
        {
            return _haircutProvider.AddOrChangeAppointment(appointment);
        }


    }
}
