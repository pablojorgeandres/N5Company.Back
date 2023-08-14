using AutoMapper;
using ErrorOr;
using MediatR;
using N5Company.CQRS.Queries.PermissionQueries;
using N5Company.DTOs;
using N5Company.Entities;
using N5Company.Kafka;
using N5Company.Repositories;

namespace N5Company.CQRS.QueryHandlers.PermissionQueryHandlers
{
    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, ErrorOr<IEnumerable<PermissionDto>>>
    {

        private readonly KafkaProducer _kafkaProducer;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, KafkaProducer kafkaProducer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<ErrorOr<IEnumerable<PermissionDto>>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _unitOfWork.Repository().FindAllAsync<Permission>();

            await _kafkaProducer.PublishPermissionOperationAsync("get");

            return _mapper.Map<List<PermissionDto>>(permissions);
        }
    }
}
