using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCApp.Model;

namespace MVCApp.Controller
{
    public class ClienteAPI
    {
        private readonly ClienteRepositorio repo = new ClienteRepositorio();
        private readonly ReservasRepositorio reservasRepo = new ReservasRepositorio();

        // Listar todas los clientes
        public List<Clientes> Listar()
        {
            return repo.Listar();
        }

        // Crear un nuevo cliente
        public void Crear(Clientes cliente)
        {
            // Comprueba que haya email y sea válido
            if (cliente.Email == null || cliente.Email == "" || !cliente.Email.Contains("@"))
            {
                throw new Exception("Email inválido");
            }

            // Comprueba que no existe ese email en otro cliente
            if (repo.BuscarPorEmail(cliente.Email) != null)
            {
                throw new Exception("Ya existe un cliente con ese email");
            }

            // Crea el cliente
            repo.Crear(cliente);
        }

        public void Editar(Clientes cliente)
        {
            // Buscar el cliente 
            var clienteOriginal = repo.BuscarPorId(cliente.IdCliente);
            // Comprueba que existe 
            if (clienteOriginal == null)
            {
                throw new Exception("El cliente no existe");
            }

            // Si el email cambia valida que no exista en otro cliente
            if (clienteOriginal.Email != cliente.Email &&
                repo.BuscarPorEmail(cliente.Email) != null)
            {
                throw new Exception("Ya existe otro cliente con ese email");
            }

            // Actualiza cliente
            repo.Editar(cliente);
        }

        // Eliminar cliente
        public void Eliminar(int id)
        {
            // Buscar el cliente por ID
            var cliente = repo.BuscarPorId(id);

            // Validar existencia antes de eliminar
            if (cliente == null)
            {
                throw new Exception("El cliente no existe");
            }

            // Validar si el cliente tiene reservas activas
            if (reservasRepo.PorCliente(id).Any())
            {
                throw new Exception("No puedes eliminar un cliente que tenga una reserva activa");
            }

            // Eliminar cliente
            repo.Eliminar(cliente.IdCliente);
        }

    }
}
