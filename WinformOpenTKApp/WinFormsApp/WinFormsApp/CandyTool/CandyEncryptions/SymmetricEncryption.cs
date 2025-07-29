using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.CandyTool.CandyEncryptions
{
    public class SymmetricEncryption
    {
        /// <summary>
        /// DES加密方法
        /// </summary>
        /// <param name="plainText">待加密的明文</param>
        /// <param name="key">密钥，必须为8字节长度字符串</param>
        /// <param name="iv">初始化向量(可选)，ECB模式下无需提供</param>
        /// <param name="cipherMode">加密模式，默认为CBC</param>
        /// <param name="paddingMode">填充模式，默认为PKCS7</param>
        /// <param name="encoding">字符编码，默认为UTF8</param>
        /// <returns>Base64编码的密文</returns>
        public static string EncryptByDES(string plainText,
            string key,
            string iv = null,
            CipherMode cipherMode = CipherMode.CBC,
            PaddingMode paddingMode = PaddingMode.PKCS7,
            Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            encoding ??= Encoding.UTF8;

            // 验证密钥长度
            byte[] keyBytes = encoding.GetBytes(key);
            if (keyBytes.Length != 8)
                throw new ArgumentException("DES密钥必须为8字节长度", nameof(key));

            // 验证IV（如果需要）
            if (cipherMode != CipherMode.ECB && string.IsNullOrEmpty(iv))
                throw new ArgumentException("非ECB模式需要提供初始化向量(IV)", nameof(iv));

            byte[] ivBytes = null;
            if (!string.IsNullOrEmpty(iv))
            {
                ivBytes = encoding.GetBytes(iv);
                if (ivBytes.Length != 8)
                    throw new ArgumentException("DES初始化向量必须为8字节长度", nameof(iv));
            }

            using var des = DES.Create();
            des.Mode = cipherMode;
            des.Padding = paddingMode;
            des.Key = keyBytes;

            if (ivBytes != null)
                des.IV = ivBytes;

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);

            sw.Write(plainText);
            sw.Flush();
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// DES解密方法
        /// </summary>
        /// <param name="cipherText">Base64编码的密文</param>
        /// <param name="key">密钥，必须与加密时使用的密钥相同</param>
        /// <param name="iv">初始化向量(可选)，必须与加密时使用的IV相同</param>
        /// <param name="cipherMode">加密模式，必须与加密时使用的模式相同</param>
        /// <param name="paddingMode">填充模式，必须与加密时使用的填充模式相同</param>
        /// <param name="encoding">字符编码，必须与加密时使用的编码相同</param>
        /// <returns>解密后的明文</returns>
        public static string DecryptByDES(string cipherText,
            string key,
            string iv = null,
            CipherMode cipherMode = CipherMode.CBC,
            PaddingMode paddingMode = PaddingMode.PKCS7,
            Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            encoding ??= Encoding.UTF8;

            // 验证密钥长度
            byte[] keyBytes = encoding.GetBytes(key);
            if (keyBytes.Length != 8)
                throw new ArgumentException("DES密钥必须为8字节长度", nameof(key));

            // 验证IV（如果需要）
            if (cipherMode != CipherMode.ECB && string.IsNullOrEmpty(iv))
                throw new ArgumentException("非ECB模式需要提供初始化向量(IV)", nameof(iv));

            byte[] ivBytes = null;
            if (!string.IsNullOrEmpty(iv))
            {
                ivBytes = encoding.GetBytes(iv);
                if (ivBytes.Length != 8)
                    throw new ArgumentException("DES初始化向量必须为8字节长度", nameof(iv));
            }

            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using var des = DES.Create();
            des.Mode = cipherMode;
            des.Padding = paddingMode;
            des.Key = keyBytes;

            if (ivBytes != null)
                des.IV = ivBytes;

            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }


        /// <summary>
        /// 3DES（Triple DES）加密方法
        /// </summary>
        /// <param name="plainText">待加密的明文</param>
        /// <param name="key">密钥，支持168位（21字节）或192位（24字节）</param>
        /// <param name="iv">初始化向量(可选)，ECB模式下无需提供，必须为8字节</param>
        /// <param name="cipherMode">加密模式，默认为CBC</param>
        /// <param name="paddingMode">填充模式，默认为PKCS7</param>
        /// <param name="encoding">字符编码，默认为UTF8</param>
        /// <returns>Base64编码的密文</returns>
        public static string EncryptByTripleDES(string plainText,
            string key,
            string iv = null,
            CipherMode cipherMode = CipherMode.CBC,
            PaddingMode paddingMode = PaddingMode.PKCS7,
            Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            encoding ??= Encoding.UTF8;

            // 验证密钥长度（支持168位或192位）
            byte[] keyBytes = encoding.GetBytes(key);
            if (keyBytes.Length != 16 && keyBytes.Length != 24)
                throw new ArgumentException("3DES密钥必须为16字节(128位)或24字节(192位)", nameof(key));

            // 验证IV（如果需要）
            if (cipherMode != CipherMode.ECB && string.IsNullOrEmpty(iv))
                throw new ArgumentException("非ECB模式需要提供初始化向量(IV)", nameof(iv));

            byte[] ivBytes = null;
            if (!string.IsNullOrEmpty(iv))
            {
                ivBytes = encoding.GetBytes(iv);
                if (ivBytes.Length != 8)
                    throw new ArgumentException("3DES初始化向量必须为8字节", nameof(iv));
            }

            using var tripleDes = TripleDES.Create();
            tripleDes.Mode = cipherMode;
            tripleDes.Padding = paddingMode;
            tripleDes.Key = keyBytes;

            if (ivBytes != null)
                tripleDes.IV = ivBytes;

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, tripleDes.CreateEncryptor(), CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);

            sw.Write(plainText);
            sw.Flush();
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// 3DES（Triple DES）解密方法
        /// </summary>
        /// <param name="cipherText">Base64编码的密文</param>
        /// <param name="key">密钥，必须与加密时使用的密钥相同</param>
        /// <param name="iv">初始化向量(可选)，必须与加密时使用的IV相同</param>
        /// <param name="cipherMode">加密模式，必须与加密时使用的模式相同</param>
        /// <param name="paddingMode">填充模式，必须与加密时使用的填充模式相同</param>
        /// <param name="encoding">字符编码，必须与加密时使用的编码相同</param>
        /// <returns>解密后的明文</returns>
        public static string DecryptByTripleDES(string cipherText,
            string key,
            string iv = null,
            CipherMode cipherMode = CipherMode.CBC,
            PaddingMode paddingMode = PaddingMode.PKCS7,
            Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            encoding ??= Encoding.UTF8;

            // 验证密钥长度
            byte[] keyBytes = encoding.GetBytes(key);
            if (keyBytes.Length != 16 && keyBytes.Length != 24)
                throw new ArgumentException("3DES密钥必须为16字节(128位)或24字节(192位)", nameof(key));

            // 验证IV（如果需要）
            if (cipherMode != CipherMode.ECB && string.IsNullOrEmpty(iv))
                throw new ArgumentException("非ECB模式需要提供初始化向量(IV)", nameof(iv));

            byte[] ivBytes = null;
            if (!string.IsNullOrEmpty(iv))
            {
                ivBytes = encoding.GetBytes(iv);
                if (ivBytes.Length != 8)
                    throw new ArgumentException("3DES初始化向量必须为8字节", nameof(iv));
            }

            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using var tripleDes = TripleDES.Create();
            tripleDes.Mode = cipherMode;
            tripleDes.Padding = paddingMode;
            tripleDes.Key = keyBytes;

            if (ivBytes != null)
                tripleDes.IV = ivBytes;

            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, tripleDes.CreateDecryptor(), CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }


        /// <summary>
        /// AES加密方法
        /// </summary>
        /// <param name="plainText">待加密的明文</param>
        /// <param name="key">密钥，支持128位(16字节)、192位(24字节)或256位(32字节)</param>
        /// <param name="iv">初始化向量(可选)，ECB模式下无需提供，必须与块大小一致(通常16字节)</param>
        /// <param name="keySize">密钥长度(位)，可选值：128、192、256</param>
        /// <param name="cipherMode">加密模式，默认为CBC</param>
        /// <param name="paddingMode">填充模式，默认为PKCS7</param>
        /// <param name="encoding">字符编码，默认为UTF8</param>
        /// <returns>Base64编码的密文</returns>
        public static string EncryptByAES(string plainText,
            string key,
            string iv = null,
            int keySize = 256,
            CipherMode cipherMode = CipherMode.CBC,
            PaddingMode paddingMode = PaddingMode.PKCS7,
            Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            encoding ??= Encoding.UTF8;

            // 验证密钥长度
            byte[] keyBytes = encoding.GetBytes(key);
            if (keySize == 128 && keyBytes.Length != 16 ||
                keySize == 192 && keyBytes.Length != 24 ||
                keySize == 256 && keyBytes.Length != 32)
            {
                throw new ArgumentException($"AES{keySize}密钥必须为{keySize / 8}字节长度", nameof(key));
            }

            // 验证IV（如果需要）
            if (cipherMode != CipherMode.ECB && string.IsNullOrEmpty(iv))
                throw new ArgumentException("非ECB模式需要提供初始化向量(IV)", nameof(iv));

            byte[] ivBytes = null;
            if (!string.IsNullOrEmpty(iv))
            {
                ivBytes = encoding.GetBytes(iv);
                if (ivBytes.Length != 16) // AES块大小固定为128位(16字节)
                    throw new ArgumentException("AES初始化向量必须为16字节", nameof(iv));
            }

            using var aes = Aes.Create();
            aes.KeySize = keySize;
            aes.BlockSize = 128; // AES块大小固定为128位
            aes.Mode = cipherMode;
            aes.Padding = paddingMode;
            aes.Key = keyBytes;

            if (ivBytes != null)
                aes.IV = ivBytes;

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);

            sw.Write(plainText);
            sw.Flush();
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// AES解密方法
        /// </summary>
        /// <param name="cipherText">Base64编码的密文</param>
        /// <param name="key">密钥，必须与加密时使用的密钥相同</param>
        /// <param name="iv">初始化向量(可选)，必须与加密时使用的IV相同</param>
        /// <param name="keySize">密钥长度(位)，必须与加密时使用的密钥长度相同</param>
        /// <param name="cipherMode">加密模式，必须与加密时使用的模式相同</param>
        /// <param name="paddingMode">填充模式，必须与加密时使用的填充模式相同</param>
        /// <param name="encoding">字符编码，必须与加密时使用的编码相同</param>
        /// <returns>解密后的明文</returns>
        public static string DecryptByAES(string cipherText,
            string key,
            string iv = null,
            int keySize = 256,
            CipherMode cipherMode = CipherMode.CBC,
            PaddingMode paddingMode = PaddingMode.PKCS7,
            Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            encoding ??= Encoding.UTF8;

            // 验证密钥长度
            byte[] keyBytes = encoding.GetBytes(key);
            if (keySize == 128 && keyBytes.Length != 16 ||
                keySize == 192 && keyBytes.Length != 24 ||
                keySize == 256 && keyBytes.Length != 32)
            {
                throw new ArgumentException($"AES{keySize}密钥必须为{keySize / 8}字节长度", nameof(key));
            }

            // 验证IV（如果需要）
            if (cipherMode != CipherMode.ECB && string.IsNullOrEmpty(iv))
                throw new ArgumentException("非ECB模式需要提供初始化向量(IV)", nameof(iv));

            byte[] ivBytes = null;
            if (!string.IsNullOrEmpty(iv))
            {
                ivBytes = encoding.GetBytes(iv);
                if (ivBytes.Length != 16) // AES块大小固定为128位(16字节)
                    throw new ArgumentException("AES初始化向量必须为16字节", nameof(iv));
            }

            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.KeySize = keySize;
            aes.BlockSize = 128;
            aes.Mode = cipherMode;
            aes.Padding = paddingMode;
            aes.Key = keyBytes;

            if (ivBytes != null)
                aes.IV = ivBytes;

            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }

    }
}
