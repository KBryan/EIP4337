using System.Numerics;
using System.Threading.Tasks;
using EIP4337;

public delegate Task UserOperationMiddlewareFn(IUserOperationMiddlewareCtx context);

namespace EIP4337
{
    public interface IUserOperationBuilder
    {
        // Get methods
        string GetSender();
        BigInteger GetNonce();
        byte[] GetInitCode();
        byte[] GetCallData();
        BigInteger GetCallGasLimit();
        BigInteger GetVerificationGasLimit();
        BigInteger GetPreVerificationGas();
        BigInteger GetMaxFeePerGas();
        BigInteger GetMaxPriorityFeePerGas();
        byte[] GetPaymasterAndData();
        byte[] GetSignature();
        IUserOperation GetOp();

        // Set methods
        IUserOperationBuilder SetSender(string address);
        IUserOperationBuilder SetNonce(BigInteger nonce);
        IUserOperationBuilder SetInitCode(byte[] code);
        IUserOperationBuilder SetCallData(byte[] data);
        IUserOperationBuilder SetCallGasLimit(BigInteger gas);
        IUserOperationBuilder SetVerificationGasLimit(BigInteger gas);
        IUserOperationBuilder SetPreVerificationGas(BigInteger gas);
        IUserOperationBuilder SetMaxFeePerGas(BigInteger fee);
        IUserOperationBuilder SetMaxPriorityFeePerGas(BigInteger fee);
        IUserOperationBuilder SetPaymasterAndData(byte[] data);
        IUserOperationBuilder SetSignature(byte[] bytes);
        IUserOperationBuilder SetPartial(IUserOperation partialOp);

        IUserOperationBuilder UseDefaults(IUserOperation partialOp);
        IUserOperationBuilder ResetDefaults();
        IUserOperationBuilder UseMiddleware(UserOperationMiddlewareFn fn);
        IUserOperationBuilder ResetMiddleware();
        Task<IUserOperation> BuildOp(string entryPoint, BigInteger chainId);
        IUserOperationBuilder ResetOp();
    }
}