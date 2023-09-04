using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace EIP4337
{
    

    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var userOperationParams = new UserOperationParams
            {
                Sender = "0x0000000000000000000000000000000000000000",
                Nonce = "0x0",
                InitCode = "0x",
                CallData = "0x",
                CallGasLimit = "0x0",
                VerificationGasLimit = "0x0",
                PreVerificationGas = "0x0",
                MaxFeePerGas = "0x0",
                MaxPriorityFeePerGas = "0x0",
                PaymasterAndData = "0x",
                Signature = "0x"
            };
            // Send User Operation
            var jsonRpcRequestSendUserOperation = new JsonRpcRequest
            {
                Method = "eth_sendUserOperation",
                Params = new object[] { userOperationParams, "0x5FF137D4b0FDCD49DcA30c7CF57E578a026d2789" }
            };
            // Get UserOperation Receipt
            var jsonRpcRequestUserOperationReceipt = new JsonRpcRequest
            {
                Method = "eth_getUserOperationReceipt",
                Params = new object[] { "0x" }
            };
            // Get Rpc Chain of Bundler
            var jsonRpcRequestChainId = new JsonRpcRequest
            {
                Method = "eth_chainId",
                Params = new object[] { }  // Empty array as per your example
            };
            // Get User Operation by Hash
            var jsonRpcRequestserOperationByHash = new JsonRpcRequest
            {
                Method = "eth_getUserOperationByHash",
                Params = new object[] { "0x" }  // Your hash parameter
            };
            // Get supported Entry Points
            var jsonRpcRequestEntryPoints = new JsonRpcRequest
            {
                Method = "eth_supportedEntryPoints",
                Params = new object[] { }  // Empty array as per your example
            };
            
            var jsonRpcRequestSponsorUserOperation = new JsonRpcRequest
            {
                Method = "pm_sponsorUserOperation",
                Params = new object[] { userOperationParams, "0x5FF137D4b0FDCD49DcA30c7CF57E578a026d2789", new { type = "payg" } }
            };
            
            var jsonRpcRequestPaymasterAccounts = new JsonRpcRequest
            {
                Method = "pm_accounts",
                Params = new object[] { "0x5FF137D4b0FDCD49DcA30c7CF57E578a026d2789" }  // Your account parameter
            };
            // Send Request
            var response = await SendRequestAsync(jsonRpcRequestChainId);
            Console.WriteLine("Response: " + response.Content);
        }

        public static async Task<RestResponse> SendRequestAsync(JsonRpcRequest jsonRpcRequest)
        {
            var options = new RestClientOptions("https://api.stackup.sh/v1/node/ADD_BUNDLER_RPC");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddJsonBody(JsonConvert.SerializeObject(jsonRpcRequest));

            return await client.PostAsync(request);
        }
    }
}