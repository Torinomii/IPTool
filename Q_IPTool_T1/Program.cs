using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Q_IPTool_T1
{
    class Program
    {
        static void Main(string[] args)
        {
            Cmd Ncmd = new Cmd();
            Ncmd.RunCmd();
        }
    }
    class Cmd
    {
        CMD Rcmd = new CMD();
        public void RunCmd()
        {
            Console.Clear();
            Console.WriteLine("1.DHCP");
            Console.WriteLine("2.Static_IP");
            Console.WriteLine("3.Show_ipconfig");
            Console.WriteLine("4.Exit");
            ConsoleKeyInfo IfType = Console.ReadKey();
            if (IfType.KeyChar == '1')//DHCP
            {
                Console.Clear();
                Console.WriteLine(Rcmd.CMDEnv("netsh interface ip set address \"以太网\" DHCP" + "&exit"));
                Console.ReadKey();
            }
            if (IfType.KeyChar == '2')//Static_IP
            {
                Console.Clear();
                Console.WriteLine("You chose Static_ip");
                Console.WriteLine("Enter IP Address,default 192.168.1.100");
                var ReadIP = Console.ReadLine();
                if (ReadIP == "")
                {
                    ReadIP = "192.168.1.100";
                }
                Console.WriteLine("Enter NetMask,default 255.255.255.0");
                var ReadNetmask = Console.ReadLine();
                if (ReadNetmask == "")
                {
                    ReadNetmask = "255.255.255.0";
                }
                Console.WriteLine("Enter GateWay,default 192.168.1.1");
                var ReadGateWay = Console.ReadLine();
                if (ReadGateWay == "")
                {
                    ReadGateWay = "192.168.1.1";
                }
                Console.Clear();
                Console.WriteLine("IPAddr : " + ReadIP + "\nNetMask: " + ReadNetmask + "\nGateWay: " + ReadGateWay);
                Console.WriteLine("Are you sure? Y/n");
                ConsoleKeyInfo IfYN = Console.ReadKey();
                if (IfYN.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(Rcmd.CMDEnv("netsh interface ip set address \"以太网\" static" + " " + ReadIP + " " + ReadNetmask + " " + ReadGateWay + "&exit"));//English System (netsh interface ip set address \"Ethernet\" static" + " " + IPaddr + " " + Netmask + " " + Gateway + "&exit")
                    Console.WriteLine(Rcmd.CMDEnv("ipconfig" + "&exit"));
                    Console.ReadKey();
                }
                if (IfYN.KeyChar == 'y')
                {
                    Console.WriteLine(Rcmd.CMDEnv("netsh interface ip set address \"以太网\" static" + " " + ReadIP + " " + ReadNetmask + " " + ReadGateWay + "&exit"));
                    Console.WriteLine(Rcmd.CMDEnv("ipconfig" + "&exit"));
                    Console.ReadKey();
                }
                else
                {
                    RunCmd();
                }
                Console.ReadKey();
            }
            if (IfType.KeyChar == '3')//Show_ipconfig
            {
                Console.Clear();
                Console.WriteLine(Rcmd.CMDEnv("ipconfig" + "&exit"));
                Console.ReadKey();
                RunCmd();
            }
            if (IfType.KeyChar == '4')//exit
            {
                return;
            }
            else
            {
                Console.WriteLine("Enter True Number");
                //Console.WriteLine(IfType);
                //Console.WriteLine(IfType.GetType());
                RunCmd();
            }
        }
    }
    class CMD
    {
        public string CMDEnv(string Code)
        {
            Process Scmd = new Process();
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            Scmd.StartInfo.FileName = "cmd.exe";
            Scmd.StartInfo.UseShellExecute = false;
            Scmd.StartInfo.RedirectStandardOutput = true;
            Scmd.StartInfo.RedirectStandardError = true;
            Scmd.StartInfo.RedirectStandardInput = true;
            //Scmd.StartInfo.Arguments = "runas /user:administrator";
            Scmd.Start();
            StreamReader sr = Scmd.StandardOutput;//get return value
            Scmd.StandardInput.WriteLine(Code);
            var ShowStatus = Scmd.StandardOutput.ReadToEnd();
            return (ShowStatus);
        }
    }
}
