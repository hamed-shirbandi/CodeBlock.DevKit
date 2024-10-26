using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Dtos;
using MediatR;

namespace CodeBlock.DevKit.Authorization.UseCases.Roles.GetRoles;

public class GetRolesUseCase : BaseQueryHandler, IRequestHandler<GetRolesRequest, IEnumerable<GetRoleDto>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRolesUseCase(IMapper mapper, IRoleRepository roleRepository)
        : base(mapper)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<GetRoleDto>> Handle(GetRolesRequest request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetListAsync();

        return _mapper.Map<IEnumerable<GetRoleDto>>(roles);
    }
}
