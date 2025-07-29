using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.CandyTool.CandyEncryptions;

namespace WinFormsApp.WindowsTool
{
    public partial class EncryptionTool : Form
    {
        public EncryptionTool()
        {
            InitializeComponent();
            InitializeAlgorithmTypes();
        }

        private void InitializeAlgorithmTypes()
        {
            // 填充算法类型下拉框
            cmbAlgorithmType.Items.AddRange(new[] {
                "对称加密",
                "非对称加密",
                "哈希加密",
                "通用加密"
            });
            cmbAlgorithmType.SelectedIndex = 0;
        }

        private void cmbAlgorithmType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 清空现有控件
            pnlParameters.Controls.Clear();
            pnlActions.Controls.Clear();
            pnlParameters.RowStyles.Clear();
            pnlParameters.RowCount = 0;

            switch (cmbAlgorithmType.SelectedItem.ToString())
            {
                case "对称加密":
                    InitializeSymmetricEncryptionUI();
                    break;
                case "非对称加密":
                    InitializeAsymmetricEncryptionUI();
                    break;
                case "哈希加密":
                    InitializeHashEncryptionUI();
                    break;
                case "通用加密":
                    InitializeCommonEncryptionUI();
                    break;
            }
        }

        #region 对称加密相关UI和事件
        private void InitializeSymmetricEncryptionUI()
        {
            // 算法选择
            AddLabelAndComboBox("算法:", "cmbSymmetricAlgorithm", new[] { "DES", "3DES", "AES" }, 0);

            // 通用参数
            AddLabelAndTextBox("明文:", "txtPlainText", 1);
            AddLabelAndTextBox("密文:", "txtCipherText", 2);
            AddLabelAndTextBox("密钥:", "txtKey", 3);
            AddLabelAndTextBox("初始化向量(IV):", "txtIV", 4);

            // AES特有参数
            AddLabelAndComboBox("密钥长度:", "cmbKeySize", new[] { "128", "192", "256" }, 5);

            // 模式选择
            AddLabelAndComboBox("加密模式:", "cmbCipherMode", Enum.GetNames(typeof(CipherMode)), 6);
            AddLabelAndComboBox("填充模式:", "cmbPaddingMode", Enum.GetNames(typeof(PaddingMode)), 7);

            // 按钮
            AddButton("加密", "btnEncrypt", BtnEncrypt_Click);
            AddButton("解密", "btnDecrypt", BtnDecrypt_Click);
        }

        private void BtnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string algorithm = GetControlValue("cmbSymmetricAlgorithm") as string;
                string plainText = GetControlValue("txtPlainText") as string;
                string key = GetControlValue("txtKey") as string;
                string iv = GetControlValue("txtIV") as string;
                int keySize = int.Parse(GetControlValue("cmbKeySize") as string);
                CipherMode cipherMode = (CipherMode)Enum.Parse(typeof(CipherMode), GetControlValue("cmbCipherMode") as string);
                PaddingMode paddingMode = (PaddingMode)Enum.Parse(typeof(PaddingMode), GetControlValue("cmbPaddingMode") as string);

                string result = "";
                switch (algorithm)
                {
                    case "DES":
                        result = SymmetricEncryption.EncryptByDES(plainText, key, iv, cipherMode, paddingMode);
                        break;
                    case "3DES":
                        result = SymmetricEncryption.EncryptByTripleDES(plainText, key, iv, cipherMode, paddingMode);
                        break;
                    case "AES":
                        result = SymmetricEncryption.EncryptByAES(plainText, key, iv, keySize, cipherMode, paddingMode);
                        break;
                }

