using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.CandyTool.CandyEncryptions
{
    public class AsymmetricEncryption
    {
        /// <summary>
        /// RSA加密方法
        /// </summary>
        /// <param name="plainText">待加密的明文</param>
        /// <param name="publicKeyXml">公钥XML字符串</param>
        /// <param name="keySize">密钥大小，可选值：1024、2048、4096等</param>
        /// <param name="padding">填充模式，默认使用OAEP_SHA256</param>
        /// <param name="encoding">编码方式，默认使用UTF-8</param>
        /// <returns>Base64编码的密文</returns>
        public static string EncryptByRSA(string plainText, string publicKeyXml, int keySize = 2048,
            RSAEncryptionPadding padding = null, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            padding = padding ?? RSAEncryptionPadding.OaepSHA256;

            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                try
                {
                    rsa.FromXmlString(publicKeyXml);
                    var dataToEncrypt = encoding.GetBytes(plainText);
                    var encryptedData = rsa.Encrypt(dataToEncrypt, true);
                    return Convert.ToBase64String(encryptedData);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        /// <summary>
        /// RSA解密方法
        /// </summary>
        /// <param name="cipherText">Base64编码的密文</param>
        /// <param name="privateKeyXml">私钥XML字符串</param>
        /// <param name="keySize">密钥大小，需与加密时一致</param>
        /// <param name="padding">填充模式，需与加密时一致</param>
        /// <param name="encoding">编码方式，需与加密时一致</param>
        /// <returns>解密后的明文</returns>
        public static string DecryptByRSA(string cipherText, string privateKeyXml, int keySize = 2048,
            RSAEncryptionPadding padding = null, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            padding = padding ?? RSAEncryptionPadding.OaepSHA256;

            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                try
                {
                    rsa.FromXmlString(privateKeyXml);
                    var dataToDecrypt = Convert.FromBase64String(cipherText);
                    var decryptedData = rsa.Decrypt(dataToDecrypt, true);
                    return encoding.GetString(decryptedData);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        /// <summary>
        /// 生成RSA密钥对
        /// </summary>
        /// <param name="keySize">密钥大小</param>
        /// <returns>包含公钥和私钥的元组</returns>
        public static (string publicKey, string privateKey) GenerateKeyPairByRSA(int keySize = 2048)
        {
            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                try
                {
                    var publicKey = rsa.ToXmlString(false);
                    var privateKey = rsa.ToXmlString(true);
                    return (publicKey, privateKey);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        /// <summary>
        /// ECC加密方法
        /// </summary>
        /// <param name="plainText">待加密的明文</param>
        /// <param name="publicKeyXml">对方的公钥XML字符串</param>
        /// <param name="curveName">椭圆曲线名称，可选值：P-256、P-384、P-521等</param>
        /// <param name="keyDerivationFunction">密钥派生函数，默认使用HKDF</param>
        /// <param name="hashAlgorithm">哈希算法，默认使用SHA256</param>
        /// <param name="encoding">编码方式，默认使用UTF-8</param>
        /// <returns>Base64编码的密文</returns>
        public static string EncryptByECC(string plainText, string publicKeyXml, string curveName = "P-256",
            ECDiffieHellmanKeyDerivationFunction keyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
            HashAlgorithmName hashAlgorithm = default, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            hashAlgorithm = hashAlgorithm == default ? HashAlgorithmName.SHA256 : hashAlgorithm;

            using (var ecdh = ECDiffieHellman.Create(curveName))
            using (var publicKeyEcdh = ECDiffieHellman.Create(curveName))
            {
                try
                {
                    publicKeyEcdh.FromXmlString(publicKeyXml);

                    // 生成共享密钥
                    byte[] sharedSecret;
                    if (keyDerivationFunction == ECDiffieHellmanKeyDerivationFunction.Hash)
                    {
                        // 使用HKDF派生密钥
                        sharedSecret = ecdh.DeriveKeyFromHash(publicKeyEcdh.PublicKey, hashAlgorithm);
                    }
                    else
                    {
                        // 使用其他密钥派生函数
                        sharedSecret = ecdh.DeriveKeyMaterial(publicKeyEcdh.PublicKey);
                    }

                    // 使用AES进行对称加密
                    using (var aes = Aes.Create())
                    {
                        aes.Key = sharedSecret;
                        aes.GenerateIV();

                        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                        var dataToEncrypt = encoding.GetBytes(plainText);
                        var encryptedData = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);

                        // 合并IV和密文
                        var combined = new byte[aes.IV.Length + encryptedData.Length];
                        Array.Copy(aes.IV, 0, combined, 0, aes.IV.Length);
                        Array.Copy(encryptedData, 0, combined, aes.IV.Length, encryptedData.Length);

                        return Convert.ToBase64String(combined);
                    }
                }
                finally
                {
                    ecdh.Clear();
                }
            }
        }

        /// <summary>
        /// ECC解密方法
        /// </summary>
        /// <param name="cipherText">Base64编码的密文</param>
        /// <param name="privateKeyXml">自己的私钥XML字符串</param>
        /// <param name="curveName">椭圆曲线名称，需与加密时一致</param>
        /// <param name="keyDerivationFunction">密钥派生函数，需与加密时一致</param>
        /// <param name="hashAlgorithm">哈希算法，需与加密时一致</param>
        /// <param name="encoding">编码方式，需与加密时一致</param>
        /// <returns>解密后的明文</returns>
        public static string DecryptByECC(string cipherText, string privateKeyXml, string curveName = "P-256",
            ECDiffieHellmanKeyDerivationFunction keyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
            HashAlgorithmName hashAlgorithm = default, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            hashAlgorithm = hashAlgorithm == default ? HashAlgorithmName.SHA256 : hashAlgorithm;

            using (var ecdh = ECDiffieHellman.Create(curveName))
            {
                try
                {
                    ecdh.FromXmlString(privateKeyXml);

                    // 解析密文，提取IV和加密数据
                    var combined = Convert.FromBase64String(cipherText);
                    var ivLength = 16; // AES IV长度
                    var iv = new byte[ivLength];
                    var encryptedData = new byte[combined.Length - ivLength];

                    Array.Copy(combined, 0, iv, 0, ivLength);
                    Array.Copy(combined, ivLength, encryptedData, 0, encryptedData.Length);

                    // 生成共享密钥
                    var publicKey = ecdh.PublicKey;
                    byte[] sharedSecret;
                    if (keyDerivationFunction == ECDiffieHellmanKeyDerivationFunction.Hash)
                    {
                        // 使用HKDF派生密钥
                        sharedSecret = ecdh.DeriveKeyFromHash(publicKey, hashAlgorithm);
                    }
                    else
                    {
                        // 使用其他密钥派生函数
                        sharedSecret = ecdh.DeriveKeyMaterial(publicKey);
                    }

                    // 使用AES进行对称解密
                    using (var aes = Aes.Create())
                    {
                        aes.Key = sharedSecret;
                        aes.IV = iv;

                        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                        var decryptedData = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

                        return encoding.GetString(decryptedData);
                    }
                }
                finally
                {
                    ecdh.Clear();
                }
            }
        }

        /// <summary>
        /// 生成ECC密钥对
        /// </summary>
        /// <param name="curveName">椭圆曲线名称</param>
        /// <returns>包含公钥和私钥的元组</returns>
        public static (string publicKey, string privateKey) GenerateKeyPairByECC(string curveName = "P-256")
        {
            using (var ecdh = ECDiffieHellman.Create(curveName))
            {
                try
                {
                    var publicKey = ecdh.ToXmlString(false);
                    var privateKey = ecdh.ToXmlString(true);
                    return (publicKey, privateKey);
                }
                finally
                {
                    ecdh.Clear();
                }
            }
        }

        /// <summary>
        /// DSA签名方法
        /// </summary>
        /// <param name="data">待签名的数据</param>
        /// <param name="privateKeyXml">私钥XML字符串</param>
        /// <param name="hashAlgorithm">哈希算法，默认使用SHA256</param>
        /// <param name="keySize">密钥大小，可选值：512-1024（64的倍数）或2048、3072</param>
        /// <param name="encoding">编码方式，默认使用UTF-8</param>
        /// <returns>Base64编码的签名</returns>
        public static string SignDataByDSA(string data, string privateKeyXml,
            HashAlgorithmName hashAlgorithm = default, int keySize = 2048, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            hashAlgorithm = hashAlgorithm == default ? HashAlgorithmName.SHA256 : hashAlgorithm;

            using (var dsa = new DSACryptoServiceProvider(keySize))
            {
                try
                {
                    dsa.FromXmlString(privateKeyXml);

                    // 计算哈希值
                    var hash = ComputeHash(data, hashAlgorithm, encoding);

                    // 生成签名
                    var signature = dsa.SignHash(hash, GetHashAlgorithmName(hashAlgorithm));

                    return Convert.ToBase64String(signature);
                }
                finally
                {
                    dsa.PersistKeyInCsp = false;
                }
            }
        }

        /// <summary>
        /// DSA验证签名方法
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="signature">Base64编码的签名</param>
        /// <param name="publicKeyXml">公钥XML字符串</param>
        /// <param name="hashAlgorithm">哈希算法，需与签名时一致</param>
        /// <param name="keySize">密钥大小，需与签名时一致</param>
        /// <param name="encoding">编码方式，需与签名时一致</param>
        /// <returns>签名是否有效</returns>
        public static bool VerifyDataByDSA(string data, string signature, string publicKeyXml,
            HashAlgorithmName hashAlgorithm = default, int keySize = 2048, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            hashAlgorithm = hashAlgorithm == default ? HashAlgorithmName.SHA256 : hashAlgorithm;

            using (var dsa = new DSACryptoServiceProvider(keySize))
            {
                try
                {
                    dsa.FromXmlString(publicKeyXml);

                    // 计算哈希值
                    var hash = ComputeHash(data, hashAlgorithm, encoding);

                    // 验证签名
                    var signatureBytes = Convert.FromBase64String(signature);
                    return dsa.VerifyHash(hash, GetHashAlgorithmName(hashAlgorithm), signatureBytes);
                }
                finally
                {
                    dsa.PersistKeyInCsp = false;
                }
            }
        }

        /// <summary>
        /// 生成DSA密钥对
        /// </summary>
        /// <param name="keySize">密钥大小</param>
        /// <returns>包含公钥和私钥的元组</returns>
        public static (string publicKey, string privateKey) GenerateKeyPairByDSA(int keySize = 2048)
        {
            using (var dsa = new DSACryptoServiceProvider(keySize))
            {
                try
                {
                    var publicKey = dsa.ToXmlString(false);
                    var privateKey = dsa.ToXmlString(true);
                    return (publicKey, privateKey);
                }
                finally
                {
                    dsa.PersistKeyInCsp = false;
                }
            }
        }

        /// <summary>
        /// 计算数据的哈希值
        /// </summary>
        private static byte[] ComputeHash(string data, HashAlgorithmName hashAlgorithm, Encoding encoding)
        {
            using (var hashAlgorithmInstance = HashAlgorithm.Create(hashAlgorithm.Name))
            {
                var dataBytes = encoding.GetBytes(data);
                return hashAlgorithmInstance.ComputeHash(dataBytes);
            }
        }

        /// <summary>
        /// 获取哈希算法名称的OID表示
        /// </summary>
        private static string GetHashAlgorithmName(HashAlgorithmName hashAlgorithm)
        {
            switch (hashAlgorithm.Name)
            {
                case nameof(HashAlgorithmName.SHA1):
                    return "1.3.14.3.2.26";
                case nameof(HashAlgorithmName.SHA256):
                    return "2.16.840.1.101.3.4.2.1";
                case nameof(HashAlgorithmName.SHA384):
                    return "2.16.840.1.101.3.4.2.2";
                case nameof(HashAlgorithmName.SHA512):
                    return "2.16.840.1.101.3.4.2.3";
                default:
                    throw new NotSupportedException($"不支持的哈希算法: {hashAlgorithm.Name}");
            }
        }

    }
}
