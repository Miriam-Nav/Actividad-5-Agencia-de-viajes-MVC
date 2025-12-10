using System.Collections.Generic;
using System.Linq;

namespace MVCApp.Model
{
    public class ClienteRepositorio
    {
        private readonly AgenciaViajesEntities _context = new AgenciaViajesEntities();

        // Seleccionar todos los clientes
        public List<Clientes> Listar()
        {
            return _context.Clientes.ToList();
        }

        // Buacar
        public Clientes BuscarPorId(int id)
        {
            return _context.Clientes.Find(id);
        }

        // Buscar Email
        public Clientes BuscarPorEmail(string email)
        {
            return _context.Clientes.FirstOrDefault(c => c.Email == email);
        }

        // Crear cliente
        public void Crear(Clientes cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        // Editar cliente
        public void Editar(Clientes cliente)
        {
            _context.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        // Eliminar cliente
        public void Eliminar(int idCliente)
        {
            var cliente = _context.Clientes.Find(idCliente);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }
    }
}
