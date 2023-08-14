using AutoMapper;
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
    public class RequestPermissionCommandHandler : IRequestHandler<RequestPermissionCommand, ErrorOr<PermissionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;
        private readonly KafkaProducer _kafkaProducer;


        public RequestPermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IElasticClient elasticClient, KafkaProducer kafkaProducer)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _elasticClient = elasticClient;
            _kafkaProducer = kafkaProducer;

        }

        public async Task<ErrorOr<PermissionDto>> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = new Permission
            {
                ApellidoEmpleado = request.ApellidoEmpleado,
                FechaPermiso = DateTime.Now,
                NombreEmpleado = request.NombreEmpleado,
                TipoPermiso = request.TipoPermiso
            };
            _unitOfWork.Repository().Add(permission);
            var indexResponse = await _elasticClient.IndexAsync(permission, idx => idx.Index("permission_index"));

            await _unitOfWork.CommitAsync(cancellationToken);

            await _kafkaProducer.PublishPermissionOperationAsync("request");

            return _mapper.Map<PermissionDto>(permission);
        }
    }
}
