using MediatR;

namespace Raditap.BusinessLogic.ApiDataObjects.Orders
{
    public class LastOrderNumberRequest : RequestBase, IRequest<LastOrderNumberResponse>
    {}
}
