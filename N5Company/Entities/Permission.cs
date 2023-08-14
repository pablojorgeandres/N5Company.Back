using System.ComponentModel.DataAnnotations;

namespace N5Company.Entities
{
    public record Permission : IEntity
    {

        [StringLength(100, MinimumLength = 3)]
        public string? NombreEmpleado { get; init; }

        [StringLength(100, MinimumLength = 3)]
        public string? ApellidoEmpleado { get; init; }
        public int TipoPermiso { get; init; }
        public DateTime FechaPermiso { get; init; }

        public virtual PermissionType? PermissionTypes { get; init; }
    }
}
