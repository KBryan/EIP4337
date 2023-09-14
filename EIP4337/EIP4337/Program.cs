using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Signer;
using Nethereum.Web3.Accounts;
using RestSharp;
using Newtonsoft.Json;
using Web3Dots.RPC.Contracts;
using Web3Dots.RPC.Providers;

namespace EIP4337
{
    public class JsonRpcRequestUserOp
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpc { get; set; } = "2.0";

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public object[] Params { get; set; }
    }
    internal class Program 
    {
        const string signingKey = "ADD_SIGNING-KEY";
        const string rpcUrl = "https://api.stackup.sh/v1/node/API_KEY";
        static BigInteger chainId = 1482601649;
        const string contractAddress = "0x0dB8dA9D41F6F0663559804A499Cf5842694D6aF";
        const string mintingABI = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"approved\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"Approval\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"ApprovalForAll\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"approve\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"}],\"name\":\"safeMint\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"safeTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"safeTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"setApprovalForAll\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"getApproved\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"}],\"name\":\"isApprovedForAll\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"ownerOf\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes4\",\"name\":\"interfaceId\",\"type\":\"bytes4\"}],\"name\":\"supportsInterface\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"tokenURI\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";
        static Account _account;
        static JsonRpcProvider _provider;  
        static Contract _contractMint;
        private static MessageSigner _signer;

        public static async Task Main(string[] args)
        {
            _contractMint =  new Contract(mintingABI, contractAddress);
            _account = new Account(new EthECKey(signingKey), chainId);
            _provider = new JsonRpcProvider(_account,rpcUrl);
            _signer = new MessageSigner();

            Console.WriteLine("Sign Message: " + _signer.HashAndSign("Hello World", signingKey ));
            
            var calldata = _contractMint.Calldata("safeMint", new object[]
            {
                "0x5Bf3DC356A5e41021AE208667a835DfB143Bf4b4"
            });
            
            Console.WriteLine("Call Data: " + calldata);
            Console.WriteLine("Balance: " + _provider.GetBalance(_account.Address).Result);
            
            var userOperationParams = new UserOperationParams
            {
                Sender = "0x0000000000000000000000000000000000000000",
                Nonce = "0x",
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
            var jsonRpcRequestSendUserOperation = new JsonRpcRequestUserOp
            {
                Method = "eth_sendUserOperation",
                Params = new object[] { userOperationParams, "0x5FF137D4b0FDCD49DcA30c7CF57E578a026d2789" },
                Id = 1
            };
            
            // Get UserOperation Receipt
            var jsonRpcRequestUserOperationReceipt = new JsonRpcRequestUserOp
            {
                Method = "eth_getUserOperationReceipt",
                Params = new object[] { "0xbcee47418db45b565284445cbe4cbb315dd72733fcd3e4e8dd72ffe29020c7cd" },
                Id = 1
            };
            
            // Get Rpc Chain of Bundler
            var jsonRpcRequestChainId = new JsonRpcRequestUserOp
            {
                Method = "eth_chainId",
                Params = new object[] { },
                Id = 1
            };
            
            // Get User Operation by Hash
            var jsonRpcRequestserOperationByHash = new JsonRpcRequestUserOp
            {
                Method = "eth_getUserOperationByHash",
                Params = new object[] { "0x6cd9b281472cb8420d2fc92733a88c931edfee3a2657e20c31448cdbc73e283c" },
                Id = 1// Your hash parameter
            };
            
            // Get supported Entry Points
            var jsonRpcRequestEntryPoints = new JsonRpcRequestUserOp
            {
                Method = "eth_supportedEntryPoints",
                Params = new object[] { },
                Id = 1// Empty array as the method doesn't require parameters 
            };
            
            // Get supported Entry Points
            var jsonRpcRequestBundlerState= new JsonRpcRequestUserOp
            {
                Method = "debug_bundler_setBundlingMode",
                Params = new object[] { },
                Id = 1// Empty array as the method doesn't require parameters 
            };
            
            var jsonRpcRequestSponsorUserOperation = new JsonRpcRequestUserOp
            {
                Method = "pm_sponsorUserOperation",
                Params = new object[] { userOperationParams, "0x5FF137D4b0FDCD49DcA30c7CF57E578a026d2789", new { type = "payg" } },
                Id = 1
            };
            
            var jsonRpcRequestPaymasterAccounts = new JsonRpcRequestUserOp
            {
                Method = "pm_accounts",
                Params = new object[] { "0x5FF137D4b0FDCD49DcA30c7CF57E578a026d2789" },
                Id = 1// Your account parameter
            };
            // Send Request
            var response = await SendRequestAsync(jsonRpcRequestEntryPoints);
            Console.WriteLine("Response: " + response.Content);
        }


        private static async Task<RestResponse> SendRequestAsync(JsonRpcRequestUserOp jsonRpcRequest)
        {
            var options = new RestClientOptions(rpcUrl);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddJsonBody(JsonConvert.SerializeObject(jsonRpcRequest));
            return await client.PostAsync(request);
        }
    }
}
