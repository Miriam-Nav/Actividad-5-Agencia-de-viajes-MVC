using System.Collections.Generic;
using System.Linq;

namespace MVCApp.Model
{
    public class ViajesRepositorio
    {
        // Contexto de Entity Framework para gestionar la base de datos
        private readonly AgenciaViajesEntities _context = new AgenciaViajesEntities();

        // Seleccionar todos los viajes
        public List<Viajes> Seleccionar()
        {
            return _context.Viajes.ToList();
        }

        // Buscar viaje por ID
        public Viajes BuscarPorId(int id)
        {
            return _context.Viajes.Find(id);
        }

        // Crear viaje
        public void Crear(Viajes viaje)
        {
            // Agrega y guarda el viaje
            _context.Viajes.Add(viaje);
            _context.SaveChanges();
        }

        // Editar viaje
        public void Editar(Viajes viaje)
        {
            // Busca viaje por ID
            var viajeExistente = _context.Viajes.Find(viaje.IdViaje);
            if (viajeExistente != null)
            {
                // Actualiza los datos del viaje
                viajeExistente.Destino = viaje.Destino;
                viajeExistente.Precio = viaje.Precio;
                viajeExistente.PlazasDisponibles = viaje.PlazasDisponibles;

                _context.SaveChanges();
            }
        }

        // Eliminar viaje
        public void Eliminar(int idViaje)
        {
            // Busca el viaje por id
            var viaje = _context.Viajes.Find(idViaje);

            // Borra el viaje encontrado
            if (viaje != null)
            {
                _context.Viajes.Remove(viaje);
                _context.SaveChanges();
            }
        }

        // Comprobar si un viaje tiene reservas asociadas
        public bool TieneReservas(int idViaje)
        {
            return _context.Reservas.Any(reserva => reserva.IdViaje == idViaje);
        }
    }
}
