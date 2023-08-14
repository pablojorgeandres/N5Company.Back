using ErrorOr;
using MediatR;
using N5Company.DTOs;

namespace N5Company.CQRS.Queries.PermissionTypeQueries
{
    public record GetPermissionTypeQuery : IRequest<ErrorOr<IEnumerable<PermissionTypeDto>>>;
}
