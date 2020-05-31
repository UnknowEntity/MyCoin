using Newtonsoft.Json;
using Secp256k1Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Base58Check;

namespace HashCompare
{
    class CryptoCurrency
    {
        public static byte[] StringToSha256(string data)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var hash = SHA256.Create().ComputeHash(buffer);

            return hash;
        }

        public static byte[] Sha256(byte[] data)
        {
            var hash = SHA256.Create().ComputeHash(data);

            return hash;
        }

        public static string Sha1(byte[] data)
        {
            var hash = SHA1.Create().ComputeHash(data);
            var result = Base58CheckEncoding.Encode(hash);
            return result;
        }

        public static byte[] ObjectToSha256(Object data)
        {
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var hash = SHA256.Create().ComputeHash(buffer);

            return hash;
        }

        public static int[] ByteArrayToIntArray(byte[] data)
        {
            return data.Select(b => (int)b).ToArray();
        }

        public static byte[] Hash32Byte()
        {
            using (var secp256k1 = new Secp256k1())
            {
                var hash = new byte[32];
                var rnd = RandomNumberGenerator.Create();
                do { rnd.GetBytes(hash); }
                while (!secp256k1.SecretKeyVerify(hash));

                return hash;
            }
                
        }

        public static byte[] GetPublicKey(byte[] privateKey)
        {
            using (var secp256k1 = new Secp256k1())
            {
                var publicKey = new byte[64];
                secp256k1.PublicKeyCreate(publicKey, privateKey);

                return publicKey;
            }
        }

        public static byte[] Sign(string data, byte[] privateKey)
        {
            var signature = new byte[64];
            var dataHash = StringToSha256(data);

            using (var secp256k1 = new Secp256k1())
            {
                secp256k1.Sign(signature, dataHash, privateKey);
                return signature;
            }
        }

        public static byte[] Sign(object data, byte[] privateKey)
        {
            var signature = new byte[64];
            var dataHash = ObjectToSha256(data);

            using (var secp256k1 = new Secp256k1())
            {
                secp256k1.Sign(signature, dataHash, privateKey);
                return signature;
            }
        }

        public static string ByteArrayToString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty);
        }

        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
