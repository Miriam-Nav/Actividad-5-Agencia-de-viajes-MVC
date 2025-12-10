using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCApp.Model;

namespace MVCApp.Controller
{
    public class ClienteAPI
    {
        //private AgenciaViajesEntities db = new AgenciaViajesEntities();
        private readonly ClienteRepositorio repo = new ClienteRepositorio();
        public List<Clientes> Listar()
        {
            return repo.Listar();
        }

        public void Crear(Clientes cliente)
        {
            if (cliente.Email == null || cliente.Email == "" || !cliente.Email.Contains("@"))
            {
                throw new Exception("Email inválido");
            }

            if (repo.BuscarPorEmail(cliente.Email) != null)
            {
                throw new Exception("Ya existe un cliente con ese email");
            }

            repo.Crear(cliente);
        }

        public void Editar(Clientes cliente)
        {
            var clienteOriginal = repo.BuscarPorId(cliente.IdCliente);
            // Buscar el cliente original en la BD
            if (clienteOriginal == null)
            {
                throw new Exception("El cliente no existe");
            }

            // Si el email cambia, validar que no exista en otro cliente
            if (clienteOriginal.Email != cliente.Email &&
                repo.BuscarPorEmail(cliente.Email) != null)
            {
                throw new Exception("Ya existe otro cliente con ese email");
            }

            // Si pasa las validaciones, actualizar
            repo.Editar(cliente);
        }


        public void Eliminar(int id)
        {
            var cliente = repo.BuscarPorId(id);

            if (cliente == null)
            {
                throw new Exception("El cliente no existe");
            }

            repo.Eliminar(cliente.IdCliente);
        }

    }
}
