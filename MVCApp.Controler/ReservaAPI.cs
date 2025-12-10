using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCApp.Model;

namespace MVCApp.Controller
{
    public class ReservaAPI
    {
        private readonly ReservasRepositorio repo = new ReservasRepositorio();
        private readonly ClienteRepositorio clienteRepo = new ClienteRepositorio();
        private readonly ViajesRepositorio viajeRepo = new ViajesRepositorio();

        // Listar todas las reservas
        public List<Reservas> Listar()
        {
            return repo.Seleccionar();
        }

        // Crear una nueva reserva
        public void Crear(int idCliente, int idViaje)
        {
            var cliente = clienteRepo.BuscarPorId(idCliente);

            if (cliente == null)
            {
                throw new Exception("El cliente no existe");
            }

            var viaje = viajeRepo.BuscarPorId(idViaje);
            if (viaje == null)
            {
                throw new Exception("El viaje no existe");
            }

            if (viaje.PlazasDisponibles <= 0)
            {
                throw new Exception("No hay plazas disponibles para este viaje");
            }

            var reserva = new Reservas
            {
                IdCliente = idCliente,
                IdViaje = idViaje,
                FechaReserva = DateTime.Now
            };

            // Reducir plazas
            viaje.PlazasDisponibles = viaje.PlazasDisponibles - 1;
            viajeRepo.Editar(viaje);
            repo.Crear(reserva);
        }

        // Cancelar una reserva y devolver plaza
        public void Cancelar(int idReserva)
        {
            var reserva = repo.BuscarPorId(idReserva);

            if (reserva == null)
            {
                throw new Exception("La reserva no existe");
            }

            var viaje = reserva.Viajes;
            if (viaje == null)
            {
                throw new Exception("El viaje asociado no existe");
            }

            // Devolver la plaza
            reserva.Viajes.PlazasDisponibles += 1;
            viajeRepo.Editar(viaje);
            repo.Eliminar(reserva);

        }

        // Mostrar reservas por cliente
        public List<Reservas> ReservasPorCliente(int idCliente)
        {
            return repo.PorCliente(idCliente);
        }

        // Mostrar reservas por viaje
        public List<Reservas> ReservasPorViaje(int idViaje)
        {
            using (var db = new AgenciaViajesEntities())
            {
                return repo.PorViaje(idViaje);
            }
        }
    }
}
