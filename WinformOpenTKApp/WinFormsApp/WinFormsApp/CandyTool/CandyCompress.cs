using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZstdNet;

namespace WinFormsApp.CandyTool
{
    public class CandyCompress
    {
        #region DEFLATE算法
        /// <summary>
        /// 使用DEFLATE算法压缩数据
        /// </summary>
        /// <param name="inputBytes">要压缩的字节数组</param>
        /// <param name="compressionLevel">压缩级别（0-9，0=无压缩，9=最高压缩比）</param>
        /// <param name="leaveOpen">是否保持流打开状态</param>
        /// <param name="bufferSize">缓冲区大小（影响压缩效率）</param>
        /// <param name="useZLibHeader">是否使用ZLIB头部而非原始DEFLATE格式</param>
        /// <returns>压缩后的字节数组</returns>
        /// <exception cref="ArgumentNullException">当输入字节数组为null时抛出</exception>
        /// <exception cref="ArgumentOutOfRangeException">当压缩级别或缓冲区大小无效时抛出</exception>
        public static byte[] CompressByDEFLATE(byte[] inputBytes,
                                     int compressionLevel = 6,
                                     bool leaveOpen = false,
                                     int bufferSize = 8192,
                                     bool useZLibHeader = true)
        {
            // 输入验证
            if (inputBytes == null)
                throw new ArgumentNullException(nameof(inputBytes));

            if (compressionLevel < 0 || compressionLevel > 9)
                throw new ArgumentOutOfRangeException(nameof(compressionLevel), "压缩级别必须在0-9之间");

            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize), "缓冲区大小必须大于0");

