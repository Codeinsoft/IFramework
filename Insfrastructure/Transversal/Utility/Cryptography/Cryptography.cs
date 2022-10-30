using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace IFramework.Infrastructure.Utility.Cryptography
{
    public class Cryptography : ICryptography
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">Cryptography key</param>
        /// <returns></returns>
        private RijndaelManaged GetCryptoParams(string key)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(key);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            RijndaelManaged aes = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = new byte[16]
            };

            return aes;
        }

        /// <summary>
        /// Encodes input with key using AES
        /// </summary>
        /// <param name="input">Plain text</param>
        /// <param name="email"></param>
        /// <param name="key">Cryptography key</param>
        /// <returns>Crypted Text</returns>
        public string EncodeAes(string input, string key)
        {
            RijndaelManaged aes = GetCryptoParams(key);
            var plainBytes = Encoding.UTF8.GetBytes(input);


            return Convert.ToBase64String(AesEncrypt(plainBytes, aes));
        }

        /// <summary>
        /// Decodes text with key using AES
        /// </summary>
        /// <param name="key">Cryptography key</param>
        /// <param name="input">Crypted Text</param>
        /// <returns>Plain Text</returns>
        public string DecodeAes(string input, string key)
        {
            RijndaelManaged aes = GetCryptoParams(key);
            try
            {
                var encryptedBytes = Convert.FromBase64String(input);
                return Encoding.UTF8.GetString(AesDecrypt(encryptedBytes, aes));
            }
            catch
            {
                return null;
            }
        }



        /// <summary>
        /// Sha1 algorithm 
        /// </summary>  
        /// <param name="input">Plain Text</param>
        /// <returns>Sha1 String</returns>
        public string Sha1(string input)
        {
            SHA1 sha = new SHA1Managed();
            byte[] data = Encoding.UTF8.GetBytes(input);
            byte[] digest = sha.ComputeHash(data);

            return GetAsHexaDecimal(digest);
        }

        /// <summary>
        /// Byte array to Hex string
        /// </summary>
        /// <param name="bytes">Byte array which will be converted</param>
        /// <returns>Hex String</returns>
        private string GetAsHexaDecimal(IList<byte> bytes)
        {
            StringBuilder s = new StringBuilder();
            int length = bytes.Count;
            for (int n = 0; n < length; n++)
            {
                s.Append(String.Format("{0,2:x}", bytes[n]).Replace(" ", "0"));
            }
            return s.ToString();
        }

        /// <summary>
        /// Byte array to string
        /// </summary>
        /// <param name="bytes">Byte array wihich will be converted</param>
        /// <returns></returns>
        private string GetAsString(IList<byte> bytes)
        {
            StringBuilder s = new StringBuilder();
            int length = bytes.Count;
            for (int n = 0; n < length; n++)
            {
                s.Append((int)bytes[n]);
                if (n != length - 1)
                {
                    s.Append(' ');
                }
            }
            return s.ToString();
        }

        /// <summary>
        /// MD5 hash algorithm
        /// </summary>
        /// <param name="input">plain text</param>
        /// <returns>crypted text</returns>
        public string Md5Encrypt(string input)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(input));
            return ByteArrayToString(hashedDataBytes);
        }

        /// <summary>
        /// Sha256 algorith
        /// </summary>
        /// <param name="input">plain text</param>
        /// <returns></returns>
        public string Sha256(string input)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256Hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256Hasher.ComputeHash(encoder.GetBytes(input));
            return ByteArrayToString(hashedDataBytes);
        }

        /// <summary>
        /// Sha256 algorith
        /// </summary>
        /// <param name="input">plain text</param>
        /// <param name="securityStamp">plain text</param>
        /// <returns></returns>
        public string Sha256(string input, string securityStamp)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256Hasher = new SHA256Managed();
            var saltedInput = $"{input}{securityStamp}";
            byte[] hashedDataBytes = sha256Hasher.ComputeHash(encoder.GetBytes(saltedInput));
            return ByteArrayToString(hashedDataBytes);
        }

        /// <summary>
        /// Sha 384 algorithm
        /// </summary>
        /// <param name="input">plain text</param>
        /// <returns></returns>
        public string Sha384(string input)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA384Managed sha384Hasher = new SHA384Managed();
            byte[] hashedDataBytes = sha384Hasher.ComputeHash(encoder.GetBytes(input));
            return ByteArrayToString(hashedDataBytes);
        }
        /// <summary>
        /// Sha 384 algorithm
        /// </summary>
        /// <param name="input">plain text</param>
        /// <param name="securityStamp">plain text</param>
        /// <returns></returns>
        public string Sha384(string input, string securityStamp)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA384Managed sha384Hasher = new SHA384Managed();
            var saltedInput = $"{input}{securityStamp}";
            byte[] hashedDataBytes = sha384Hasher.ComputeHash(encoder.GetBytes(saltedInput));
            return ByteArrayToString(hashedDataBytes);
        }

        /// <summary>
        /// Sha 512 algorithm
        /// </summary>
        /// <param name="input">plain text</param>
        /// <returns></returns>
        public string Sha512(string input)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA512Managed sha512Hasher = new SHA512Managed();
            byte[] hashedDataBytes = sha512Hasher.ComputeHash(encoder.GetBytes(input));
            return ByteArrayToString(hashedDataBytes);
        }

        /// <summary>
        /// Byte array to sting
        /// </summary>
        /// <param name="inputArray">byte array</param>
        /// <returns></returns>
        private string ByteArrayToString(IEnumerable<byte> inputArray)
        {
            StringBuilder output = new StringBuilder("");
            foreach (byte item in inputArray)
            {
                output.Append(item.ToString("X2"));
            }
            return output.ToString();
        }

        #region AES

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainBytes"></param>
        /// <param name="rijndaelManaged"></param>
        /// <returns></returns>
        private byte[] AesEncrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {


            return rijndaelManaged.CreateEncryptor()
                                  .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="rijndaelManaged"></param>
        /// <returns></returns>
        private byte[] AesDecrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                                  .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        #endregion
    }
}
