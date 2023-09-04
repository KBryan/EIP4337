namespace EIP4337
{
    public class JsonRpcRequest
    {
        public string Jsonrpc { get; set; } = "2.0";
        
        public int Id { get; set; } = 1;
        public string Method { get; set; }
        public object[] Params { get; set; }
    }
}