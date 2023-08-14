using AutoMapper;
using ErrorOr;
using MediatR;
using N5Company.CQRS.Queries.PermissionTypeQueries;
using N5Company.DTOs;
using N5Company.Entities;
using N5Company.Kafka;
using N5Company.Repositories;

namespace N5Company.CQRS.QueryHandlers.PermissionTypeQueryHandlers
{
    public class GetPermissionTypeQueryHandler : IRequestHandler<GetPermissionTypeQuery, ErrorOr<IEnumerable<PermissionTypeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly KafkaProducer _kafkaProducer;

        public GetPermissionTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, KafkaProducer kafkaProducer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<ErrorOr<IEnumerable<PermissionTypeDto>>> Handle(GetPermissionTypeQuery request, CancellationToken cancellationToken)
        {
            var permissionTypes = await _unitOfWork.Repository().FindAllAsync<PermissionType>();

            await _kafkaProducer.PublishPermissionOperationAsync("get");

            return _mapper.Map<List<PermissionTypeDto>>(permissionTypes);
        }
    }
}
