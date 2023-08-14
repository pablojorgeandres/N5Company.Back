using N5Company.Entities;

namespace N5Company.DTOs
{
    public class PermissionTypeDto
    {
        public int Id { get; set; }
        public string Descripcion { get; init; }
        public IList<Permission>? Permissions { get; init; }
    }
}
