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
        string AdpadapterName;
        string LenovoIP = "192.168.70.120";
        string HuaWeiIP = "192.168.2.99";
        string DellIP = "192.168.0.99";       
        string NetMask24 = "255.255.255.0";
        public void RunCmd()
        {
            //Console.WriteLine("Chose your language:\n1.Enlish\n2.中文");
            Console.Clear();
            Console.WriteLine("0.快速维护工具箱");
            Console.WriteLine("1.DHCP");
            Console.WriteLine("2.静态IP");
            Console.WriteLine("3.显示网卡信息");
            Console.WriteLine("4.设置默认适配器");
            Console.WriteLine(AdpadapterName);
            IfType();           
        }
        public void IfType()
            {
            if (AdpadapterName == "")
            {
                AdpadapterName = "以太网";
            }
            ConsoleKeyInfo IfType = Console.ReadKey();
            if (IfType.KeyChar == '0')
            {
                Console.Clear();
                Console.WriteLine("快速维护工具箱\n");
              //Console.WriteLine("1.快速设置IP为192.168.2.99/24,并开启DHCP服务----主要用于连接未经过配置的HP_ILO服务器管理口");
                Console.WriteLine("2.快速设置IP为192.168.70.120/24,----主要用户连接IBM/Lenove的服务器管理口IMM(192.168.70.125)\n");
                Console.WriteLine("3.快速设置IP为192.168.2.99/25,-----主要用于连接华为服务器管理口MG(192.168.2.100)\n");
                Console.WriteLine("4.快速设置IP为192.168.0.99/24,----主要用于连接DELL服务器管理口IDRAC(192.168.0.12)\n");
                Console.WriteLine("0.返回");
                Console.ReadKey();
                //if (IfType.KeyChar == '1')
                //{
                //}
                if (IfType.KeyChar == '2')
                {
                    Console.WriteLine(Rcmd.CMDEnv("netsh interface ip set address \"" + AdpadapterName + "\" static" + " " + LenovoIP + " " + NetMask24 + "&exit"));
                }
                if (IfType.KeyChar == '3')
                {
                    Console.WriteLine(Rcmd.CMDEnv("netsh interface ip set address \"" + AdpadapterName + "\" static" + " " + HuaWeiIP + " " + NetMask24 + "&exit"));
                }
                if (IfType.KeyChar == '4')
                {
                    Console.WriteLine(Rcmd.CMDEnv("netsh interface ip set address \"" + AdpadapterName + "\" static" + " " + DellIP + " " + NetMask24 + "&exit"));
                }
                if (IfType.KeyChar =='0')
                {
                    RunCmd();
                }
            }          
            if (IfType.KeyChar == '1')//DHCP
            {
                Console.Clear();
                Console.WriteLine(Rcmd.CMDEnv("netsh interface ip set address \""+AdpadapterName+"\" DHCP" + "&exit"));//这里的"以太网"根据实际网络适配器名字设置
                Console.ReadKey();
            }
            if (IfType.KeyChar == '2')//静态IP
            {
                Console.Clear();
                Console.WriteLine("设置静态IP");
                Console.WriteLine("输入你的地址然后回车,默认 192.168.1.100");
                var ReadIP = Console.ReadLine();
                if (ReadIP == "")
                {
                    ReadIP = "192.168.1.100";
                }
                Console.WriteLine("输入你的子网掩码,默认 255.255.255.0");
                var ReadNetmask = Console.ReadLine();
                if (ReadNetmask == "")
                {
                    ReadNetmask = "255.255.255.0";
                }
                Console.WriteLine("输入你的网关,默认 192.168.1.1");
                var ReadGateWay = Console.ReadLine();
                if (ReadGateWay == "")
                {
                    ReadGateWay = "192.168.1.1";
                }
                Console.Clear();
                Console.WriteLine("IP地址 : " + ReadIP + "\n子网掩码: " + ReadNetmask + "\n网关: " + ReadGateWay);
                Console.WriteLine("你确定吗? Y/n");
                ConsoleKeyInfo IfYN = Console.ReadKey();
                if (IfYN.Key == ConsoleKey.Enter)
                { 
                    Console.WriteLine(Rcmd.CMDEnv("netsh interface ip set address \""+AdpadapterName+"\" static" + " " + ReadIP + " " + ReadNetmask + " " + ReadGateWay + "&exit"));//English Language System (netsh interface ip set address \"Ethernet\" static" + " " + IPaddr + " " + Netmask + " " + Gateway + "&exit")
                    Console.WriteLine(Rcmd.CMDEnv("ipconfig" + "&exit"));
                    Console.ReadKey();
                }
                if (IfYN.KeyChar == 'y')
                {
                    Console.WriteLine(Rcmd.CMDEnv("netsh interface ip set address \""+AdpadapterName+"\" static" + " " + ReadIP + " " + ReadNetmask + " " + ReadGateWay + "&exit"));
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
                Console.Clear();
                Console.WriteLine("请复制默认网络适配器的名字在这里");
                AdpadapterName = Console.ReadLine();
                RunCmd();
            }
            if (IfType.KeyChar == '5')
            {
                return;
            }
            else
            {
                Console.WriteLine("请输入正确的数字");
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
