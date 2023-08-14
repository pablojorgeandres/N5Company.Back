using AutoMapper;
using Elasticsearch.Net;
using ErrorOr;
using MediatR;
using N5Company.CQRS.Commands.PermissionCommands;
using N5Company.DTOs;
using N5Company.Entities;
using N5Company.Kafka;
using N5Company.Repositories;
using Nest;

namespace N5Company.CQRS.CommandHandlers.PermissionCommandHandlers
{
    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, ErrorOr<PermissionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elastic;
        private readonly KafkaProducer _kafkaProducer;


        public ModifyPermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IElasticClient elastic, KafkaProducer kafkaProducer)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _elastic = elastic;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<ErrorOr<PermissionDto>> Handle(ModifyPermissionCommand request, CancellationToken cancellation)
        {
            var permission = await _unitOfWork.Repository().GetById<Permission>(request.Id);

            if (permission is null)
                return default;
            permission = new Permission
            {
                Id = permission.Id,
                ApellidoEmpleado = request.ApellidoEmpleado,
                FechaPermiso = DateTime.Now,
                NombreEmpleado = request.NombreEmpleado,
                TipoPermiso = request.TipoPermiso,
            };

            _unitOfWork.Repository().Update<Permission>(permission);

            var updateResponse = await _elastic.UpdateAsync<Permission>(permission.Id, u => u
            .Index("permission_index")
            .Doc(permission)
            .Refresh(Refresh.True));

            await _unitOfWork.CommitAsync(cancellation);
            await _kafkaProducer.PublishPermissionOperationAsync("modify");

            return _mapper.Map<PermissionDto>(permission);
        }
    }
}
