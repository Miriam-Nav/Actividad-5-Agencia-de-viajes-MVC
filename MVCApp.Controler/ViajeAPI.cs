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
            if (viaje.Destino == null || viaje.Destino == "")
            {
                throw new Exception("El destino no puede estar vacío");
            }

            if (viaje.Precio <= 0)
            {
                throw new Exception("El precio debe ser mayor que 0");
            }
            if (viaje.PlazasDisponibles < 0)
            {
                throw new Exception("Las plazas disponibles no pueden ser negativas");
            }

            repo.Crear(viaje);
        }

        // Modificar un viaje existente
        public void Editar(Viajes viaje)
        {
            if (viaje.Precio <= 0)
            {
                throw new Exception("El precio debe ser mayor que 0");
            }
            if (viaje.PlazasDisponibles < 0)
            {
                throw new Exception("Las plazas disponibles no pueden ser negativas");
            }

            var viajeExistente = repo.BuscarPorId(viaje.IdViaje);
            if (viajeExistente == null)
            {
                throw new Exception("El viaje no existe");
            }
            // Actualizar campos
            viajeExistente.Destino = viaje.Destino;
            viajeExistente.Precio = viaje.Precio;
            viajeExistente.PlazasDisponibles = viaje.PlazasDisponibles;

            repo.Editar(viajeExistente);
        }

        // Eliminar un viaje por Id
        public void Eliminar(int id)
        {

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

            repo.Eliminar(viaje);

        }
    }
}
