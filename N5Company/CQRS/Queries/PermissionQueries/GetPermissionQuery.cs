using ErrorOr;
using MediatR;
using N5Company.DTOs;

namespace N5Company.CQRS.Queries.PermissionQueries
{
    public record GetPermissionQuery : IRequest<ErrorOr<IEnumerable<PermissionDto>>>;
}
