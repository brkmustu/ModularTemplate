using AutoMapper;
using BaseModule.Common;
using BaseModule.Contracts;
using CoreModule.Application.Common.Contracts;
using CoreModule.Application.Common.Interfaces;
using CoreModule.Application.Extensions;

namespace BaseModule.QueryHandlers;

public class GetUsersQuery : IQuery<Paged<UserDto>>
{
    public PageInfo Paging { get; set; } = new PageInfo();
    public bool? IsActive { get; set; }

    public class Handler : BaseModuleApplicationService, IQueryHandler<GetUsersQuery, Paged<UserDto>>
    {
        public Handler(IBaseModuleDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Paged<UserDto>> Handle(GetUsersQuery query)
        {
            var users = _dbContext.Users
                        .Where(user => query.IsActive.HasValue ? query.IsActive.Value == user.IsActive : true)
                        .Select(user => _mapper.Map<UserDto>(user))
                        .ToArray();

            return await Task.FromResult(users.Page(query.Paging));
        }
    }
}
