using Microsoft.EntityFrameworkCore;
using Nof.Model;
using Nof.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nof.Provider
{
    public interface IHaircutProvider
    {
        List<HaircutRecored> GetAllAppointments();
        HaircutRecored GetAppointmentByUserId(int UserId);
        bool DeleteAppointment(int userId);
        int AddOrChangeAppointment(HaircutRecored appointment);
    }
    public class HaircutProvider : IHaircutProvider
    {
        private readonly ApplicationDbContext _context;
        public HaircutProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<HaircutRecored> GetAllAppointments()
        {
            try
            {
                var haircutList = _context.HaircutRecored.Include(x => x.User).ToList();
                //
                return haircutList;

            } catch (Exception xe)
            {
                return null;
            }
            
        }
        public HaircutRecored GetAppointmentByUserId(int UserId)
        {
            try
            {
                var haircutAppoitment = _context.HaircutRecored.Where(ap=> ap.User.Id == UserId).FirstOrDefault();
                return haircutAppoitment;

            } catch (Exception xe)
            {
                return null;
            }
        }
        public bool DeleteAppointment(int userId)
        {
            try
            {
                var apToRemove = _context.HaircutRecored.Where(app => app.UserId == userId).FirstOrDefault();
                _context.HaircutRecored.Remove(apToRemove);
                _context.SaveChanges();
                return true; ;

            }
            catch (Exception xe)
            {
                return false;
            }
        }
        public int AddOrChangeAppointment(HaircutRecored appointment)
        {
            try
            {
                var theAppointment = _context.HaircutRecored.Where(app => app.UserId == appointment.User.Id).FirstOrDefault();
                if (theAppointment != null)
                {
                    theAppointment.AppointmentTime = appointment.AppointmentTime.ToLocalTime();
                    theAppointment.AppointmentTimeCreated = DateTime.Now.ToLocalTime();
                    _context.SaveChanges();

                    return 1;
                } else
                {
                    appointment.UserId = appointment.User.Id;
                    appointment.User = null;
                    appointment.AppointmentTime = appointment.AppointmentTime.ToLocalTime();
                    appointment.AppointmentTimeCreated = DateTime.Now.ToLocalTime();
                    _context.HaircutRecored.Add(appointment);
                    _context.SaveChanges();
                    return 0;
                }

            }
            catch (Exception xe)
            {
                return -1;
            }
        }
    }
}
