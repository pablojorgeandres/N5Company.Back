using ErrorOr;
using MediatR;
using N5Company.DTOs;

namespace N5Company.CQRS.Commands.PermissionCommands
{
    public class ModifyPermissionCommand : IRequest<ErrorOr<PermissionDto>>
    {
        public int Id { get; set; }
        public string? NombreEmpleado { get; set; }
        public string? ApellidoEmpleado { get; set; }
        public int TipoPermiso { get; set; }
    }
}
