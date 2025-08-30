using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace _3dedit
{
    public partial class KeybindSetup : Form
    {
        Keybindings keybinds;
        string curKeybindsName;
        Keybindings.KeybindSet curKeybinds;

        static Dictionary<string, Func<Keybindings.IAction>> actionList = new Dictionary<string, Func<Keybindings.IAction>>
        {
            { "Grip", () => new Keybindings.Grip() },
            { "Twist", () => new Keybindings.Twist() },
            { "Twist2c", () => new Keybindings.Twist2c() },
            { "Layer", () => new Keybindings.Layer() },
            { "Recenter", () => new Keybindings.Recenter() },
        };

        public KeybindSetup(Keybindings keybinds)
        {
            InitializeComponent();
            this.keybinds = keybinds;
        }

        private void SetLayout(string name)
        {
            if (!keybinds.keybinds.ContainsKey(name)) return;

            curKeybindsName = name;
            curKeybinds = keybinds.keybinds[name];
            this.Text = $"Keybinds Setup - {name}";

            Control addButton = keybindsPanel.Controls[keybindsPanel.Controls.Count - 1];
            keybindsPanel.Controls.Clear();
            foreach (var item in curKeybinds.binds)
            {
                FlowLayoutPanel panel = CreateKeybindPanel(item.Key, item.Value);
                keybindsPanel.Controls.Add(panel);
            }
            keybindsPanel.Controls.Add(addButton);
        }

        private FlowLayoutPanel CreateKeybindPanel(string key, Keybindings.IAction action)
        {
            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Name = key,
                Height = 28,
                WrapContents = false,
            };

            var boundKey = key;
            TextBox textBox = new TextBox
            {
                Text = key,
                Name = "textBox" + key,
                Size = new Size(64, 24),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
            };
            textBox.KeyUp += new KeyEventHandler(this.Hotkey_KeyUp);
            textBox.KeyUp += (object sender, KeyEventArgs e) =>
            {
                var tb = (TextBox)sender;
                if (curKeybinds.binds.ContainsKey(tb.Text))
                {
                    MessageBox.Show($"{tb.Text} is already bound to {curKeybinds.binds[tb.Text].Serialize()}");
                    tb.Text = boundKey;
                }
                else
                {
                    curKeybinds.binds.Remove(boundKey);
                    curKeybinds.binds.Add(tb.Text, action);
                }
            };

            ComboBox comboBox = new ComboBox
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                DropDownStyle = ComboBoxStyle.DropDownList,
                ItemHeight = 24,
                Name = "comboBox" + key,
                Size = new Size(96, 30),
            };

            FlowLayoutPanel extra = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
            };
            var extras = action.SetupControls();
            extra.Controls.AddRange(extras);

            var actions = actionList.Keys.ToArray();
            comboBox.Items.AddRange(actions);
            comboBox.SelectedIndex = comboBox.Items.IndexOf(action.GetType().Name);
            comboBox.MouseWheel += (object sender, MouseEventArgs e) => ((HandledMouseEventArgs)e).Handled = true;
            comboBox.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                Console.WriteLine(comboBox.SelectedItem);
                action = actionList[(string)comboBox.SelectedItem]();
                extra.Controls.Clear();
                extra.Controls.AddRange(action.SetupControls());

                curKeybinds.binds[textBox.Text] = action;
            };


            panel.Controls.Add(textBox);
            panel.Controls.Add(comboBox);
            panel.Controls.Add(extra);

            return panel;
        }

        private void CreateLayoutButton(string name)
        {
            Button btn = new Button();
            btn.Text = name;
            btn.Click += new System.EventHandler(this.SwitchLayout_Click);
            btn.Width = addNewLayout.Width;

            this.keybindSetsPanel.Controls.Add(btn);
        }

        private void KeybindSetup_Load(object sender, EventArgs e)
        {
            foreach (var item in keybinds.keybinds) {
                CreateLayoutButton(item.Key);
            }

            SetLayout(keybinds.activeKeybindsName);
        }

        private void AddNewLayout_Click(object sender, EventArgs e)
        {
            CreateLayoutButton("Temp Name");
        }

        private void DeleteLayout_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SwitchLayout_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            SetLayout(btn.Text);
        }

        private void Hotkey_KeyUp(object sender, KeyEventArgs e)
        {
            Control ctrl = (Control)sender;
            ctrl.Text = e.KeyCode.ToString();
        }
    }
}
