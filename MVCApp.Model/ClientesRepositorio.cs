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

        // Buscar
        public Clientes BuscarPorId(int id)
        {
            return _context.Clientes.Find(id);
        }

        // Buscar Email
        public Clientes BuscarPorEmail(string email)
        {
            // Comprueba que existe el email
            return _context.Clientes.FirstOrDefault(c => c.Email == email);
        }

        // Crear cliente
        public void Crear(Clientes cliente)
        {
            // Crea y guarda el cliente
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        // Editar cliente
        public void Editar(Clientes cliente)
        {
            // Busca cliente por ID
            var clienteExistente = _context.Clientes.Find(cliente.IdCliente);
            if (clienteExistente != null)
            {
                // Actualiza los datos del cliente
                clienteExistente.Nombre = cliente.Nombre;
                clienteExistente.Apellidos = cliente.Apellidos;
                clienteExistente.Email = cliente.Email;

                _context.SaveChanges(); 
            }
        }

        // Eliminar cliente
        public void Eliminar(int idCliente)
        {
            // Busca el cliente por id
            var cliente = _context.Clientes.Find(idCliente);

            // Borra el cliente encontrado
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }
    }
}
