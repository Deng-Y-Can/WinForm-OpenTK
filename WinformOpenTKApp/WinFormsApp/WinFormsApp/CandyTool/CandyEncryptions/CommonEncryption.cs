using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.CandyTool.CandyEncryptions
{
    public class CommonEncryption
    {
        // CRC16 加密方法
        public static byte[] CalculateCrc16(
            byte[] data,                 // 输入的待加密数据
            ushort polynomial = 0xA001,  // 多项式值，默认是 CRC-16-IBM
            ushort initialValue = 0xFFFF,// 初始值，默认是 0xFFFF
            ushort finalXorValue = 0x0000, // 结果异或值，默认是 0x0000
            bool reflectInput = true,    // 是否反转输入数据，默认是
            bool reflectOutput = true    // 是否反转输出结果，默认是
        )
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            // 预计算 CRC 表
            ushort[] table = new ushort[256];
            for (ushort i = 0; i < table.Length; ++i)
            {
                ushort value = 0;
                ushort temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)(value >> 1 ^ polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                table[i] = value;
            }

            // 计算 CRC
            ushort crc = initialValue;
            foreach (byte b in data)
            {
                byte index;
                if (reflectInput)
                {
                    index = (byte)(crc ^ ReflectBits(b, 8));
                }
                else
                {
                    index = (byte)(crc ^ b);
                }

                crc = (ushort)(crc >> 8 ^ table[index]);
            }

            // 应用最终异或值
            if (reflectOutput)
            {
                crc = ReflectBits(crc, 16);
            }
            crc ^= finalXorValue;

            // 转换为字节数组
            return new byte[] { (byte)(crc & 0xFF), (byte)(crc >> 8) };
        }

        // 辅助方法：反转位
        private static ushort ReflectBits(ushort value, int bitCount)
        {
            ushort result = 0;
            for (int i = 0; i < bitCount; ++i)
            {
                if ((value & 1 << i) != 0)
                {
                    result |= (ushort)(1 << bitCount - 1 - i);
                }
            }
            return result;
        }

        // CRC16 验证方法（解码）
        public static bool VerifyCrc16(
            byte[] data,                 // 输入的待验证数据（包含CRC）
            int dataLength,              // 数据部分的长度（不包含CRC）
            ushort polynomial = 0xA001,  // 多项式值，必须与加密时相同
            ushort initialValue = 0xFFFF,// 初始值，必须与加密时相同
            ushort finalXorValue = 0x0000, // 结果异或值，必须与加密时相同
            bool reflectInput = true,    // 是否反转输入数据，必须与加密时相同
            bool reflectOutput = true    // 是否反转输出结果，必须与加密时相同
        )
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (data.Length < dataLength + 2)
                throw new ArgumentException("数据长度不足，无法包含CRC校验值。");

            // 提取原始数据和CRC校验值
            byte[] dataWithoutCrc = new byte[dataLength];
            Array.Copy(data, 0, dataWithoutCrc, 0, dataLength);

            byte[] receivedCrc = new byte[2];
            Array.Copy(data, dataLength, receivedCrc, 0, 2);

            // 计算数据的CRC
            byte[] calculatedCrc = CalculateCrc16(
                dataWithoutCrc,
                polynomial,
                initialValue,
                finalXorValue,
                reflectInput,
                reflectOutput
            );

            // 比较计算得到的CRC和接收到的CRC
            return calculatedCrc[0] == receivedCrc[0] && calculatedCrc[1] == receivedCrc[1];
        }


        /// <summary>
        /// 计算CRC8校验值
        /// </summary>
        /// <param name="data">待计算的数据字节数组</param>
        /// <param name="polynomial">CRC多项式（8位，最高位默认1，实际传入7位值）</param>
        /// <param name="initialValue">初始校验值</param>
        /// <param name="finalXor">最终异或值</param>
        /// <param name="reflectInput">是否反转输入字节</param>
        /// <param name="reflectOutput">是否反转输出结果</param>
        /// <param name="checkRange">是否校验数据范围（true时忽略0x00和0xFF）</param>
        /// <returns>计算得到的CRC8字节</returns>
        public static byte CalculateCrc8(
            byte[] data,
            byte polynomial = 0x07,
            byte initialValue = 0x00,
            byte finalXor = 0x00,
            bool reflectInput = false,
            bool reflectOutput = false,
            bool checkRange = false
        )
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            // 预计算CRC表
            byte[] crcTable = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                byte temp = (byte)i;
                if (reflectInput)
                    temp = ReflectByte(temp);

                byte crc = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (((crc ^ temp) & 0x80) != 0)
                    {
                        crc = (byte)(crc << 1 ^ polynomial);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                    temp <<= 1;
                }

                if (reflectInput)
                    crc = ReflectByte(crc);

                crcTable[i] = crc;
            }

            // 计算CRC
            byte crcValue = initialValue;
            foreach (byte b in data)
            {
                // 应用数据范围检查
                if (checkRange && (b == 0x00 || b == 0xFF))
                    continue;

                byte input = reflectInput ? ReflectByte(b) : b;
                crcValue = crcTable[crcValue ^ input];
            }

            // 处理输出
            if (reflectOutput)
                crcValue = ReflectByte(crcValue);

            return (byte)(crcValue ^ finalXor);
        }

        /// <summary>
        /// 验证包含CRC8校验值的数据
        /// </summary>
        /// <param name="dataWithCrc">包含原始数据和CRC8的字节数组</param>
        /// <param name="dataLength">原始数据长度（不包含CRC字节）</param>
        /// <param name="polynomial">CRC多项式（需与加密时一致）</param>
        /// <param name="initialValue">初始校验值（需与加密时一致）</param>
        /// <param name="finalXor">最终异或值（需与加密时一致）</param>
        /// <param name="reflectInput">是否反转输入字节（需与加密时一致）</param>
        /// <param name="reflectOutput">是否反转输出结果（需与加密时一致）</param>
        /// <param name="checkRange">是否校验数据范围（需与加密时一致）</param>
        /// <returns>验证结果（true=通过，false=失败）</returns>
        public static bool VerifyCrc8(
            byte[] dataWithCrc,
            int dataLength,
            byte polynomial = 0x07,
            byte initialValue = 0x00,
            byte finalXor = 0x00,
            bool reflectInput = false,
            bool reflectOutput = false,
            bool checkRange = false
        )
        {
            if (dataWithCrc == null)
                throw new ArgumentNullException(nameof(dataWithCrc));

            if (dataLength < 0 || dataLength + 1 > dataWithCrc.Length)
                throw new ArgumentOutOfRangeException(nameof(dataLength), "无效的数据长度");

            // 提取原始数据
            byte[] data = new byte[dataLength];
            Array.Copy(dataWithCrc, 0, data, 0, dataLength);

            // 提取接收到的CRC
            byte receivedCrc = dataWithCrc[dataLength];

            // 计算CRC并验证
            byte calculatedCrc = CalculateCrc8(
                data,
                polynomial,
                initialValue,
                finalXor,
                reflectInput,
                reflectOutput,
                checkRange
            );

            return receivedCrc == calculatedCrc;
        }

        /// <summary>
        /// 反转字节的8位顺序
        /// </summary>
        private static byte ReflectByte(byte value)
        {
            byte result = 0;
            for (int i = 0; i < 8; i++)
            {
                result = (byte)(result << 1 | value & 0x01);
                value >>= 1;
            }
            return result;
        }

    }
}
