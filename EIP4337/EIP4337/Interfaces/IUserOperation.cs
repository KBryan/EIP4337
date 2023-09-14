using System.Numerics;

namespace EIP4337.Interfaces
{
    public interface IUserOperation
    {
        string Sender { get; set; }
        BigInteger Nonce { get; set; }
        byte[] InitCode { get; set; }
        byte[] CallData { get; set; }
        BigInteger CallGasLimit { get; set; }
        BigInteger VerificationGasLimit { get; set; }
        BigInteger PreVerificationGas { get; set; }
        BigInteger MaxFeePerGas { get; set; }
        BigInteger MaxPriorityFeePerGas { get; set; }
        byte[] PaymasterAndData { get; set; }
        byte[] Signature { get; set; }
    }
}