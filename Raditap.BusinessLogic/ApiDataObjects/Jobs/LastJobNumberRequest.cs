using MediatR;

namespace Raditap.BusinessLogic.ApiDataObjects.Jobs
{
    public class LastJobNumberRequest : RequestBase, IRequest<LastJobNumberResponse>
    {
    }
}
