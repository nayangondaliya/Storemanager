using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Interfaces.Validations
{
    public interface IRequestValidator<in TReq, TRes> where TReq : class where TRes : class
    {
        Task<TRes> Validate(TReq request);
    }
}
