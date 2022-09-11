using Application.Abstractions.Messaging;
using Application.Contracts;

namespace Application.Features.Users.Commands.CreateUser;
public sealed record CreateUserCommand(string FirstName, string LastName) : ICommand<UserResponse>;