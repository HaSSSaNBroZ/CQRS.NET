using Application.Abstractions.Messaging;
using Application.Contracts;

namespace Application.Features.Users.Queries.GetUserById;
public sealed record GetUserByIdQuery(int UserId) : IQuery<GetUserByIdViewModel>;