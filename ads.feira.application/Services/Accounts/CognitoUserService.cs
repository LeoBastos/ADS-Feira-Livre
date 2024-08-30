using ads.feira.application.CQRS.Accounts.Commands;
using ads.feira.application.CQRS.Accounts.Queries;
using ads.feira.application.DTO.Accounts;
using ads.feira.application.Interfaces.Accounts;
using ads.feira.domain.Interfaces.Accounts;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace ads.feira.application.Services.Accounts
{
    public class CognitoUserService : ICognitoUserService
    {
        private readonly ICognitoUserRepository _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CognitoUserService(IMapper mapper, ICognitoUserRepository context, IMediator mediator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<CognitoUserDTO>> Find(Expression<Func<CognitoUserDTO, bool>> predicate)
        {
            var findUserQuery = new FindCognitoUserQuery(predicate);
            var result = await _mediator.Send(findUserQuery);
            return _mapper.Map<IEnumerable<CognitoUserDTO>>(result);
        }

        public async Task<IEnumerable<CognitoUserDTO>> GetAll()
        {
            var usersQuery = new GetAllCognitoUserQuery();
            var result = await _mediator.Send(usersQuery);
            return _mapper.Map<IEnumerable<CognitoUserDTO>>(result);
        }

        public async Task<CognitoUserDTO> GetById(Guid id)
        {
            var userQuery = new GetCognitoUserByIdQuery(id);
            var result = await _mediator.Send(userQuery);

            if (result == null)
                throw new NullReferenceException("Usuário não encontrado com o id fornecido.");

            return _mapper.Map<CognitoUserDTO>(result);
        }

        public async Task Create(CreateCognitoUserDTO cognitoDTO)
        {           

            if (cognitoDTO == null)
            {
                throw new ArgumentNullException(nameof(cognitoDTO));
            }

            var userCreateCommand = _mapper.Map<CognitoUserCreateCommand>(cognitoDTO);

            if (userCreateCommand == null)
            {
                throw new Exception("Entity could not be created.");
            }

            var createdUser = await _mediator.Send(userCreateCommand);
        }

        public async Task Update(CognitoUserDTO cognitoDTO)
        {
            var userUpdateCommand = _mapper.Map<CognitoUserUpdateCommand>(cognitoDTO);

            if (userUpdateCommand == null)
            {
                throw new Exception("Entity could not be updated.");
            }

            await _mediator.Send(userUpdateCommand);
        }

        public async Task Remove(Guid id)
        {
            var userRemoveCommand = new CognitoUserRemoveCommand(id);
            await _mediator.Send(userRemoveCommand);
        }
    }
}
