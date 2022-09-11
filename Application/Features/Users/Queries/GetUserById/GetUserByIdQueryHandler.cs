using Application.Abstractions.Messaging;
using Application.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetUserById
{
    internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, GetUserByIdViewModel>
    {
        private readonly IBaseRepository<User> repositoryUser;
        public GetUserByIdQueryHandler(IBaseRepository<User> repositoryUser)
        {
            this.repositoryUser = repositoryUser;
        }

        public async Task<GetUserByIdViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {


            //TODO

            return new GetUserByIdViewModel(); 
        }
    }
}
