// QUAN-20260531-154643
using MediatR;
using Microsoft.Extensions.Logging;
using ONENET.Application.Common.Interfaces;
using ONENET.Application.Common.Models;
using ONENET.Application.Subjects.DTOs;
using ONENET.Domain.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core; // For dynamic sorting

namespace ONENET.Application.Subjects.Queries
{
    public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, PaginatedList<SubjectDto>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<GetSubjectsQueryHandler> _logger;

        public GetSubjectsQueryHandler(ISubjectRepository subjectRepository, IMapper mapper, ICurrentUser currentUser, ILogger<GetSubjectsQueryHandler> logger)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<PaginatedList<SubjectDto>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching subjects list with PageIndex: {PageIndex}, PageSize: {PageSize}, SearchQuery: {SearchQuery}, IncludeInactive: {IncludeInactive}",
                request.PageIndex, request.PageSize, request.SearchQuery, request.IncludeInactive);

            var query = _subjectRepository.GetQueryable().AsNoTracking();

            // Apply filtering for isActive based on user roles and includeInactive flag
            if (!request.IncludeInactive || (!_currentUser.IsInRole("Admin") && !_currentUser.IsInRole("ChuyenVienDaoTao")))
            {
                query = query.Where(s => s.IsActive);
            }

            // Apply search query
            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                var search = request.SearchQuery.Trim().ToLower();
                query = query.Where(s => s.Code.ToLower().Contains(search) || s.Name.ToLower().Contains(search));
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                var order = request.SortOrder.ToLower() == "desc" ? "descending" : "ascending";
                try
                {
                    query = query.OrderBy($"{request.SortBy} {order}");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Invalid SortBy or SortOrder parameter. Defaulting to 'name asc'. SortBy: {SortBy}, SortOrder: {SortOrder}", request.SortBy, request.SortOrder);
                    query = query.OrderBy("Name ascending");
                }
            }
            else
            {
                query = query.OrderBy("Name ascending"); // Default sort if not specified
            }

            // Project to DTO and paginate
            var paginatedList = await query.ToPaginatedListAsync(_mapper, request.PageIndex, request.PageSize, cancellationToken);

            _logger.LogInformation("Fetched {Count} subjects (Page {PageIndex} of {TotalPages}).", paginatedList.Items.Count, paginatedList.PageIndex, paginatedList.TotalPages);
            return paginatedList;
        }
    }
}