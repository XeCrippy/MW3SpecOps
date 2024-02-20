using JRPC_Client;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using XDevkit;

namespace MW3SpecOps
{
    public partial class Form1 : Form
    {
        IXboxConsole xb;
        private bool connected;

        private byte[] ConvertHexStringToByteArray(string hex)
        {
            int i = hex.Length;
            byte[] zero = new byte[i];
            byte[] bytes = Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
            return bytes;
        }

        public UInt32 ReverseUInt32(UInt32 value)
        {
            return (value & 0x000000FFU) << 24 |
                   (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 |
                   (value & 0xFF000000U) >> 24;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (xb.Connect(out xb))
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    connected = true;
                }
                else
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    connected = false;
                    MessageBox.Show("Failed to connect to default console", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!connected)
                {
                    MessageBox.Show("You are not connected to your console", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                uint xpAddr = 0x8370CD39;
                uint xp = 2175000u;
                xb.WriteUInt32(xpAddr, ReverseUInt32(xp));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!connected)
                {
                    MessageBox.Show("You are not connected to your console", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                uint branchAddrMoney = 0x822B4D04;
                uint branchBytesMoney = 0x49C4B2FC;
                uint newFuncAddrMoney = 0x83F00000;
                string hexMoney = "3C40BFBF6042700B7C02E80041820008408200203C4083F06042010093E200003840270F1C4203E8905F00004A3B4CDC90DF00004A3B4CD4";
                byte[] newFuncBytesMoney = ConvertHexStringToByteArray(hexMoney);
                xb.SetMemory(newFuncAddrMoney, newFuncBytesMoney);
                xb.WriteUInt32(branchAddrMoney, branchBytesMoney);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!connected)
                {
                    MessageBox.Show("You are not connected to your console", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                uint branchAddrGodOHK = 0x82200300;
                uint branchBytesGodOHK = 0x49CFFD50;
                uint newFuncAddrGodOHK = 0x83F00050;
                string hexGodOHK = "3A0000006210AE407C0980004182001040820004901F01504A30029C600000004A300294";
                byte[] newFuncBytesGodOHK = ConvertHexStringToByteArray(hexGodOHK);
                xb.SetMemory(newFuncAddrGodOHK, newFuncBytesGodOHK);
                xb.WriteUInt32(branchAddrGodOHK, branchBytesGodOHK);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
