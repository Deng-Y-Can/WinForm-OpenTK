using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp.WindowsTool
{
    public partial class CandyInput : Form
    {
        // Windows API 导入
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // 钩子类型和常量定义
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_CHAR = 0x0102;
        private const uint KEYEVENTF_KEYDOWN = 0x0000;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        // 虚拟键码常量
        private const int VK_SHIFT = 0x10;
        private const int VK_CAPITAL = 0x14;
        private const int VK_CONTROL = 0x11;
        private const int VK_MENU = 0x12; // Alt键
        private const int VK_SPACE = 0x20;
        private const int VK_BACK = 0x08;
        private const int VK_RETURN = 0x0D;
        private const int VK_ADD = 0x6B;
        private const int VK_SUBTRACT = 0x6D;
        private const int VK_MULTIPLY = 0x6A;
        private const int VK_DIVIDE = 0x6F;
        private const int VK_DECIMAL = 0x6E;

        // 输入法状态
        private bool isEnabled = false;
        private InputMode currentMode = InputMode.EnglishLower;
        private IntPtr hookId = IntPtr.Zero;
        private HookProc hookProc;
        private StringBuilder pinyinBuffer = new StringBuilder();
        private List<string> candidates = new List<string>();
        private int candidateIndex = 0;
        private bool isShiftPressed = false;
        private bool isCtrlPressed = false;
        private bool isAltPressed = false;

        // 安全机制变量
        private DateTime lastHookTime = DateTime.MinValue;
        private int hookCallCount = 0;
        private const int MAX_HOOK_CALLS_PER_SECOND = 1000;
        private bool isErrorState = false;

        // 按键防抖 - 使用更精确的按键状态跟踪
        private Dictionary<int, KeyStateInfo> keyStates = new Dictionary<int, KeyStateInfo>();
        private const int KEY_DEBOUNCE_MS = 50; // 按键防抖时间，毫秒

        // 记录最后处理的按键信息
        private int lastProcessedVkCode = 0;
        private IntPtr lastProcessedWParam = IntPtr.Zero;
        private DateTime lastProcessedTime = DateTime.MinValue;
        private const int DUPLICATE_KEY_THRESHOLD = 50; // 重复按键阈值，毫秒

        // 拼音字典
        private Dictionary<string, List<string>> pinyinDict = new Dictionary<string, List<string>>
        {
            { "ni", new List<string> { "你", "尼", "泥", "妮", "拟" } },
            { "hao", new List<string> { "好", "号", "浩", "豪", "耗" } },
            { "wo", new List<string> { "我", "窝", "卧", "握", "沃" } },
            { "shi", new List<string> { "是", "十", "时", "事", "市" } },
            { "zhong", new List<string> { "中", "种", "重", "众", "忠" } },
            { "wen", new List<string> { "文", "问", "闻", "温", "稳" } },
            { "hello", new List<string> { "你好", "喂", "哈喽" } },
            { "world", new List<string> { "世界", "地球", "天下" } },
            { "china", new List<string> { "中国", "中华", "华夏" } },
            { "nihao", new List<string> { "你好", "您好" } },
            { "zaijian", new List<string> { "再见", "再会", "拜拜" } },
            { "xiexie", new List<string> { "谢谢", "多谢", "感谢" } },
            { "qing", new List<string> { "请", "清", "轻", "情", "晴" } },
            { "shuo", new List<string> { "说", "硕", "烁" } },
            { "hua", new List<string> { "话", "花", "画", "华", "滑" } }
        };

        // 符号映射表 - 扩展版，支持更多符号
        private Dictionary<int, char> symbolMap = new Dictionary<int, char>
        {
            { 0x30, '0' }, { 0x31, '1' }, { 0x32, '2' }, { 0x33, '3' }, { 0x34, '4' },
            { 0x35, '5' }, { 0x36, '6' }, { 0x37, '7' }, { 0x38, '8' }, { 0x39, '9' },
            { 0xBE, '.' }, { 0xBB, '=' }, { 0xBD, '-' },
            { 0xC0, '`' }, { 0xDB, '[' }, { 0xDC, '\\' },
            { 0xDD, ']' }, { 0xDE, '\'' }, { 0xBA, ';' },
            { 0xBC, ',' }, { 0xBF, '/' },
            { VK_ADD, '+' }, { VK_SUBTRACT, '-' }, { VK_MULTIPLY, '*' },
            { VK_DIVIDE, '/' }, { VK_DECIMAL, '.' }
        };

        private Dictionary<int, char> shiftedSymbolMap = new Dictionary<int, char>
        {
            { 0x30, ')' }, { 0x31, '!' }, { 0x32, '@' }, { 0x33, '#' }, { 0x34, '$' },
            { 0x35, '%' }, { 0x36, '^' }, { 0x37, '&' }, { 0x38, '*' }, { 0x39, '(' },
            { 0xBE, '>' }, { 0xBB, '+' }, { 0xBD, '_' },
            { 0xC0, '~' }, { 0xDB, '{' }, { 0xDC, '|' },
            { 0xDD, '}' }, { 0xDE, '"' }, { 0xBA, ':' },
            { 0xBC, '<' }, { 0xBF, '?' }
        };

        // 输入法状态显示控件
        private Label statusLabel;
        private Label pinyinLabel;
        private Label candidateLabel;
        private Button toggleButton;
        private ComboBox modeComboBox;
        private Label safetyLabel;

        // 显式定义 HookProc 委托
        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        // 输入法模式枚举
        private enum InputMode
        {
            Chinese,
            EnglishUpper,
            EnglishLower
        }

        // 按键状态信息类
        private class KeyStateInfo
        {
            public bool IsDown { get; set; }
            public DateTime LastProcessedTime { get; set; }
            public int ProcessCount { get; set; }
        }

        public CandyInput()
        {
            // 初始化窗体
            Text = "自定义输入法";
            Width = 400;
            Height = 310;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = System.Drawing.Color.FromArgb(245, 245, 245);

            // 初始化钩子处理函数
            hookProc = HookCallback;

            // 创建顶部标题
            Label titleLabel = new Label
            {
                Text = "自定义输入法控制台",
                Font = new System.Drawing.Font("微软雅黑", 12, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(50, 50, 50),
                Location = new System.Drawing.Point(120, 15),
                Size = new System.Drawing.Size(160, 30),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            Controls.Add(titleLabel);

            // 创建状态标签
            statusLabel = new Label
            {
                Text = "状态: 禁用 | 英文小写",
                Font = new System.Drawing.Font("微软雅黑", 9),
                ForeColor = System.Drawing.Color.FromArgb(80, 80, 80),
                Location = new System.Drawing.Point(120, 50),
                Size = new System.Drawing.Size(160, 20),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            Controls.Add(statusLabel);

            // 创建安全状态标签
            safetyLabel = new Label
            {
                Text = "安全状态: 正常",
                Font = new System.Drawing.Font("微软雅黑", 8, System.Drawing.FontStyle.Italic),
                ForeColor = System.Drawing.Color.Green,
                Location = new System.Drawing.Point(120, 70),
                Size = new System.Drawing.Size(160, 15),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            Controls.Add(safetyLabel);

            // 创建控制按钮
            toggleButton = new Button
            {
                Text = "启用输入法",
                Font = new System.Drawing.Font("微软雅黑", 10),
                BackColor = System.Drawing.Color.FromArgb(66, 133, 244),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(150, 85),
                Size = new System.Drawing.Size(100, 35)
            };
            toggleButton.FlatAppearance.BorderSize = 0;
            toggleButton.Click += ToggleButton_Click;
            Controls.Add(toggleButton);

            // 创建模式选择下拉框
            modeComboBox = new ComboBox
            {
                Font = new System.Drawing.Font("微软雅黑", 9),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new System.Drawing.Point(120, 135),
                Size = new System.Drawing.Size(160, 23)
            };
            modeComboBox.Items.AddRange(new object[] { "中文", "英文大写", "英文小写" });
            modeComboBox.SelectedIndex = (int)InputMode.EnglishLower;
            modeComboBox.SelectedIndexChanged += ModeComboBox_SelectedIndexChanged;
            Controls.Add(modeComboBox);

            // 创建拼音缓冲区显示
            pinyinLabel = new Label
            {
                Text = "拼音: ",
                Font = new System.Drawing.Font("微软雅黑", 10),
                ForeColor = System.Drawing.Color.FromArgb(50, 50, 50),
                Location = new System.Drawing.Point(50, 180),
                Size = new System.Drawing.Size(300, 25),
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(pinyinLabel);

            // 创建候选词显示
            candidateLabel = new Label
            {
                Text = "候选: ",
                Font = new System.Drawing.Font("微软雅黑", 10),
                ForeColor = System.Drawing.Color.FromArgb(50, 50, 50),
                Location = new System.Drawing.Point(50, 210),
                Size = new System.Drawing.Size(300, 25),
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(candidateLabel);

            // 创建错误提示标签
            Label errorLabel = new Label
            {
                Text = "注意: 按Ctrl+Shift+X可强制禁用输入法",
                Font = new System.Drawing.Font("微软雅黑", 8, System.Drawing.FontStyle.Italic),
                ForeColor = System.Drawing.Color.FromArgb(100, 100, 100),
                Location = new System.Drawing.Point(50, 240),
                Size = new System.Drawing.Size(300, 20),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            Controls.Add(errorLabel);

            // 初始化定时器
            System.Windows.Forms.Timer safetyTimer = new System.Windows.Forms.Timer();
            safetyTimer.Interval = 1000;
            safetyTimer.Tick += (sender, e) =>
            {
                hookCallCount = 0;
                keyStates.Clear(); // 每秒重置所有按键状态

                if (isErrorState)
                {
                    safetyLabel.Text = "安全状态: 错误 - 已禁用";
                    safetyLabel.ForeColor = System.Drawing.Color.Red;

                    if (isEnabled)
                    {
                        this.Invoke(new Action(() =>
                        {
                            ToggleButton_Click(toggleButton, EventArgs.Empty);
                        }));
                    }
                }
                else
                {
                    safetyLabel.Text = "安全状态: 正常";
                    safetyLabel.ForeColor = System.Drawing.Color.Green;
                }
            };
            safetyTimer.Start();
        }

        // 切换输入法启用/禁用状态
        private void ToggleButton_Click(object sender, EventArgs e)
        {
            try
            {
                isEnabled = !isEnabled;

                if (isEnabled)
                {
                    toggleButton.Text = "禁用输入法";
                    toggleButton.BackColor = System.Drawing.Color.FromArgb(219, 68, 55);

                    using (System.Diagnostics.Process curProcess = System.Diagnostics.Process.GetCurrentProcess())
                    using (System.Diagnostics.ProcessModule curModule = curProcess.MainModule)
                    {
                        hookId = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, GetModuleHandle(curModule.ModuleName), 0);

                        if (hookId == IntPtr.Zero)
                        {
                            throw new Exception("无法设置键盘钩子，可能缺少权限");
                        }
                    }

                    isErrorState = false;
                    keyStates.Clear(); // 清除所有按键状态
                }
                else
                {
                    toggleButton.Text = "启用输入法";
                    toggleButton.BackColor = System.Drawing.Color.FromArgb(66, 133, 244);

                    if (hookId != IntPtr.Zero)
                    {
                        UnhookWindowsHookEx(hookId);
                        hookId = IntPtr.Zero;
                    }

                    ClearInputState();
                    keyStates.Clear(); // 清除所有按键状态
                }

                UpdateStatusLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"切换输入法状态时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isEnabled = false;
                toggleButton.Text = "启用输入法";
                toggleButton.BackColor = System.Drawing.Color.FromArgb(66, 133, 244);
                isErrorState = true;
            }
        }

        // 模式选择变更
        private void ModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isEnabled)
                {
                    currentMode = (InputMode)modeComboBox.SelectedIndex;
                    ClearInputState();
                    UpdateStatusLabel();
                }
                else
                {
                    // 如果输入法未启用，恢复之前的选择
                    modeComboBox.SelectedIndex = (int)currentMode;
                    MessageBox.Show("请先启用输入法", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"切换输入模式时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isErrorState = true;
            }
        }

        // 更新状态标签显示
        private void UpdateStatusLabel()
        {
            string status = isEnabled ? "启用" : "禁用";
            string modeText = currentMode switch
            {
                InputMode.Chinese => "中文",
                InputMode.EnglishUpper => "英文大写",
                InputMode.EnglishLower => "英文小写",
                _ => "未知"
            };

            statusLabel.Text = $"状态: {status} | {modeText}";
        }

        // 清空输入状态
        private void ClearInputState()
        {
            pinyinBuffer.Clear();
            candidates.Clear();
            candidateIndex = 0;
            UpdateInputDisplay();
        }

        // 更新输入显示
        private void UpdateInputDisplay()
        {
            pinyinLabel.Text = currentMode == InputMode.Chinese ?
                "拼音: " + pinyinBuffer.ToString() : "拼音: (中文模式下有效)";

            string candidateText = "候选: ";
            if (currentMode == InputMode.Chinese)
            {
                for (int i = 0; i < candidates.Count && i < 5; i++)
                {
                    candidateText += $"[{i + 1}]{candidates[i]} ";
                }
            }
            else
            {
                candidateText += "(中文模式下有效)";
            }

            candidateLabel.Text = candidateText;
        }

        // 键盘钩子回调函数
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                // 安全检查
                DateTime now = DateTime.Now;
                TimeSpan elapsed = now - lastHookTime;

                if (elapsed.TotalSeconds < 1)
                {
                    hookCallCount++;

                    if (hookCallCount > MAX_HOOK_CALLS_PER_SECOND)
                    {
                        isErrorState = true;
                        MessageBox.Show("检测到异常输入活动，已自动禁用输入法", "安全警告",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Invoke(new Action(() =>
                        {
                            if (isEnabled)
                            {
                                ToggleButton_Click(toggleButton, EventArgs.Empty);
                            }
                        }));
                        return (IntPtr)1;
                    }
                }
                else
                {
                    hookCallCount = 1;
                    lastHookTime = now;
                }

                if (nCode >= 0)
                {
                    int vkCode = Marshal.ReadInt32(lParam);

                    // 紧急禁用快捷键
                    if (isCtrlPressed && isShiftPressed && vkCode == 0x58) // Ctrl+Shift+X
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (isEnabled)
                            {
                                ToggleButton_Click(toggleButton, EventArgs.Empty);
                                MessageBox.Show("已通过快捷键强制禁用输入法", "操作成功",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }));
                        return (IntPtr)1;
                    }

                    // 按键防抖处理
                    bool isKeyDown = (wParam == (IntPtr)WM_KEYDOWN);

                    // 获取或创建按键状态
                    if (!keyStates.TryGetValue(vkCode, out KeyStateInfo state))
                    {
                        state = new KeyStateInfo { IsDown = false, LastProcessedTime = DateTime.MinValue, ProcessCount = 0 };
                        keyStates[vkCode] = state;
                    }

                    // 检查重复按键
                    if (isKeyDown && vkCode == lastProcessedVkCode && wParam == lastProcessedWParam)
                    {
                        TimeSpan timeSinceLast = now - lastProcessedTime;
                        if (timeSinceLast.TotalMilliseconds < DUPLICATE_KEY_THRESHOLD)
                        {
                            // 忽略过快的重复按键
                            return (IntPtr)1;
                        }
                    }

                    // 检查按键是否已经按下
                    if (isKeyDown && state.IsDown)
                    {
                        // 按键已处于按下状态，检查是否是长按产生的重复按键
                        TimeSpan timeSinceLastDown = now - state.LastProcessedTime;

                        // 如果不是长按产生的合理重复，则忽略
                        if (timeSinceLastDown.TotalMilliseconds < KEY_DEBOUNCE_MS)
                        {
                            return (IntPtr)1;
                        }
                    }

                    // 更新最后处理的按键信息
                    lastProcessedVkCode = vkCode;
                    lastProcessedWParam = wParam;
                    lastProcessedTime = now;

                    // 跟踪Ctrl、Shift、Alt键状态
                    if (isKeyDown)
                    {
                        if (vkCode == VK_CONTROL) isCtrlPressed = true;
                        if (vkCode == VK_SHIFT) isShiftPressed = true;
                        if (vkCode == VK_MENU) isAltPressed = true;
                    }
                    else if (wParam == (IntPtr)WM_KEYUP)
                    {
                        if (vkCode == VK_CONTROL) isCtrlPressed = false;
                        if (vkCode == VK_SHIFT) isShiftPressed = false;
                        if (vkCode == VK_MENU) isAltPressed = false;
                    }

                    // 更新按键状态
                    state.IsDown = isKeyDown;
                    state.LastProcessedTime = now;

                    // 只处理按键按下事件
                    if (isKeyDown)
                    {
                        // 输入法切换快捷键
                        if (isCtrlPressed && isShiftPressed && vkCode == 0x49) // Ctrl+Shift+I
                        {
                            this.Invoke(new Action(() =>
                            {
                                ToggleButton_Click(toggleButton, EventArgs.Empty);
                            }));
                            return (IntPtr)1;
                        }

                        // 模式切换快捷键
                        if (isCtrlPressed && vkCode == 0x4D) // Ctrl+M
                        {
                            this.Invoke(new Action(() =>
                            {
                                // 循环切换模式
                                int nextMode = ((int)currentMode + 1) % 3;
                                modeComboBox.SelectedIndex = nextMode;
                                currentMode = (InputMode)nextMode;
                                ClearInputState();
                                UpdateStatusLabel();
                            }));
                            return (IntPtr)1;
                        }

                        // 只处理启用状态下的按键
                        if (isEnabled && !isErrorState)
                        {
                            // 处理功能键 (F1-F12, Tab, Enter, Backspace等)
                            if (IsFunctionKey(vkCode))
                            {
                                // 直接传递功能键
                                return CallNextHookEx(hookId, nCode, wParam, lParam);
                            }

                            // 根据当前模式处理按键
                            switch (currentMode)
                            {
                                case InputMode.Chinese:
                                    return ProcessChineseInput(vkCode, nCode, wParam, lParam);

                                case InputMode.EnglishUpper:
                                    return ProcessEnglishInput(vkCode, true, nCode, wParam, lParam);

                                case InputMode.EnglishLower:
                                    return ProcessEnglishInput(vkCode, false, nCode, wParam, lParam);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"键盘钩子处理错误: {ex}");
                isErrorState = true;

                this.Invoke(new Action(() =>
                {
                    MessageBox.Show($"输入法遇到错误并已禁用: {ex.Message}", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (isEnabled)
                    {
                        ToggleButton_Click(toggleButton, EventArgs.Empty);
                    }
                }));
            }

            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        // 判断是否为功能键
        private bool IsFunctionKey(int vkCode)
        {
            // F1-F12
            if (vkCode >= 0x70 && vkCode <= 0x7B) return true;

            // Tab, Enter, Backspace, Escape等
            if (vkCode == 0x09 || vkCode == 0x0D || vkCode == 0x08 || vkCode == 0x1B) return true;

            // 方向键
            if (vkCode >= 0x25 && vkCode <= 0x28) return true;

            // 其他功能键
            if (vkCode == 0x2D || vkCode == 0x2E || vkCode == 0x2F) return true;

            return false;
        }

        // 处理中文输入
        private IntPtr ProcessChineseInput(int vkCode, int nCode, IntPtr wParam, IntPtr lParam)
        {
            // 字母键处理
            if ((vkCode >= 0x41 && vkCode <= 0x5A) || (vkCode >= 0x61 && vkCode <= 0x7A))
            {
                char c = (char)(vkCode >= 0x41 && vkCode <= 0x5A ? vkCode + 32 : vkCode - 48);
                pinyinBuffer.Append(c);
                GenerateCandidates();
                UpdateInputDisplay();
                return (IntPtr)1;
            }
            else if (vkCode == VK_RETURN) // Enter 键
            {
                // 确认当前候选词
                if (candidates.Count > 0)
                {
                    SendChineseCharacter(candidates[0]);
                    ClearInputState();
                }
                return (IntPtr)1;
            }
            else if (vkCode == VK_BACK) // Backspace 键
            {
                if (pinyinBuffer.Length > 0)
                {
                    pinyinBuffer.Length--;
                    GenerateCandidates();
                    UpdateInputDisplay();
                }
                return (IntPtr)1;
            }
            else if (vkCode >= 0x31 && vkCode <= 0x39) // 数字键 1-9
            {
                // 选择候选词
                int selection = vkCode - 0x31;
                if (selection < candidates.Count)
                {
                    SendChineseCharacter(candidates[selection]);
                    ClearInputState();
                }
                return (IntPtr)1;
            }
            else if (vkCode == VK_SPACE) // 空格键
            {
                // 确认当前候选词
                if (candidates.Count > 0)
                {
                    SendChineseCharacter(candidates[0]);
                    ClearInputState();
                }
                return (IntPtr)1;
            }
            else if (symbolMap.ContainsKey(vkCode) || shiftedSymbolMap.ContainsKey(vkCode))
            {
                // 符号键处理
                char c = GetSymbolFromVkCode(vkCode);
                SendKey((byte)c);
                ClearInputState(); // 输入符号后清空拼音缓冲区
                return (IntPtr)1;
            }

            // 拦截所有其他按键
            return (IntPtr)1;
        }

        // 处理英文输入
        private IntPtr ProcessEnglishInput(int vkCode, bool isUpper, int nCode, IntPtr wParam, IntPtr lParam)
        {
            // 字母键处理
            if ((vkCode >= 0x41 && vkCode <= 0x5A) || (vkCode >= 0x61 && vkCode <= 0x7A))
            {
                char baseChar = (char)(vkCode >= 0x41 && vkCode <= 0x5A ? vkCode : vkCode - 32);
                char c = isUpper ? baseChar : (char)(baseChar + 32);
                SendKey((byte)c);
                return (IntPtr)1;
            }
            else if (symbolMap.ContainsKey(vkCode) || shiftedSymbolMap.ContainsKey(vkCode))
            {
                // 符号键处理
                char c = GetSymbolFromVkCode(vkCode);
                SendKey((byte)c);
                return (IntPtr)1;
            }

            // 让系统处理其他按键（如功能键）
            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        // 根据VK码获取符号 - 优化符号处理逻辑
        private char GetSymbolFromVkCode(int vkCode)
        {
            // 检查是否是Shift+数字组合
            if (isShiftPressed && vkCode >= 0x30 && vkCode <= 0x39)
            {
                // 数字键0-9在Shift下的符号
                char[] shiftedDigits = { ')', '!', '@', '#', '$', '%', '^', '&', '*', '(' };
                return shiftedDigits[vkCode - 0x30];
            }

            // 检查普通符号映射
            if (shiftedSymbolMap.ContainsKey(vkCode) && isShiftPressed)
            {
                return shiftedSymbolMap[vkCode];
            }
            else if (symbolMap.ContainsKey(vkCode))
            {
                return symbolMap[vkCode];
            }

            // 默认返回原始字符
            return (char)vkCode;
        }

        // 生成候选词
        private void GenerateCandidates()
        {
            candidates.Clear();
            string pinyin = pinyinBuffer.ToString();

            if (!string.IsNullOrEmpty(pinyin))
            {
                // 精确匹配
                if (pinyinDict.ContainsKey(pinyin))
                {
                    candidates.AddRange(pinyinDict[pinyin]);
                }

                // 模糊匹配（前缀匹配）
                if (candidates.Count == 0)
                {
                    foreach (var key in pinyinDict.Keys)
                    {
                        if (key.StartsWith(pinyin))
                        {
                            candidates.AddRange(pinyinDict[key]);
                            if (candidates.Count >= 5) break;
                        }
                    }
                }
            }
        }

        // 发送按键
        private void SendKey(byte vkCode)
        {
            IntPtr hwnd = GetForegroundWindow();

            // 发送按键按下消息
            keybd_event(vkCode, 0, KEYEVENTF_KEYDOWN, 0);

            // 发送字符消息
            char c = (char)vkCode;
            PostMessage(hwnd, WM_CHAR, (IntPtr)c, IntPtr.Zero);

            // 发送按键释放消息
            keybd_event(vkCode, 0, KEYEVENTF_KEYUP, 0);
        }

        // 发送中文字符
        private void SendChineseCharacter(string text)
        {
            IntPtr hwnd = GetForegroundWindow();

            foreach (char c in text)
            {
                PostMessage(hwnd, WM_CHAR, (IntPtr)c, IntPtr.Zero);
            }
        }

        // 窗体关闭时清理资源
        private void CandyInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (hookId != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(hookId);
                }

                base.OnFormClosing(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"关闭输入法时出错: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
