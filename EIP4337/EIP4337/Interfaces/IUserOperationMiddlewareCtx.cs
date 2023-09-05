using System.Numerics;

namespace EIP4337
{
    public interface IUserOperationMiddlewareCtx
    {
        IUserOperation Op { get; set; }
        string EntryPoint { get; set; }
        BigInteger ChainId { get; set; }
        string GetUserOpHash();
    }
}