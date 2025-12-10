using MVCApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;

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
        public void Crear(int idCliente, int idViaje, string fechaTexto)
        {
            // Comrpueba existencia del cliente
            var cliente = clienteRepo.BuscarPorId(idCliente);
            if (cliente == null)
            {
                throw new Exception("El cliente no existe");
            }

            // Comrpueba existencia del viaje
            var viaje = viajeRepo.BuscarPorId(idViaje);
            if (viaje == null)
            {
                throw new Exception("El viaje no existe");
            }

            // Comrpueba plazas disponibles
            if (viaje.PlazasDisponibles <= 0)
            {
                throw new Exception("No hay plazas disponibles para este viaje");
            }

            // Validación de formato de fecha
            DateTime fechaReserva;
            bool formatoCorrecto = DateTime.TryParseExact( fechaTexto, "dd/MM/yyyy", null, DateTimeStyles.None, out fechaReserva);

            if (!formatoCorrecto)
            {
                throw new Exception("La fecha no tiene el formato correcto (dd/MM/yyyy)");
            }

            // Crea una reserva 
            var reserva = new Reservas
            {
                IdCliente = idCliente,
                IdViaje = idViaje,
                FechaReserva = fechaReserva
            };

            // Reduce plazas
            viaje.PlazasDisponibles = viaje.PlazasDisponibles - 1;

            viajeRepo.Editar(viaje);
            repo.Crear(reserva);
        }

        // Cancelar una reserva y devolver plaza
        public void Cancelar(int idReserva)
        {
            // Comprueba que existe la reserva
            var reserva = repo.BuscarPorId(idReserva);

            if (reserva == null)
            {
                throw new Exception("La reserva no existe");
            }

            // Comprueba que existe el viaje
            var viaje = reserva.Viajes;
            if (viaje == null)
            {
                throw new Exception("El viaje asociado no existe");
            }


            // Devolver la plaza
            reserva.Viajes.PlazasDisponibles += 1;

            viajeRepo.Editar(viaje);
            repo.Eliminar(reserva.IdReserva);

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
