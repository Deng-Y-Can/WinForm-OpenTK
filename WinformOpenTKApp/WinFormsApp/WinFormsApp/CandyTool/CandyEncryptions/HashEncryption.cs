using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.CandyTool.CandyEncryptions
{
    public class HashEncryption
    {
        /// <summary>
        /// 计算输入文本的MD5哈希值
        /// </summary>
        /// <param name="input">待加密的原始文本</param>
        /// <param name="encoding">文本编码方式，默认使用UTF8</param>
        /// <param name="upperCase">是否返回大写形式的哈希值，默认是</param>
        /// <param name="includeHyphen">是否在哈希值中包含连字符，默认否</param>
        /// <param name="salt">盐值，会附加到输入文本后增加安全性</param>
        /// <returns>计算得到的MD5哈希值</returns>
        public static string EncryptByMD5(string input,
            Encoding encoding = null,
            bool upperCase = true,
            bool includeHyphen = false,
            string salt = "")
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // 设置默认编码
            if (encoding == null)
                encoding = Encoding.UTF8;

            // 拼接盐值
            string saltedInput = input + salt;

            // 计算MD5哈希
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = encoding.GetBytes(saltedInput);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // 转换为十六进制字符串
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                    if (includeHyphen && i < hashBytes.Length - 1)
                        sb.Append("-");
                }

                string result = sb.ToString();
                return upperCase ? result.ToUpper() : result;
            }
        }

        /// <summary>
        /// 验证输入文本与目标MD5哈希值是否匹配
        /// </summary>
        /// <param name="input">待验证的原始文本</param>
        /// <param name="md5Hash">目标MD5哈希值</param>
        /// <param name="encoding">文本编码方式，默认使用UTF8</param>
        /// <param name="upperCase">哈希值是否为大写形式，默认是</param>
        /// <param name="includeHyphen">哈希值中是否包含连字符，默认否</param>
        /// <param name="salt">加密时使用的盐值</param>
        /// <returns>如果匹配返回true，否则返回false</returns>
        public static bool VerifyByMD5(string input,
            string md5Hash,
            Encoding encoding = null,
            bool upperCase = true,
            bool includeHyphen = false,
            string salt = "")
        {
            string computedHash = EncryptByMD5(input, encoding, upperCase, includeHyphen, salt);
            return string.Equals(computedHash, md5Hash, StringComparison.Ordinal);
        }


        /// <summary>
        /// 计算输入文本的SHA系列哈希值
        /// </summary>
        /// <param name="input">待加密的原始文本</param>
        /// <param name="algorithm">哈希算法类型（SHA1/SHA256/SHA512）</param>
        /// <param name="encoding">文本编码方式，默认使用UTF8</param>
        /// <param name="upperCase">是否返回大写形式的哈希值，默认是</param>
        /// <param name="includeHyphen">是否在哈希值中包含连字符，默认否</param>
        /// <param name="salt">盐值，会附加到输入文本后增加安全性</param>
        /// <returns>计算得到的SHA哈希值</returns>
        public static string EncryptBySHA(string input,
            SHAAlgorithmType algorithm = SHAAlgorithmType.SHA256,
            Encoding encoding = null,
            bool upperCase = true,
            bool includeHyphen = false,
            string salt = "")
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // 设置默认编码
            if (encoding == null)
                encoding = Encoding.UTF8;

            // 拼接盐值
            string saltedInput = input + salt;

            // 创建哈希算法实例
            using (HashAlgorithm hashAlgorithm = GetHashAlgorithm(algorithm))
            {
                byte[] inputBytes = encoding.GetBytes(saltedInput);
                byte[] hashBytes = hashAlgorithm.ComputeHash(inputBytes);

                // 转换为十六进制字符串
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                    if (includeHyphen && i < hashBytes.Length - 1)
                        sb.Append("-");
                }

                string result = sb.ToString();
                return upperCase ? result.ToUpper() : result;
            }
        }

        /// <summary>
        /// 验证输入文本与目标SHA哈希值是否匹配
        /// </summary>
        /// <param name="input">待验证的原始文本</param>
        /// <param name="hashValue">目标SHA哈希值</param>
        /// <param name="algorithm">哈希算法类型（SHA1/SHA256/SHA512）</param>
        /// <param name="encoding">文本编码方式，默认使用UTF8</param>
        /// <param name="upperCase">哈希值是否为大写形式，默认是</param>
        /// <param name="includeHyphen">哈希值中是否包含连字符，默认否</param>
        /// <param name="salt">加密时使用的盐值</param>
        /// <returns>如果匹配返回true，否则返回false</returns>
        public static bool VerifyBySHA(string input,
            string hashValue,
            SHAAlgorithmType algorithm = SHAAlgorithmType.SHA256,
            Encoding encoding = null,
            bool upperCase = true,
            bool includeHyphen = false,
            string salt = "")
        {
            string computedHash = EncryptBySHA(input, algorithm, encoding, upperCase, includeHyphen, salt);
            return string.Equals(computedHash, hashValue, StringComparison.Ordinal);
        }

        /// <summary>
        /// 根据算法类型获取哈希算法实例
        /// </summary>
        private static HashAlgorithm GetHashAlgorithm(SHAAlgorithmType algorithm)
        {
            switch (algorithm)
            {
                case SHAAlgorithmType.SHA1:
                    return SHA1.Create();
                case SHAAlgorithmType.SHA256:
                    return SHA256.Create();
                case SHAAlgorithmType.SHA512:
                    return SHA512.Create();
                default:
                    throw new ArgumentException("不支持的哈希算法类型");
            }
        }

        /// <summary>
        /// SHA算法类型枚举
        /// </summary>
        public enum SHAAlgorithmType
        {
            SHA1,
            SHA256,
            SHA512
        }

        // CRC32标准多项式 (IEEE 802.3)
        private const uint DefaultPolynomial = 0xEDB88320;
        private const uint DefaultSeed = 0xFFFFFFFF;

        // 静态CRC32表，避免每次都重新计算
        private static readonly uint[] CrcTable = InitializeTable(DefaultPolynomial);

        /// <summary>
        /// 计算输入数据的CRC32校验值
        /// </summary>
        /// <param name="input">待计算CRC32的原始数据</param>
        /// <param name="encoding">文本编码方式，默认使用UTF8</param>
        /// <param name="reverseBits">是否反转计算结果的位顺序</param>
        /// <param name="finalXor">最终异或值，用于结果混淆</param>
        /// <param name="initialValue">初始CRC值</param>
        /// <param name="reflectInput">是否反转输入字节的位顺序</param>
        /// <param name="outputHex">是否以十六进制字符串形式输出</param>
        /// <param name="upperCase">是否返回大写形式的十六进制字符串</param>
        /// <returns>计算得到的CRC32值（字符串或数值形式）</returns>
        public static object ComputeCRC32(
            string input,
            Encoding encoding = null,
            bool reverseBits = true,
            uint finalXor = 0xFFFFFFFF,
            uint initialValue = DefaultSeed,
            bool reflectInput = true,
            bool outputHex = true,
            bool upperCase = true)
        {
            if (string.IsNullOrEmpty(input))
                return outputHex ? string.Empty : 0u;

            // 设置默认编码
            if (encoding == null)
                encoding = Encoding.UTF8;

            byte[] bytes = encoding.GetBytes(input);
            uint crc = initialValue;

            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = reflectInput ? Reflect8(bytes[i]) : bytes[i];
                crc = crc >> 8 ^ CrcTable[(crc ^ b) & 0xFF];
            }

            uint result = reverseBits ? Reflect32(crc) ^ finalXor : crc ^ finalXor;

            if (outputHex)
            {
                string hex = result.ToString("X8");
                return upperCase ? hex : hex.ToLower();
            }

            return result;
        }

        /// <summary>
        /// 验证输入数据与目标CRC32值是否匹配
        /// </summary>
        /// <param name="input">待验证的原始数据</param>
        /// <param name="crc32Value">目标CRC32值（字符串或数值形式）</param>
        /// <param name="encoding">文本编码方式，默认使用UTF8</param>
        /// <param name="reverseBits">是否反转计算结果的位顺序</param>
        /// <param name="finalXor">最终异或值，用于结果混淆</param>
        /// <param name="initialValue">初始CRC值</param>
        /// <param name="reflectInput">是否反转输入字节的位顺序</param>
        /// <param name="isHex">目标CRC32值是否为十六进制字符串</param>
        /// <returns>如果匹配返回true，否则返回false</returns>
        public static bool VerifyCRC32(
            string input,
            object crc32Value,
            Encoding encoding = null,
            bool reverseBits = true,
            uint finalXor = 0xFFFFFFFF,
            uint initialValue = DefaultSeed,
            bool reflectInput = true,
            bool isHex = true)
        {
            object computedCrc = ComputeCRC32(
                input, encoding, reverseBits, finalXor, initialValue, reflectInput, isHex);

            if (isHex)
            {
                return string.Equals(computedCrc.ToString(), crc32Value.ToString(), StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                return Convert.ToUInt32(computedCrc) == Convert.ToUInt32(crc32Value);
            }
        }

        // 初始化CRC表
        private static uint[] InitializeTable(uint polynomial)
        {
            uint[] table = new uint[256];
            for (uint i = 0; i < 256; i++)
            {
                uint entry = i;
                for (int j = 0; j < 8; j++)
                {
                    if ((entry & 1) == 1)
                        entry = entry >> 1 ^ polynomial;
                    else
                        entry >>= 1;
                }
                table[i] = entry;
            }
            return table;
        }

        // 反转32位值的位顺序
        private static uint Reflect32(uint value)
        {
            uint result = 0;
            for (int i = 0; i < 32; i++)
            {
                if ((value & 1u << i) != 0)
                    result |= 1u << 31 - i;
            }
            return result;
        }

        // 反转8位值的位顺序
        private static byte Reflect8(byte value)
        {
            byte result = 0;
            for (int i = 0; i < 8; i++)
            {
                if ((value & 1 << i) != 0)
                    result |= (byte)(1 << 7 - i);
            }
            return result;
        }

    }
}
