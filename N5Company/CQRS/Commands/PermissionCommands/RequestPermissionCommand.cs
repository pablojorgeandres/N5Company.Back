using ErrorOr;
using MediatR;
using N5Company.DTOs;

namespace N5Company.CQRS.Commands.PermissionCommands
{
    public class RequestPermissionCommand : IRequest<ErrorOr<PermissionDto>>
    {
        public string? NombreEmpleado { get; set; }
        public string? ApellidoEmpleado { get; set; }
        public int TipoPermiso { get; set; }
    }
}
