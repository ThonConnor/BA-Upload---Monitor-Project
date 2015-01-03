using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace KTVServerApp.Script.Encryption
{
    [Serializable]
    public class MyParam
    {
        public byte[] D;
        public byte[] DP;
        public byte[] DQ;
        public byte[] P;
        public byte[] Q;
        public byte[] InverseQ;
        public byte[] Module;
        public byte[] Exponent;

        public MyParam(RSAParameters param)
        {
            this.D = param.D;
            this.DP = param.DP;
            this.DQ = param.DQ;
            this.P = param.P;
            this.Q = param.Q;
            this.InverseQ = param.InverseQ;
            this.Module = param.Modulus;
            this.Exponent = param.Exponent;
        }
        public RSAParameters GetValue()
        {
            RSAParameters param = new RSAParameters();
            param.D = this.D;
            param.DP = this.DP;
            param.DQ = this.DQ;
            param.P = this.P;
            param.Q = this.Q;
            param.InverseQ = this.InverseQ;
            param.Modulus = this.Module;
            param.Exponent = this.Exponent;
            return param;
        }
    }
}
