using System.ComponentModel.DataAnnotations;

namespace N5Company.Entities
{
    public record PermissionType : IEntity
    {

        [StringLength(100, MinimumLength = 3)]
        public string? Descripcion { get; init; }

        //Relation tables
        public virtual ICollection<Permission>? Permission { get; init; }
    }
}
