using N5Company.Entities;

namespace N5Company.DTOs
{
    public class PermissionDto
    {
        public int Id { get; init; }
        public string NombreEmpleado { get; init; }
        public string ApellidoEmpleado { get; init; }
        public int TipoPermiso { get; init; }
        public DateTime FechaPermiso { get; init; }
        public PermissionType PermissionType { get; init; }
    }
}
