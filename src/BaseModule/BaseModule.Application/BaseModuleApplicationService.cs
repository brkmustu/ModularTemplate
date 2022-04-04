using AutoMapper;
using BaseModule.Common;

namespace BaseModule
{
    public abstract class BaseModuleApplicationService
    {
        protected readonly IBaseModuleDbContext _dbContext;
        protected readonly IMapper _mapper;

        protected BaseModuleApplicationService(IBaseModuleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
    }
}
