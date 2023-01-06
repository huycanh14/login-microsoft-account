using System.ComponentModel.DataAnnotations;
using AlecEdu_api.Application.Common;
using AlecEdu_api.Application.Models;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlecEdu_api.Application.Features.V1.Users.Queries.GetUsers;

public class GetUserQuery: IRequest<BaseResponse<ListResponse<UserDto>>>
{
    public FilterBase<User> Filter { get; init; } = null!;
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, BaseResponse<ListResponse<UserDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserQueryHandler> _logger;
    
    public GetUserQueryHandler(
        ILogger<GetUserQueryHandler> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<BaseResponse<ListResponse<UserDto>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetUserQueryHandler@Handle -- Start");
        var requestFilter = request.Filter;
        var filter = requestFilter.GetFilterWhere();
        var data = await _unitOfWork.GetRepository<User>().GetAllFilterAsync(
            fields: filter,
            memberNames: requestFilter.MemberNames,
            keySort: requestFilter.UnqualifiedFieldName,
            includes: requestFilter.Includes,
            sortDir: requestFilter.sort,
            pageSize: requestFilter.Rows,
            page: requestFilter.Page
        );
        var listUser = _mapper.Map<IEnumerable<UserDto>>(data.ListData);

        var properties = typeof(UserDto).GetProperties()
            .Where(p => p.IsDefined(typeof(DisplayAttribute), false) && !p.IsDefined(typeof(JsonIgnoreAttribute), false))
            .Select(p => new HeaderTableVm
            {
                Text = p.Name.Substring(0, 1).ToLower() + p.Name.Substring(1),
                Value = p.GetCustomAttributes(typeof(DisplayAttribute),
                    false).Cast<DisplayAttribute>().Single().Name
            }).ToList();

        _logger.LogInformation("CommonGetListQueryEntityHandler@Handle -- End");
        return new BaseResponse<ListResponse<UserDto>>(
            data: new ListResponse<UserDto>(
                data: listUser,
                listHeader: properties,
                count: data.Count),
            resultCode: EResultCode.GET
        );
    }
}