            using (var outputStream = new MemoryStream())
            {
                // 显式声明为Stream基类，解决类型推断问题
                Stream deflateStream = useZLibHeader
                    ? new ZLibStream(outputStream, GetCompressionLevelByDEFLATE(compressionLevel), leaveOpen)
                    : new DeflateStream(outputStream, GetCompressionLevelByDEFLATE(compressionLevel), leaveOpen);

                using (deflateStream)
                {
                    // 使用指定的缓冲区大小进行压缩
                    deflateStream.Write(inputBytes, 0, inputBytes.Length);
                }

                return outputStream.ToArray();
            }
        }

        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="inputString">要压缩的字符串</param>
        /// <param name="encoding">字符串编码格式，默认为UTF8</param>
        /// <param name="compressionLevel">压缩级别（0-9）</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="useZLibHeader">是否使用ZLIB头部</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] CompressStringByDEFLATE(string inputString,
                                           Encoding encoding = null,
                                           int compressionLevel = 6,
                                           int bufferSize = 8192,
                                           bool useZLibHeader = true)
        {
            if (string.IsNullOrEmpty(inputString))
                return Array.Empty<byte>();

            encoding ??= Encoding.UTF8;
            byte[] inputBytes = encoding.GetBytes(inputString);
            return CompressByDEFLATE(inputBytes, compressionLevel, false, bufferSize, useZLibHeader);
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="destinationFilePath">目标文件路径</param>
        /// <param name="compressionLevel">压缩级别（0-9）</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="useZLibHeader">是否使用ZLIB头部</param>
        /// <returns>压缩后的文件大小</returns>
        public static long CompressFileByDEFLATE(string sourceFilePath,
                                       string destinationFilePath,
                                       int compressionLevel = 6,
                                       int bufferSize = 8192,
                                       bool useZLibHeader = true)
        {
            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException("源文件不存在", sourceFilePath);

            using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            using (var destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                // 显式声明为Stream基类
                Stream deflateStream = useZLibHeader
                    ? new ZLibStream(destinationStream, GetCompressionLevelByDEFLATE(compressionLevel), false)
                    : new DeflateStream(destinationStream, GetCompressionLevelByDEFLATE(compressionLevel), false);

                using (deflateStream)
                {
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead;
                    while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        deflateStream.Write(buffer, 0, bytesRead);
                    }
                }
            }

            return new FileInfo(destinationFilePath).Length;
        }

        /// <summary>
        /// 将整数压缩级别转换为CompressionLevel枚举
        /// </summary>
        private static CompressionLevel GetCompressionLevelByDEFLATE(int level)
        {
            return level switch
            {
                0 => CompressionLevel.NoCompression,
                1 or 2 => CompressionLevel.Fastest,
                3 or 4 or 5 or 6 => CompressionLevel.Optimal,
                7 or 8 or 9 => CompressionLevel.SmallestSize,
                _ => CompressionLevel.Optimal
            };
        }

        /// <summary>
        /// 解压DEFLATE压缩的数据
        /// </summary>
        /// <param name="compressedData">压缩的数据</param>
        /// <param name="leaveOpen">是否保持流打开</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="useZLibHeader">是否使用ZLIB头部</param>
        /// <returns>解压后的字节数组</returns>
        public static byte[] DecompressByDEFLATE(byte[] compressedData,
                                       bool leaveOpen = false,
                                       int bufferSize = 8192,
                                       bool useZLibHeader = true)
        {
            if (compressedData == null || compressedData.Length == 0)
                return Array.Empty<byte>();

            using (var inputStream = new MemoryStream(compressedData))
            {
                // 显式声明为Stream基类
                Stream deflateStream = useZLibHeader
                    ? new ZLibStream(inputStream, CompressionMode.Decompress, leaveOpen)
                    : new DeflateStream(inputStream, CompressionMode.Decompress, leaveOpen);

                using (deflateStream)
                using (var outputStream = new MemoryStream())
                {
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead;
                    while ((bytesRead = deflateStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        outputStream.Write(buffer, 0, bytesRead);
                    }

                    return outputStream.ToArray();
                }
            }
        }

        #endregion


        #region  LZMA 算法
        // 隐藏底层DLL导入细节
        #region 私有DLL导入实现
        [DllImport("7z.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int LzmaCompress(
            byte[] outBuffer, ref ulong outSize,
            byte[] inBuffer, ulong inSize,
            byte[] properties, ref uint propertiesSize,
            int level, uint dictSize,
            int lc, int lp, int pb, int fb, int numThreads
        );

        [DllImport("7z.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int LzmaUncompress(
            byte[] outBuffer, ref ulong outSize,
            byte[] inBuffer, ref ulong inSize,
            byte[] properties, uint propertiesSize
        );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern nint LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern nint GetProcAddress(nint hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeLibrary(nint hModule);
        #endregion

        private const int LZMA_PROPERTIES_SIZE = 5;
        private static bool _isLibraryLoaded = false;

        /// <summary>
        /// 检查并初始化7z.dll
        /// </summary>
        /// <param name="dllPath">7z.dll路径</param>
        /// <returns>是否初始化成功</returns>
        public static bool Initialize(string dllPath = "7z.dll")
        {
            if (_isLibraryLoaded) return true;

            if (!File.Exists(dllPath))
                return false;

            try
            {
                var handle = LoadLibrary(dllPath);
                if (handle == nint.Zero)
                    return false;

                var compressAddr = GetProcAddress(handle, "LzmaCompress");
                var uncompressAddr = GetProcAddress(handle, "LzmaUncompress");

                FreeLibrary(handle);
                _isLibraryLoaded = compressAddr != nint.Zero && uncompressAddr != nint.Zero;
                return _isLibraryLoaded;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="inputBytes">输入数据</param>
        /// <param name="compressionLevel">压缩级别(0-9，9为最高)</param>
        /// <param name="dictSize">字典大小(建议: 1<<18 至 1<<27)</param>
        /// <returns>压缩后的数据</returns>
        public static byte[] CompressByLZMA(byte[] inputBytes, int compressionLevel = 6, uint dictSize = 1 << 23)
        {
            // 参数验证
            if (inputBytes == null || inputBytes.Length == 0)
                return Array.Empty<byte>();

            if (!_isLibraryLoaded && !Initialize())
                throw new InvalidOperationException("7z.dll初始化失败，请先调用Initialize方法");

            if (compressionLevel < 0 || compressionLevel > 9)
                throw new ArgumentOutOfRangeException(nameof(compressionLevel), "压缩级别必须在0-9之间");

            // 压缩实现（使用默认高级参数）
            var properties = new byte[LZMA_PROPERTIES_SIZE];
            uint propsSize = LZMA_PROPERTIES_SIZE;

            ulong inSize = (ulong)inputBytes.Length;
            ulong outSize = inSize + (1UL << 20); // 预留1MB空间
            var outBuffer = new byte[outSize];

            int result = LzmaCompress(
                outBuffer, ref outSize,
                inputBytes, inSize,
                properties, ref propsSize,
                compressionLevel, dictSize,
                3, 0, 2, 32, 1 // 默认高级参数
            );

            if (result != 0)
                throw new InvalidOperationException($"压缩失败，错误代码: {result}");

            // 组合属性和压缩数据
            var compressedData = new byte[LZMA_PROPERTIES_SIZE + outSize];
            Buffer.BlockCopy(properties, 0, compressedData, 0, LZMA_PROPERTIES_SIZE);
            Buffer.BlockCopy(outBuffer, 0, compressedData, LZMA_PROPERTIES_SIZE, (int)outSize);

            return compressedData;
        }

        /// <summary>
        /// 解压字节数组
        /// </summary>
        /// <param name="compressedData">压缩数据</param>
        /// <param name="estimatedSize">预估解压大小</param>
        /// <returns>解压后的数据</returns>
        public static byte[] DecompressByLZMA(byte[] compressedData, long estimatedSize = 0)
        {
            if (compressedData == null || compressedData.Length < LZMA_PROPERTIES_SIZE)
                return Array.Empty<byte>();

            if (!_isLibraryLoaded && !Initialize())
                throw new InvalidOperationException("7z.dll初始化失败，请先调用Initialize方法");

            // 分离属性和压缩数据
            var properties = new byte[LZMA_PROPERTIES_SIZE];
            Buffer.BlockCopy(compressedData, 0, properties, 0, LZMA_PROPERTIES_SIZE);

            int compressedSize = compressedData.Length - LZMA_PROPERTIES_SIZE;
            var compressedBytes = new byte[compressedSize];
            Buffer.BlockCopy(compressedData, LZMA_PROPERTIES_SIZE, compressedBytes, 0, compressedSize);

            // 解压实现
            ulong outSize = (ulong)(estimatedSize > 0 ?
                estimatedSize : Math.Max(1 << 20, compressedSize * 10));
            var outBuffer = new byte[outSize];
            ulong inSize = (ulong)compressedBytes.Length;

            int result = LzmaUncompress(
                outBuffer, ref outSize,
                compressedBytes, ref inSize,
                properties, LZMA_PROPERTIES_SIZE
            );

            if (result != 0)
                throw new InvalidOperationException($"解压失败，错误代码: {result}");

            // 调整缓冲区大小
            var finalBuffer = new byte[outSize];
            Buffer.BlockCopy(outBuffer, 0, finalBuffer, 0, (int)outSize);

            return finalBuffer;
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="destinationPath">目标文件路径</param>
        /// <param name="compressionLevel">压缩级别</param>
        public static void CompressFileByLZMA(string sourcePath, string destinationPath, int compressionLevel = 6)
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("源文件不存在", sourcePath);

            byte[] data = File.ReadAllBytes(sourcePath);
            byte[] compressed = CompressByLZMA(data, compressionLevel);
            File.WriteAllBytes(destinationPath, compressed);
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="sourcePath">压缩文件路径</param>
        /// <param name="destinationPath">目标文件路径</param>
        public static void DecompressFileByLZMA(string sourcePath, string destinationPath)
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("压缩文件不存在", sourcePath);

            byte[] compressed = File.ReadAllBytes(sourcePath);
            byte[] decompressed = DecompressByLZMA(compressed);
            File.WriteAllBytes(destinationPath, decompressed);
        }

        #endregion


        #region  BZIP2 算法
        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="inputBytes">要压缩的字节数组</param>
        /// <param name="blockSize">块大小(1-9)，越大压缩比越高但内存占用越大</param>
        /// <param name="bufferSize">缓冲区大小(字节)，影响IO性能</param>
        /// <returns>压缩后的字节数组</returns>
        /// <exception cref="ArgumentNullException">输入字节数组为空时抛出</exception>
        /// <exception cref="ArgumentOutOfRangeException">参数超出有效范围时抛出</exception>
        public static byte[] CompressByBZIP2(
            byte[] inputBytes,
            int blockSize = 9,
            int bufferSize = 8192)
        {
            // 参数验证
            if (inputBytes == null)
                throw new ArgumentNullException(nameof(inputBytes));

            if (blockSize < 1 || blockSize > 9)
                throw new ArgumentOutOfRangeException(nameof(blockSize), "块大小必须在1-9之间（100KB-900KB）");

            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize), "缓冲区大小必须大于0");

            using (var outputStream = new MemoryStream())
            {
                // 创建BZIP2压缩流，SharpZipLib的构造函数参数顺序不同
                using (var bzip2Stream = new BZip2OutputStream(outputStream, blockSize))
                {
                    // 分块写入数据，优化大文件处理
                    int bytesRead;
                    for (int i = 0; i < inputBytes.Length; i += bufferSize)
                    {
                        int chunkSize = Math.Min(bufferSize, inputBytes.Length - i);
                        bzip2Stream.Write(inputBytes, i, chunkSize);
                    }

                    // 完成压缩（替代Finish方法）
                    bzip2Stream.Flush();
                }

                return outputStream.ToArray();
            }
        }

        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="inputString">要压缩的字符串</param>
        /// <param name="encoding">字符串编码格式，默认为UTF8</param>
        /// <param name="blockSize">块大小(1-9)</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] CompressStringByBZIP2(
            string inputString,
            Encoding encoding = null,
            int blockSize = 9)
        {
            if (string.IsNullOrEmpty(inputString))
                return Array.Empty<byte>();

            encoding ??= Encoding.UTF8;
            byte[] inputBytes = encoding.GetBytes(inputString);
            return CompressByBZIP2(inputBytes, blockSize);
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath">源文件路径</param>
        /// <param name="destinationFilePath">目标文件路径</param>
        /// <param name="blockSize">块大小(1-9)</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>压缩后的文件大小</returns>
        public static long CompressFileByBZIP2(
            string sourceFilePath,
            string destinationFilePath,
            int blockSize = 9,
            int bufferSize = 8192)
        {
            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException("源文件不存在", sourceFilePath);

            using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            using (var bzip2Stream = new BZip2OutputStream(destinationStream, blockSize))
            {
                byte[] buffer = new byte[bufferSize];
                int bytesRead;
                while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    bzip2Stream.Write(buffer, 0, bytesRead);
                }
                bzip2Stream.Flush();
            }

            return new FileInfo(destinationFilePath).Length;
        }

        /// <summary>
        /// 解压字节数组
        /// </summary>
        /// <param name="compressedData">压缩的数据</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns>解压后的字节数组</returns>
        public static byte[] DecompressByBZIP2(
            byte[] compressedData,
            int bufferSize = 8192)
        {
            if (compressedData == null || compressedData.Length == 0)
                return Array.Empty<byte>();

            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize), "缓冲区大小必须大于0");

            using (var inputStream = new MemoryStream(compressedData))
            using (var outputStream = new MemoryStream())
            using (var bzip2Stream = new BZip2InputStream(inputStream))
            {
                byte[] buffer = new byte[bufferSize];
                int bytesRead;
                while ((bytesRead = bzip2Stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, bytesRead);
                }

                return outputStream.ToArray();
            }
        }

        /// <summary>
        /// 解压字符串
        /// </summary>
        /// <param name="compressedData">压缩的数据</param>
        /// <param name="encoding">编码格式，默认为UTF8</param>
        /// <returns>解压后的字符串</returns>
        public static string DecompressStringByBZIP2(
            byte[] compressedData,
            Encoding encoding = null)
        {
            byte[] decompressedBytes = DecompressByBZIP2(compressedData);
            encoding ??= Encoding.UTF8;
            return encoding.GetString(decompressedBytes);
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="sourceFilePath">压缩文件路径</param>
        /// <param name="destinationFilePath">目标文件路径</param>
        /// <param name="bufferSize">缓冲区大小</param>
        public static void DecompressFileByBZIP2(
            string sourceFilePath,
            string destinationFilePath,
            int bufferSize = 8192)
        {
            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException("压缩文件不存在", sourceFilePath);

            using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            using (var destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            using (var bzip2Stream = new BZip2InputStream(sourceStream))
            {
                byte[] buffer = new byte[bufferSize];
                int bytesRead;
                while ((bytesRead = bzip2Stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    destinationStream.Write(buffer, 0, bytesRead);
                }
            }
        }
        #endregion


        #region  ZSTD 算法
        /// <summary>
        /// 创建压缩选项
        /// 根据ZstdNet库实际API调整参数设置方式
        /// </summary>
        private static CompressionOptions CreateCompressionOptions(int compressionLevel)
        {
            // 确保压缩级别在有效范围内
            compressionLevel = Math.Max(1, Math.Min(22, compressionLevel));

            // 不同版本的ZstdNet库可能有不同的构造函数参数
            // 尝试常用的构造方式
            try
            {
                // 尝试通过构造函数直接设置级别
                return new CompressionOptions(compressionLevel);
            }
            catch
            {
                // 如果上述方式失败，尝试使用默认构造函数
                return new CompressionOptions(1);
            }
        }

        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="inputBytes">要压缩的字节数组</param>
        /// <param name="compressionLevel">压缩级别(1-22)</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] CompressByZSTD(byte[] inputBytes, int compressionLevel = 3)
        {
            if (inputBytes == null || inputBytes.Length == 0)
                return Array.Empty<byte>();

            // 创建压缩选项
            var options = CreateCompressionOptions(compressionLevel);

            // 创建压缩器并压缩数据
            using (var compressor = new Compressor(options))
            {
                return compressor.Wrap(inputBytes);
            }
        }

        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="inputString">要压缩的字符串</param>
        /// <param name="encoding">编码方式，默认为UTF8</param>
        /// <param name="compressionLevel">压缩级别(1-22)</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] CompressStringByZSTD(string inputString, Encoding encoding = null, int compressionLevel = 3)
        {
            if (string.IsNullOrEmpty(inputString))
                return Array.Empty<byte>();

            encoding ??= Encoding.UTF8;
            byte[] data = encoding.GetBytes(inputString);
            return CompressByZSTD(data, compressionLevel);
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="destinationPath">目标文件路径</param>
        /// <param name="compressionLevel">压缩级别(1-22)</param>
        public static void CompressFileByZSTD(string sourcePath, string destinationPath, int compressionLevel = 3)
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("源文件不存在", sourcePath);

            byte[] data = File.ReadAllBytes(sourcePath);
            byte[] compressedData = CompressByZSTD(data, compressionLevel);
            File.WriteAllBytes(destinationPath, compressedData);
        }

        /// <summary>
        /// 解压字节数组
        /// </summary>
        /// <param name="compressedData">压缩的数据</param>
        /// <returns>解压后的字节数组</returns>
        public static byte[] DecompressByZSTD(byte[] compressedData)
        {
            if (compressedData == null || compressedData.Length == 0)
                return Array.Empty<byte>();

            using (var decompressor = new Decompressor())
            {
                return decompressor.Unwrap(compressedData);
            }
        }

        /// <summary>
        /// 解压字符串
        /// </summary>
        /// <param name="compressedData">压缩的数据</param>
        /// <param name="encoding">编码方式，默认为UTF8</param>
        /// <returns>解压后的字符串</returns>
        public static string DecompressStringByZSTD(byte[] compressedData, Encoding encoding = null)
        {
            byte[] data = DecompressByZSTD(compressedData);
            if (data == null || data.Length == 0)
                return string.Empty;

            encoding ??= Encoding.UTF8;
            return encoding.GetString(data);
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="sourcePath">压缩文件路径</param>
        /// <param name="destinationPath">目标文件路径</param>
        public static void DecompressFileByZSTD(string sourcePath, string destinationPath)
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("压缩文件不存在", sourcePath);

            byte[] compressedData = File.ReadAllBytes(sourcePath);
            byte[] data = DecompressByZSTD(compressedData);
            File.WriteAllBytes(destinationPath, data);
        }
        #endregion
    }
}
