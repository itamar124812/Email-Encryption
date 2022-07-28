using Intel.Dal;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace EmailEncryptionHost
{
    public class EmailTee
    {
        JhiSession session;
        Jhi jhi;
        CaClient client;
        private byte[] sendMessage(byte[] sendBuff, byte[] recvBuff, int cmdId, out int responseCode)
        {
            //Console.WriteLine("Performing send and receive operation.");
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            // Console.Out.WriteLine("Response buffer is " + UTF32Encoding.UTF8.GetString(recvBuff));          
            return recvBuff;
        }
        public byte [] genretePublicKey()
        {
            int responseCode;
            int cmdID = 1;
            byte[] sendBuff = new byte[1];
            byte[] recvBuff = new byte[2000];
            try
            {
               var pb= sendMessage(sendBuff, recvBuff, cmdID, out responseCode);
                client.SetMyPublicKey(pb);
                return pb;
            }
            catch(Exception)
            {
                throw new ApplicationException("Error was occurred when generate the keys.");
            }
        }
        public byte [] encreptAndSign(byte [] message,byte [] ReciverPK)
        {
            int responseCode;
            int cmdID = 2;
            byte[] messageLength = BitConverter.GetBytes(message.Length);
            byte[] sendBuff = Tools.ArrayUnionLength(message, ReciverPK);
            byte[] recvBuff = new byte[2000];
            try
            {
                return sendMessage(sendBuff, recvBuff, cmdID, out responseCode);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error was occurred when encrepting the email");
            }

        }
        public  byte [] descreptAndCheck(byte[] message,byte [] pbSender)
        {
            int responseCode;
            int cmdID = 3;
            byte[] sendBuff = Tools.ArrayUnionLength(message, pbSender);
            byte[] recvBuff = new byte[2000];
            try
            {
               byte [] result= sendMessage(sendBuff, recvBuff, cmdID, out responseCode);
                if (responseCode != -1) return result;
                else throw new ApplicationException("There is problems with authentication");
            }
            catch (Exception)
            {
                throw new ApplicationException("Error was occurred when decrepting the email");
            }

        }
        int setCAPK(byte [] pb)
        {
            int responseCode;
            int cmdID = 4;
            byte[] recvBuff = new byte[2000];
            try
            {
                sendMessage(pb, recvBuff, cmdID,out responseCode);
                return responseCode;
            }
            catch
            {
                return -1;
            }
        }
        
        public EmailTee(string email)
        {
            /*
            ProcessStartInfo peb = new ProcessStartInfo();
            peb.FileName = "cmd.exe";
            peb.WindowStyle = ProcessWindowStyle.Normal;
            peb.Arguments = @"/C Start C:\DALsdk\Tools\Emulauncher\Emulauncher.exe \n";
            Process.Start(peb);*/
            client = new CaClient(email);

#if AMULET
            // When compiled for Amulet the Jhi.DisableDllValidation flag is set to true 
            // in order to load the JHI.dll without DLL verification.
            // This is done because the JHI.dll is not in the regular JHI installation folder, 
            // and therefore will not be found by the JhiSharp.dll.
            // After disabling the .dll validation, the JHI.dll will be loaded using the Windows search path
            // and not by the JhiSharp.dll (see http://msdn.microsoft.com/en-us/library/7d83bc18(v=vs.100).aspx for 
            // details on the search path that is used by Windows to locate a DLL) 
            // In this case the JHI.dll will be loaded from the $(OutDir) folder (bin\Amulet by default),
            // which is the directory where the executable module for the current process is located.
            // The JHI.dll was placed in the bin\Amulet folder during project build.
            Console.WriteLine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Console.WriteLine(Directory.GetCurrentDirectory());
            Jhi.DisableDllValidation = true;
#endif
            
            jhi = Jhi.Instance;
            // This is the UUID of this Trusted Application (TA).
            //The UUID is the same value as the applet.id field in the Intel(R) DAL Trusted Application manifest.
            string appletID = "f284e9ad-5c24-43d4-9ff5-c4c60d4eacfe";
            // This is the path to the Intel Intel(R) DAL Trusted Application .dalp file that was created by the Intel(R) DAL Eclipse plug-in.
            string appletPath = @"C:\Users\USER\TEEdal\EmailEncryption\bin\EmailEncryption.dalp";

            // Install the Trusted Application
            Console.WriteLine("Installing the applet.");
            jhi.Install(appletID, appletPath);

            // Start a session with the Trusted Application
            byte[] initBuffer = new byte[] { }; // Data to send to the applet onInit function
            Console.WriteLine("Opening a session.");
            jhi.CreateSession(appletID, JHI_SESSION_FLAGS.None, initBuffer, out session);
        }


        public void closeConnection()
        {
            string appletID = "f284e9ad-5c24-43d4-9ff5-c4c60d4eacfe";

            // Close the session
            Console.WriteLine("Closing the session.");
            jhi.CloseSession(session);

            //Uninstall the Trusted Application
            Console.WriteLine("Uninstalling the applet.");
            jhi.Uninstall(appletID);

            Console.WriteLine("Press Enter to finish.");
            Console.Read();
        }
        void start(int choice)
        {
            
            byte[] pb = new byte[2];
            switch (choice)
                {
                    case 1:
                       pb= genretePublicKey();
                       client.SetMyPublicKey(pb);
                        break;
                    case 2:
                        setCAPK(client.CaCertificate.GetPublicKey());
                        break;
                    case 3:
                        byte[] message = Encoding.UTF8.GetBytes(".הודעה מגניבה");
                        DebugOnly(pb, message);
                        message=encreptAndSign(message, pb);
                        byte[] result=descreptAndCheck(message, pb);
                        string msg = Encoding.UTF8.GetString(result);
                        Console.WriteLine(msg);
                        break;
                    case 4:
                       
                        break;
                    default:
                        break;
                
            }          
            
        }
      
        private  void DebugOnly(byte[] pb, byte[] message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                if (i == 0 || i % 15 != 0 && i != message.Length)
                    Console.Write(" ");
                else if (i % 15 == 0 || i == message.Length) Console.WriteLine();
            }
            Console.WriteLine("public key:");
            for (int i = 0; i < pb.Length; i++)
            {
                Console.Write(pb[i]);
                if (i % 15 != 0 && i != pb.Length)
                    Console.Write(" ");
                else if (i % 15 == 0 || i == pb.Length) Console.WriteLine();
            }
        }

    }
}