using Intel.Dal;
using System;
using System.IO;
using System.Text;

namespace EmailEncryptionHost
{
    class Program
    {
        static JhiSession session;
        static Jhi jhi;
        private static byte[] sendMessage(byte[] sendBuff, byte[] recvBuff, int cmdId, out int responseCode)
        {
            //Console.WriteLine("Performing send and receive operation.");
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            // Console.Out.WriteLine("Response buffer is " + UTF32Encoding.UTF8.GetString(recvBuff));          
            return recvBuff;
        }
        static byte [] genretePublicKey()
        {
            int responseCode;
            int cmdID = 1;
            byte[] sendBuff = new byte[1];
            byte[] recvBuff = new byte[2000];
            try
            {
                return sendMessage(sendBuff, recvBuff, cmdID, out responseCode);
            }
            catch(Exception)
            {
                throw new ApplicationException("Error was occurred when generate the keys.");
            }
        }
        static byte [] encreptAndSign(byte [] message,byte [] ReciverPK)
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
        static byte [] descreptAndCheck(byte[] message,byte [] pbSender)
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
        static int setCAPK(byte [] pb)
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
        static void Main(string[] args)
        {
            CaClient client = new CaClient(@"itamarit@jct.ac.il");
            
            int choice = 0;
            byte[] pb=new byte[2];
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
            while(true)
            {
                Console.WriteLine("please enter a number:\n  1. for generating keys.\n  2. for getting your public key.\n  3. for encrypt some message.\n  4. for decrypt some message.\nany other key for getting out.");
                try
                {
                    choice=int.Parse(Console.ReadLine());
                }              
                catch(Exception)
                {
                    break;
                }
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
            // Close the session
            Console.WriteLine("Closing the session.");
            jhi.CloseSession(session);

            //Uninstall the Trusted Application
            Console.WriteLine("Uninstalling the applet.");
            jhi.Uninstall(appletID);

            Console.WriteLine("Press Enter to finish.");
            Console.Read();
        }
      
        private static void DebugOnly(byte[] pb, byte[] message)
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