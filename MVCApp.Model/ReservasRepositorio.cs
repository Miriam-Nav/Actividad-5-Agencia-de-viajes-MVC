using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVCApp.Model
{
    public class ReservasRepositorio
    {
        private readonly AgenciaViajesEntities _context = new AgenciaViajesEntities();

        // Lista todas las reservas con sus clientes y viajes asociados
        public List<Reservas> Seleccionar()
        {
            return _context.Reservas.Include(reserva => reserva.Clientes).Include(r => r.Viajes).ToList();
        }

        // Buscar reserva por ID incluyendo el viaje
        public Reservas BuscarPorId(int id)
        {
            return _context.Reservas.Include(reserva => reserva.Viajes).FirstOrDefault(r => r.IdReserva == id);
        }

        // Crear reserva
        public void Crear(Reservas reserva)
        {
            // Agrega y guarda la reserva
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
        }

        // Eliminar reserva
        public void Eliminar(int idReserva)
        {
            // Busca la reserva por id
            var reserva = _context.Reservas.Find(idReserva);

            // Borra la reserva encontrada
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
                _context.SaveChanges();
            }
        }

        // Editar reserva
        public void Editar(Reservas reserva)
        {
            // Busca la reserva por ID
            var reservaExistente = _context.Reservas.Find(reserva.IdReserva);
            if (reservaExistente != null)
            {
                // Actualiza los datos de la reserva
                reservaExistente.IdCliente = reserva.IdCliente;
                reservaExistente.IdViaje = reserva.IdViaje;
                reservaExistente.FechaReserva = reserva.FechaReserva;

                _context.SaveChanges();
            }
        }

        // Seleccionar reservas por cliente incluyendo viajes
        public List<Reservas> PorCliente(int idCliente)
        {
            return _context.Reservas.Where(r => r.IdCliente == idCliente).Include(r => r.Viajes).ToList();
        }

        // Seleccionar reservas por viaje incluyendo clientes
        public List<Reservas> PorViaje(int idViaje)
        {
            return _context.Reservas.Where(r => r.IdViaje == idViaje).Include(r => r.Clientes).ToList();
        }
    }
}
