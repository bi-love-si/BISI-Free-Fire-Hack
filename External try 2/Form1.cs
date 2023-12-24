using Memory;
using System.Diagnostics;
namespace External_try_2
{
    public partial class Form1 : Form
    {
        Mem memory = new Mem();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("HD-Player").Length == 0)
            {
                label3.Text = "Open emulator";
            }
            else
            {
                label3.Text = "Emulator found";
                Int32 proc = Process.GetProcessesByName("HD-Player")[0].Id;
                memory.OpenProcess(proc);

                var result = await memory.AoBScan("FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? ?? 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 A5 43 00 00 00 00", true, true);


                if (result.Any())
                {
                    foreach (var CurrentAddress in result)
                    {
                        Int64 bikashRead = CurrentAddress + 0x60;
                        Int64 bikashWrite = CurrentAddress + 0x5C;
                        var Read = memory.ReadMemory<int>(bikashRead.ToString("X"));
                        memory.WriteMemory(bikashWrite.ToString("X"), "int", Read.ToString());
                    }
                    label5.Text = "Aimbot injected";

                }
                else
                {
                    label5.Text = "Scan failed";

                }


            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
