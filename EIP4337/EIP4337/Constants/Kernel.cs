namespace EIP4337.Constants
{
    public class Kernel
    {
        public const string Factory = "0x5D006d3880645ec6e254E18C1F879DAC9Dd71A39";
        public const string ECDSAFactory = "0xD49a72cb78C44c6bfbf0d471581B7635cF62E81e";
        public const string ECDSAValidator = "0x180D6465F921C7E0DEA0040107D342c87455fFF5";

        public static class Modes
        {
            public const string Sudo = "0x00000000";
            public const string Plugin = "0x00000001";
            public const string Enable = "0x00000002";
        }
    }
}