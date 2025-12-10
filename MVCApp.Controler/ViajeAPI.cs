using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCApp.Model;

namespace MVCApp.Controller
{
    public class ViajeAPI
    {
        private readonly ViajesRepositorio repo = new ViajesRepositorio();

        // Listar todos los viajes
        public List<Viajes> Listar()
        {
            return repo.Seleccionar();
        }

        // Crear un nuevo viaje
        public void Crear(Viajes viaje)
        {
            // Comrpueba que hay destino
            if (viaje.Destino == null || viaje.Destino == "")
            {
                throw new Exception("El destino no puede estar vacío");
            }

            // Comrpueba que el precio es valido
            if (viaje.Precio <= 0)
            {
                throw new Exception("El precio debe ser mayor que 0");
            }

            // Comrpueba que hay plazas libres
            if (viaje.PlazasDisponibles < 0)
            {
                throw new Exception("Las plazas disponibles no pueden ser negativas");
            }

            // Crea el viaje
            repo.Crear(viaje);
        }

        // Modificar un viaje existente
        public void Editar(Viajes viaje)
        {
            // Comprueba que el viaje existe
            var viajeExistente = repo.BuscarPorId(viaje.IdViaje);
            if (viajeExistente == null)
            {
                throw new Exception("El viaje no existe");
            }

            // Comprueba que el precio sea válido
            if (viaje.Precio <= 0)
            {
                throw new Exception("El precio debe ser mayor que 0");
            }

            // Comprueba las plazas disponibles
            if (viaje.PlazasDisponibles < 0)
            {
                throw new Exception("Las plazas disponibles no pueden ser negativas");
            }

            
            // Actualiza campos
            viajeExistente.Destino = viaje.Destino;
            viajeExistente.Precio = viaje.Precio;
            viajeExistente.PlazasDisponibles = viaje.PlazasDisponibles;

            // Edita el viaje
            repo.Editar(viajeExistente);
        }

        // Eliminar un viaje por Id
        public void Eliminar(int id)
        {
            // Comrpueba que el viaje existe
            var viaje = repo.BuscarPorId(id);
            if (viaje == null)
            {
                throw new Exception("El viaje no existe");
            }

            // Valida que no tenga reservas
            if (repo.TieneReservas(id))
            {
                throw new Exception("No se puede eliminar un viaje con reservas asociadas");
            }

            // Elimina el viaje
            repo.Eliminar(viaje.IdViaje);

        }
    }
}