                SetControlValue("txtCipherText", result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加密失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string algorithm = GetControlValue("cmbSymmetricAlgorithm") as string;
                string cipherText = GetControlValue("txtCipherText") as string;
                string key = GetControlValue("txtKey") as string;
                string iv = GetControlValue("txtIV") as string;
                int keySize = int.Parse(GetControlValue("cmbKeySize") as string);
                CipherMode cipherMode = (CipherMode)Enum.Parse(typeof(CipherMode), GetControlValue("cmbCipherMode") as string);
                PaddingMode paddingMode = (PaddingMode)Enum.Parse(typeof(PaddingMode), GetControlValue("cmbPaddingMode") as string);

                string result = "";
                switch (algorithm)
                {
                    case "DES":
                        result = SymmetricEncryption.DecryptByDES(cipherText, key, iv, cipherMode, paddingMode);
                        break;
                    case "3DES":
                        result = SymmetricEncryption.DecryptByTripleDES(cipherText, key, iv, cipherMode, paddingMode);
                        break;
                    case "AES":
                        result = SymmetricEncryption.DecryptByAES(cipherText, key, iv, keySize, cipherMode, paddingMode);
                        break;
                }

                SetControlValue("txtPlainText", result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"解密失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 非对称加密相关UI和事件
        private void InitializeAsymmetricEncryptionUI()
        {
            // 算法选择
            AddLabelAndComboBox("算法:", "cmbAsymmetricAlgorithm", new[] { "RSA", "ECC", "DSA" }, 0);

            // 通用参数
            AddLabelAndTextBox("明文:", "txtAsyPlainText", 1);
            AddLabelAndTextBox("密文/签名:", "txtAsyCipherText", 2);
            AddLabelAndTextBox("公钥:", "txtPublicKey", 3, true);
            AddLabelAndTextBox("私钥:", "txtPrivateKey", 4, true);

            // 特有参数
            AddLabelAndComboBox("密钥长度:", "cmbAsyKeySize", new[] { "1024", "2048", "4096" }, 5);
            AddLabelAndComboBox("椭圆曲线:", "cmbCurveName", new[] { "P-256", "P-384", "P-521" }, 6);
            AddLabelAndComboBox("哈希算法:", "cmbHashAlgorithm", new[] { "SHA1", "SHA256", "SHA384", "SHA512" }, 7);

            // 按钮
            AddButton("生成密钥对", "btnGenerateKeys", BtnGenerateKeys_Click);
            AddButton("加密", "btnAsyEncrypt", BtnAsyEncrypt_Click);
            AddButton("解密", "btnAsyDecrypt", BtnAsyDecrypt_Click);
            AddButton("签名", "btnSign", BtnSign_Click);
            AddButton("验证签名", "btnVerify", BtnVerify_Click);
        }

        private void BtnGenerateKeys_Click(object sender, EventArgs e)
        {
            try
            {
                string algorithm = GetControlValue("cmbAsymmetricAlgorithm") as string;
                int keySize = int.Parse(GetControlValue("cmbAsyKeySize") as string);
                string curveName = GetControlValue("cmbCurveName") as string;

                var (publicKey, privateKey) = ("", "");

                switch (algorithm)
                {
                    case "RSA":
                        (publicKey, privateKey) = AsymmetricEncryption.GenerateKeyPairByRSA(keySize);
                        break;
                    case "ECC":
                        (publicKey, privateKey) = AsymmetricEncryption.GenerateKeyPairByECC(curveName);
                        break;
                    case "DSA":
                        (publicKey, privateKey) = AsymmetricEncryption.GenerateKeyPairByDSA(keySize);
                        break;
                }

                SetControlValue("txtPublicKey", publicKey);
                SetControlValue("txtPrivateKey", privateKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"生成密钥失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAsyEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string algorithm = GetControlValue("cmbAsymmetricAlgorithm") as string;
                if (algorithm == "DSA")
                {
                    MessageBox.Show("DSA算法不支持加密功能，它用于数字签名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string plainText = GetControlValue("txtAsyPlainText") as string;
                string publicKey = GetControlValue("txtPublicKey") as string;
                int keySize = int.Parse(GetControlValue("cmbAsyKeySize") as string);

                string result = "";
                switch (algorithm)
                {
                    case "RSA":
                        result = AsymmetricEncryption.EncryptByRSA(plainText, publicKey, keySize);
                        break;
                    case "ECC":
                        // ECC加密在提供的类中是基于密钥交换的，这里简化处理
                        MessageBox.Show("ECC加密功能需要特殊处理，请参考相关文档", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }

                SetControlValue("txtAsyCipherText", result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加密失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAsyDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string algorithm = GetControlValue("cmbAsymmetricAlgorithm") as string;
                if (algorithm == "DSA")
                {
                    MessageBox.Show("DSA算法不支持解密功能，它用于数字签名验证", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string cipherText = GetControlValue("txtAsyCipherText") as string;
                string privateKey = GetControlValue("txtPrivateKey") as string;
                int keySize = int.Parse(GetControlValue("cmbAsyKeySize") as string);

                string result = "";
                switch (algorithm)
                {
                    case "RSA":
                        result = AsymmetricEncryption.DecryptByRSA(cipherText, privateKey, keySize);
                        break;
                    case "ECC":
                        // ECC解密在提供的类中是基于密钥交换的，这里简化处理
                        MessageBox.Show("ECC解密功能需要特殊处理，请参考相关文档", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }

                SetControlValue("txtAsyPlainText", result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"解密失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSign_Click(object sender, EventArgs e)
        {
            try
            {
                string algorithm = GetControlValue("cmbAsymmetricAlgorithm") as string;
                if (algorithm != "DSA" && algorithm != "RSA")
                {
                    MessageBox.Show("只有DSA和RSA算法支持签名功能", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string data = GetControlValue("txtAsyPlainText") as string;
                string privateKey = GetControlValue("txtPrivateKey") as string;
                int keySize = int.Parse(GetControlValue("cmbAsyKeySize") as string);
                string hashName = GetControlValue("cmbHashAlgorithm") as string;
                var hashAlgorithm = new HashAlgorithmName(hashName);

                string result = "";
                if (algorithm == "DSA")
                {
                    result = AsymmetricEncryption.SignDataByDSA(data, privateKey, hashAlgorithm, keySize);
                }
                // RSA签名可以在这里实现

                SetControlValue("txtAsyCipherText", result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"签名失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                string algorithm = GetControlValue("cmbAsymmetricAlgorithm") as string;
                if (algorithm != "DSA" && algorithm != "RSA")
                {
                    MessageBox.Show("只有DSA和RSA算法支持签名验证功能", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string data = GetControlValue("txtAsyPlainText") as string;
                string signature = GetControlValue("txtAsyCipherText") as string;
                string publicKey = GetControlValue("txtPublicKey") as string;
                int keySize = int.Parse(GetControlValue("cmbAsyKeySize") as string);
                string hashName = GetControlValue("cmbHashAlgorithm") as string;
                var hashAlgorithm = new HashAlgorithmName(hashName);

                bool result = false;
                if (algorithm == "DSA")
                {
                    result = AsymmetricEncryption.VerifyDataByDSA(data, signature, publicKey, hashAlgorithm, keySize);
                }
                // RSA验证可以在这里实现

                MessageBox.Show(result ? "签名验证成功" : "签名验证失败", "结果", MessageBoxButtons.OK,
                    result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"验证签名失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 哈希加密相关UI和事件
        private void InitializeHashEncryptionUI()
        {
            // 算法选择
            AddLabelAndComboBox("算法:", "cmbHashAlgorithm", new[] { "MD5", "SHA1", "SHA256", "SHA512", "CRC32" }, 0);

            // 参数
            AddLabelAndTextBox("输入数据:", "txtHashInput", 1);
            AddLabelAndTextBox("哈希结果:", "txtHashOutput", 2);
            AddLabelAndTextBox("盐值(Salt):", "txtSalt", 3);

            // 选项
            AddLabelAndCheckBox("大写形式", "chkUpperCase", 4, true);
            AddLabelAndCheckBox("包含连字符", "chkIncludeHyphen", 5, false);

            // 按钮
            AddButton("计算哈希", "btnComputeHash", BtnComputeHash_Click);
            AddButton("验证哈希", "btnVerifyHash", BtnVerifyHash_Click);
        }

        private void BtnComputeHash_Click(object sender, EventArgs e)
        {
            try
            {
                string algorithm = GetControlValue("cmbHashAlgorithm") as string;
                string input = GetControlValue("txtHashInput") as string;
                string salt = GetControlValue("txtSalt") as string;
                bool upperCase = GetControlValue("chkUpperCase") as bool? ?? true;
                bool includeHyphen = GetControlValue("chkIncludeHyphen") as bool? ?? false;

                string result = "";
                switch (algorithm)
                {
                    case "MD5":
                        result = HashEncryption.EncryptByMD5(input, null, upperCase, includeHyphen, salt);
                        break;
                    case "SHA1":
                        result = HashEncryption.EncryptBySHA(input, HashEncryption.SHAAlgorithmType.SHA1, null, upperCase, includeHyphen, salt);
                        break;
                    case "SHA256":
                        result = HashEncryption.EncryptBySHA(input, HashEncryption.SHAAlgorithmType.SHA256, null, upperCase, includeHyphen, salt);
                        break;
                    case "SHA512":
                        result = HashEncryption.EncryptBySHA(input, HashEncryption.SHAAlgorithmType.SHA512, null, upperCase, includeHyphen, salt);
                        break;
                    case "CRC32":
                        // 修正：调用优化后的CRC32方法
                        result = (string)HashEncryption.ComputeCRC32(input, null, true, 0xFFFFFFFF, 0xFFFFFFFF, true, true, upperCase);
                        break;
                }

                SetControlValue("txtHashOutput", result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"计算哈希失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVerifyHash_Click(object sender, EventArgs e)
        {
            try
            {
                string algorithm = GetControlValue("cmbHashAlgorithm") as string;
                string input = GetControlValue("txtHashInput") as string;
                string targetHash = GetControlValue("txtHashOutput") as string;
                string salt = GetControlValue("txtSalt") as string;
                bool upperCase = GetControlValue("chkUpperCase") as bool? ?? true;
                bool includeHyphen = GetControlValue("chkIncludeHyphen") as bool? ?? false;

                bool result = false;
                switch (algorithm)
                {
                    case "MD5":
                        result = HashEncryption.VerifyByMD5(input, targetHash, null, upperCase, includeHyphen, salt);
                        break;
                    case "SHA1":
                        result = HashEncryption.VerifyBySHA(input, targetHash, HashEncryption.SHAAlgorithmType.SHA1, null, upperCase, includeHyphen, salt);
                        break;
                    case "SHA256":
                        result = HashEncryption.VerifyBySHA(input, targetHash, HashEncryption.SHAAlgorithmType.SHA256, null, upperCase, includeHyphen, salt);
                        break;
                    case "SHA512":
                        result = HashEncryption.VerifyBySHA(input, targetHash, HashEncryption.SHAAlgorithmType.SHA512, null, upperCase, includeHyphen, salt);
                        break;
                    case "CRC32":
                        // 修正：调用优化后的CRC32验证方法，移除多余的upperCase参数
                        result = HashEncryption.VerifyCRC32(
                            input,
                            targetHash,
                            null,
                            true,
                            0xFFFFFFFF,
                            0xFFFFFFFF,
                            true,
                            true);
                        break;
                }

                MessageBox.Show(result ? "哈希验证成功" : "哈希验证失败", "结果", MessageBoxButtons.OK,
                    result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"验证哈希失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 通用加密相关UI和事件
        private void InitializeCommonEncryptionUI()
        {
            // 算法选择
            AddLabelAndComboBox("算法:", "cmbCommonAlgorithm", new[] { "CRC16", "CRC8" }, 0);

            // 参数
            AddLabelAndTextBox("输入数据:", "txtCommonInput", 1);
            AddLabelAndTextBox("校验结果:", "txtCommonOutput", 2);

            // CRC16特有参数
            AddLabelAndTextBox("多项式:", "txtPolynomial", 3, false, "A001");
            AddLabelAndTextBox("初始值:", "txtInitialValue", 4, false, "FFFF");
            AddLabelAndTextBox("最终异或值:", "txtFinalXor", 5, false, "0000");

            // 选项
            AddLabelAndCheckBox("反转输入位", "chkReflectInput", 6, true);
            AddLabelAndCheckBox("反转输出位", "chkReflectOutput", 7, true);
            AddLabelAndCheckBox("校验数据范围", "chkCheckRange", 8, false);

            // 按钮
            AddButton("计算校验值", "btnComputeChecksum", BtnComputeChecksum_Click);
            AddButton("验证校验值", "btnVerifyChecksum", BtnVerifyChecksum_Click);
        }

        private void BtnComputeChecksum_Click(object sender, EventArgs e)
        {
            try
            {
                string algorithm = GetControlValue("cmbCommonAlgorithm") as string;
                string input = GetControlValue("txtCommonInput") as string;
                byte[] data = Encoding.UTF8.GetBytes(input);

                // 获取参数
                bool reflectInput = GetControlValue("chkReflectInput") as bool? ?? true;
                bool reflectOutput = GetControlValue("chkReflectOutput") as bool? ?? true;
                bool checkRange = GetControlValue("chkCheckRange") as bool? ?? false;

                if (algorithm == "CRC16")
                {
                    ushort polynomial = ushort.Parse(GetControlValue("txtPolynomial") as string, System.Globalization.NumberStyles.HexNumber);
                    ushort initialValue = ushort.Parse(GetControlValue("txtInitialValue") as string, System.Globalization.NumberStyles.HexNumber);
                    ushort finalXor = ushort.Parse(GetControlValue("txtFinalXor") as string, System.Globalization.NumberStyles.HexNumber);

                    byte[] result = CommonEncryption.CalculateCrc16(data, polynomial, initialValue, finalXor, reflectInput, reflectOutput);
                    SetControlValue("txtCommonOutput", BitConverter.ToString(result).Replace("-", ""));
                }
                else if (algorithm == "CRC8")
                {
                    byte polynomial = byte.Parse(GetControlValue("txtPolynomial") as string, System.Globalization.NumberStyles.HexNumber);
                    byte initialValue = byte.Parse(GetControlValue("txtInitialValue") as string, System.Globalization.NumberStyles.HexNumber);
                    byte finalXor = byte.Parse(GetControlValue("txtFinalXor") as string, System.Globalization.NumberStyles.HexNumber);

                    byte result = CommonEncryption.CalculateCrc8(data, polynomial, initialValue, finalXor, reflectInput, reflectOutput, checkRange);
                    SetControlValue("txtCommonOutput", result.ToString("X2"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"计算校验值失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVerifyChecksum_Click(object sender, EventArgs e)
        {
            try
            {
                string algorithm = GetControlValue("cmbCommonAlgorithm") as string;
                string input = GetControlValue("txtCommonInput") as string;
                string checksum = GetControlValue("txtCommonOutput") as string;

                // 合并数据和校验值
                byte[] data = Encoding.UTF8.GetBytes(input);
                byte[] checksumBytes = algorithm == "CRC16" ?
                    new byte[2] {
                        byte.Parse(checksum.Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                        byte.Parse(checksum.Substring(0, 2), System.Globalization.NumberStyles.HexNumber)
                    } :
                    new byte[1] { byte.Parse(checksum, System.Globalization.NumberStyles.HexNumber) };

                byte[] dataWithChecksum = new byte[data.Length + checksumBytes.Length];
                Array.Copy(data, dataWithChecksum, data.Length);
                Array.Copy(checksumBytes, 0, dataWithChecksum, data.Length, checksumBytes.Length);

                // 获取参数
                bool reflectInput = GetControlValue("chkReflectInput") as bool? ?? true;
                bool reflectOutput = GetControlValue("chkReflectOutput") as bool? ?? true;
                bool checkRange = GetControlValue("chkCheckRange") as bool? ?? false;

                bool result = false;
                if (algorithm == "CRC16")
                {
                    ushort polynomial = ushort.Parse(GetControlValue("txtPolynomial") as string, System.Globalization.NumberStyles.HexNumber);
                    ushort initialValue = ushort.Parse(GetControlValue("txtInitialValue") as string, System.Globalization.NumberStyles.HexNumber);
                    ushort finalXor = ushort.Parse(GetControlValue("txtFinalXor") as string, System.Globalization.NumberStyles.HexNumber);

                    result = CommonEncryption.VerifyCrc16(dataWithChecksum, data.Length, polynomial, initialValue, finalXor, reflectInput, reflectOutput);
                }
                else if (algorithm == "CRC8")
                {
                    byte polynomial = byte.Parse(GetControlValue("txtPolynomial") as string, System.Globalization.NumberStyles.HexNumber);
                    byte initialValue = byte.Parse(GetControlValue("txtInitialValue") as string, System.Globalization.NumberStyles.HexNumber);
                    byte finalXor = byte.Parse(GetControlValue("txtFinalXor") as string, System.Globalization.NumberStyles.HexNumber);

                    result = CommonEncryption.VerifyCrc8(dataWithChecksum, data.Length, polynomial, initialValue, finalXor, reflectInput, reflectOutput, checkRange);
                }

                MessageBox.Show(result ? "校验值验证成功" : "校验值验证失败", "结果", MessageBoxButtons.OK,
                    result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"验证校验值失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 控件操作辅助方法
        private void AddLabelAndComboBox(string labelText, string comboBoxName, string[] items, int row)
        {
            var label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Margin = new Padding(5)
            };

            var comboBox = new ComboBox
            {
                Name = comboBoxName,
                Dock = DockStyle.Fill,
                Margin = new Padding(5)
            };
            comboBox.Items.AddRange(items);
            comboBox.SelectedIndex = 0;

            pnlParameters.RowCount++;
            pnlParameters.RowStyles.Add(new RowStyle(SizeType.Absolute, 35f));
            pnlParameters.Controls.Add(label, 0, row);
            pnlParameters.Controls.Add(comboBox, 1, row);
        }

        private void AddLabelAndTextBox(string labelText, string textBoxName, int row, bool multiLine = false, string defaultValue = "")
        {
            var label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Margin = new Padding(5)
            };

            var textBox = new TextBox
            {
                Name = textBoxName,
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                Multiline = multiLine
            };

            pnlParameters.RowCount++;
            if (multiLine)
            {
                textBox.Height = 80;
                pnlParameters.RowStyles.Add(new RowStyle(SizeType.Absolute, 90f));
            }
            else
            {
                pnlParameters.RowStyles.Add(new RowStyle(SizeType.Absolute, 35f));
            }

            if (!string.IsNullOrEmpty(defaultValue))
                textBox.Text = defaultValue;

            pnlParameters.Controls.Add(label, 0, row);
            pnlParameters.Controls.Add(textBox, 1, row);
        }

        private void AddLabelAndCheckBox(string labelText, string checkBoxName, int row, bool defaultValue)
        {
            var label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                Margin = new Padding(5)
            };

            var checkBox = new CheckBox
            {
                Name = checkBoxName,
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                Checked = defaultValue,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Text = ""
            };

            pnlParameters.RowCount++;
            pnlParameters.RowStyles.Add(new RowStyle(SizeType.Absolute, 35f));
            pnlParameters.Controls.Add(label, 0, row);
            pnlParameters.Controls.Add(checkBox, 1, row);
        }

        private void AddButton(string text, string buttonName, EventHandler clickEvent)
        {
            var button = new Button
            {
                Text = text,
                Name = buttonName,
                Size = new Size(120, 35),
                Margin = new Padding(5, 5, 10, 5)
            };
            button.Click += clickEvent;

            // 样式优化
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = Color.FromArgb(66, 133, 244);
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;

            pnlActions.Controls.Add(button);
        }

        private object GetControlValue(string controlName)
        {
            Control control = pnlParameters.Controls.Find(controlName, true).FirstOrDefault();
            if (control == null)
                control = pnlActions.Controls.Find(controlName, true).FirstOrDefault();

            if (control is ComboBox comboBox)
                return comboBox.SelectedItem;
            if (control is TextBox textBox)
                return textBox.Text;
            if (control is CheckBox checkBox)
                return checkBox.Checked;

            return null;
        }

        private void SetControlValue(string controlName, string value)
        {
            Control control = pnlParameters.Controls.Find(controlName, true).FirstOrDefault();
            if (control is TextBox textBox)
                textBox.Text = value;
        }
        #endregion
    }
}
