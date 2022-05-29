using MediatR;

namespace Raditap.BusinessLogic.ApiDataObjects.Users.Login
{
    public class LogoutRequest : RequestBase, IRequest<LogoutResponse> { }
}
