using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVCApp.Model
{
    public class ReservasRepositorio
    {
        private readonly AgenciaViajesEntities _context = new AgenciaViajesEntities();

        public List<Reservas> Seleccionar()
        {
            return _context.Reservas.Include(r => r.Clientes).Include(r => r.Viajes).ToList();
        }

        public Reservas BuscarPorId(int id)
        {
            return _context.Reservas.Include(r => r.Viajes).FirstOrDefault(r => r.IdReserva == id);
        }

        public void Crear(Reservas reserva)
        {
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
        }

        public void Eliminar(Reservas reserva)
        {
            _context.Reservas.Remove(reserva);
            _context.SaveChanges();
        }

        public void Editar(Reservas reserva)
        {
            _context.Entry(reserva).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Reservas> PorCliente(int idCliente)
        {
            return _context.Reservas.Where(r => r.IdCliente == idCliente).Include(r => r.Viajes).ToList();
        }

        public List<Reservas> PorViaje(int idViaje)
        {
            return _context.Reservas.Where(r => r.IdViaje == idViaje).Include(r => r.Clientes).ToList();
        }
    }
}
