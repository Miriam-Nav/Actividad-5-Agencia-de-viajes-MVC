using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MVCApp.Model
{
    public class ViajesRepositorio
    {
        private readonly AgenciaViajesEntities _context = new AgenciaViajesEntities();

        public List<Viajes> Seleccionar()
        {
            return _context.Viajes.ToList();
        }

        public Viajes BuscarPorId(int id)
        {
            return _context.Viajes.Find(id);
        }

        public void Crear(Viajes viaje)
        {
            _context.Viajes.Add(viaje);
            _context.SaveChanges();
        }

        public void Editar(Viajes viaje)
        {
            _context.Entry(viaje).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Eliminar(Viajes viaje)
        {
            _context.Viajes.Remove(viaje);
            _context.SaveChanges();
        }

        public bool TieneReservas(int idViaje)
        {
            return _context.Reservas.Any(r => r.IdViaje == idViaje);
        }
    }
}
