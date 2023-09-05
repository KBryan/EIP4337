namespace EIP4337
{
    public class UserOperationParams
    {
        public string Sender { get; set; }
        public string Nonce { get; set; }
        public string InitCode { get; set; }
        public string CallData { get; set; }
        public string CallGasLimit { get; set; }
        public string VerificationGasLimit { get; set; }
        public string PreVerificationGas { get; set; }
        public string MaxFeePerGas { get; set; }
        public string MaxPriorityFeePerGas { get; set; }
        public string PaymasterAndData { get; set; }
        public string Signature { get; set; }
    }
} 
