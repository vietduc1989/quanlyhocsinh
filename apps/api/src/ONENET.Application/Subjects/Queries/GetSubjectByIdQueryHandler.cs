// QUAN-20260531-154643
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Exceptions;
using ONENET.Domain.Interfaces;
using ONENET.Application.Subjects.DTOs;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ONENET.Application.Subjects.Queries
{
    public class GetSubjectByIdQueryHandler : IRequestHandler<GetSubjectByIdQuery, SubjectDetailDto?>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSubjectByIdQueryHandler> _logger;

        public GetSubjectByIdQueryHandler(ISubjectRepository subjectRepository, IMapper mapper, ILogger<GetSubjectByIdQueryHandler> logger)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SubjectDetailDto?> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching subject with Id: {Id}", request.Id);

            // Use AsNoTracking for read-only queries to improve performance
            var subject = await _subjectRepository.GetQueryable()
                                                  .AsNoTracking()
                                                  .ProjectTo<SubjectDetailDto>(_mapper.ConfigurationProvider)
                                                  .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (subject == null)
            {
                _logger.LogWarning("Subject with Id '{Id}' not found.", request.Id);
                throw new NotFoundException($"Môn học với ID '{request.Id}' không tìm thấy.");
            }

            _logger.LogInformation("Subject with Id: {Id} fetched successfully.", request.Id);
            return subject;
        }
    }
}