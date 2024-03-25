using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace client_Csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteClient();
        }

        // ExecuteClient() Method
        static void ExecuteClient()
        {

            try
            {

                // Establish the remote endpoint 
                // for the socket. This example 
                // uses port 11111 on the local 
                // computer.
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                
                
                
                //IPAddress ipAddr = IPAddress.Parse("192.168.1.150");
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

                // Creation TCP/IP Socket using 
                // Socket Class Constructor
                Socket sender = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                try
                {

                    // Connect Socket to the remote 
                    // endpoint using method Connect()
                    sender.Connect(localEndPoint);

                    // We print EndPoint information 
                    // that we are connected
                    Console.WriteLine("Socket connected to -> {0} ",
                                  sender.RemoteEndPoint.ToString());

                    // Creation of message that
                    // we will send to Server


                    byte[] messageSent = Encoding.ASCII.GetBytes(@"H|\^&|||OCD^VISION^5.13.1.46935^JNumber|||||||P|LIS2-A|20210309021156P|1|090321-267545|||NGUYEN MY TIEN|||U||||||||||||||||||||||||||O|1|267545||DINH NHOM MAU ABO/Rh GELCARD|N|20210309020354|||||||||CENTBLOOD|||||||20210309091048|||F|||||R|1|ABO|A|||||F||ntpt^Automatic||20210309021155|JNumberR|2|Rh|POS|||||F||ntpt^Automatic||20210309021155|JNumberL<EOF>");







                    int byteSent = sender.Send(messageSent);

                    // Data buffer
                    byte[] messageReceived = new byte[10240];

                    // We receive the message using 
                    // the method Receive(). This 
                    // method returns number of bytes
                    // received, that we'll use to 
                    // convert them to string
                    int byteRecv = sender.Receive(messageReceived);
                    Console.WriteLine("Message from Server -> {0}",
                          Encoding.ASCII.GetString(messageReceived,
                                                     0, byteRecv));

                    // Close Socket using 
                    // the method Close()
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }

                // Manage of Socket's Exceptions
                catch (ArgumentNullException ane)
                {

                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }

                catch (SocketException se)
                {

                    Console.WriteLine("SocketException : {0}", se.ToString());
                }

                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }
    }
}
