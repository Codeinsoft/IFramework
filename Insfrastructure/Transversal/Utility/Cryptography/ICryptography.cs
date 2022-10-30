using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace IFramework.Infrastructure.Utility.Cryptography
{
    public interface ICryptography
    {
        /// <summary>
        /// Encodes input with key using AES
        /// </summary>
        /// <param name="input">Plain text</param>
        /// <param name="email"></param>
        /// <param name="key">Cryptography key</param>
        /// <returns>Crypted Text</returns>
        string EncodeAes(string input, string key);

        /// <summary>
        /// Decodes text with key using AES
        /// </summary>
        /// <param name="key">Cryptography key</param>
        /// <param name="input">Crypted Text</param>
        /// <returns>Plain Text</returns>
        string DecodeAes(string input, string key);
        
        /// <summary>
        /// Sha1 algorithm 
        /// </summary>  
        /// <param name="input">Plain Text</param>
        /// <returns>Sha1 String</returns>
        string Sha1(string input);
        
        /// <summary>
        /// MD5 hash algorithm
        /// </summary>
        /// <param name="input">plain text</param>
        /// <returns>crypted text</returns>
        string Md5Encrypt(string input);

        /// <summary>
        /// Sha256 algorith
        /// </summary>
        /// <param name="input">plain text</param>
        /// <returns></returns>
        string Sha256(string input);

        /// <summary>
        /// Sha256 algorith
        /// </summary>
        /// <param name="input">plain text</param>
        /// <param name="securityStamp">plain text</param>
        /// <returns></returns>
        string Sha256(string input,string securityStamp);

        /// <summary>
        /// Sha 384 algorithm
        /// </summary>
        /// <param name="input">plain text</param>
        /// <returns></returns>
        string Sha384(string input);

        /// <summary>
        /// Sha 384 algorithm
        /// </summary>
        /// <param name="input">plain text</param>
        /// <param name="securityStamp">plain text</param>
        /// <returns></returns>
        string Sha384(string input, string securityStamp);

        /// <summary>
        /// Sha 512 algorithm
        /// </summary>
        /// <param name="input">plain text</param>
        /// <returns></returns>
        string Sha512(string input);
    }
}
