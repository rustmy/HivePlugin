using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Oxide.Ext.Hive.Config;
using Oxide.Ext.Hive.Utils;

namespace Oxide.Ext.Hive.Net
{
	public class HiveNetHandler
	{
		private string server;
		private int port;
		private TcpClient client;
		private static HiveNetHandler obj;
		private Action<string> receiveListener;
		private bool running;
		private string EOT;
		private const byte EOTByte = 0x04;
		private bool connected = false;


		// Pending messages to Hive
		private Queue<byte[]> msgQueue;
		private bool reqLock;



		private HiveNetHandler(string url, int port)
		{
			server = url;
			this.port = port;
			msgQueue = new Queue<byte[]>();
			reqLock = false;
			EOT = Encoding.UTF8.GetString(new byte[] { 0x04 });
		}

		public void Connect()
		{
			connectInThread();
		}


		// Connect with HiveNet
		private void connectInThread()
		{
			// Reconnect
			try
			{
				client = new TcpClient(server, port);
				connected = true;
				OxideUtils.PrintSuccess("Hive","Connected to Hive Network");
			}
			catch (Exception)
			{
				OxideUtils.PrintError("Hive","Could not connect with HiveNetwork");
				connected = false;
				client = null;
				return;
			}

			// Connected
			Net.Requests.Hello req = new Net.Requests.Hello();
			SendHiveNetRequest(HiveNetHandler.createHiveNetReq(req.type, req));
		}

		// Init class
		public static HiveNetHandler getInstance(string url, int port)
		{
			if (obj == null)
			{
				obj = new HiveNetHandler(url, port);
				return obj;
			}
			else {
				return obj;
			}
		}

		// Init class
		public static HiveNetHandler getInstance()
		{
			return obj;
		}


		// Set callback listener
		public void SetOnReceive(Action<string> callback)
		{
			receiveListener = callback;
		}


		// 
		public void StartListening()
		{
			Thread t = new Thread(new ThreadStart(StartListenerInThread));
			t.Start();
		}



		// For thread use
		private void StartListenerInThread()
		{
			running = true;

			OxideUtils.Puts("Now listening for Hive!");

			while (running)
			{
				// Listener delay
				Thread.Sleep(1000);

				// Real-Time connection check
				if (connected)
				{
					connected = Connected();
				}

				// Check for connection
				if (!connected)
				{
					connectInThread();
					Thread.Sleep(1500);
				}
				else {
					try
					{
						if (client.Available > 0)
						{

							byte[] buf = new byte[16384];
							int pointer = 0;

							NetworkStream stream = client.GetStream();

							while (client.Available > 0)
							{
								byte cur = Convert.ToByte(stream.ReadByte());

								if (!cur.Equals(EOTByte))
								{
									buf[pointer] = cur;
									pointer++;
								}
								else {
									ThreadPool.QueueUserWorkItem(new WaitCallback(InvokeCall), Encoding.UTF8.GetString(buf, 0, pointer));
								}
							}
						}
					}
					catch (Exception e)
					{
						OxideUtils.PrintError("Hive","Error while listening to Hive");
						OxideUtils.PrintWarning(e.StackTrace);
					}
				}
			}
		}

		private void InvokeCall(object s)
		{
			receiveListener.Invoke(s.ToString());
		}

		// Checks if the theres a connection with the server and reconnects async if wanted
		public bool Connected()
		{
			bool part1 = client.Client.Poll(1000, SelectMode.SelectRead);
			bool part2 = (client.Client.Available == 0);
			if (part1 && part2)
			{
				OxideUtils.PrintError("Hive","No connection to HiveNet. Trying to reconnect...");


				return false;
			}

			else
				return true;
		}


		// Stopping the Listener
		public void stop()
		{
			running = false;
		}


		/// <summary>
		/// Adds a new request to the request queue
		/// </summary>
		/// <param name="req">Req.</param>
		public void SendHiveNetRequest(string req)
		{
			OxideUtils.Puts("Adding the following request to the queue: " + req);

			string msg = req + EOT;

			msgQueue.Enqueue(Encoding.UTF8.GetBytes(msg));

			if (!reqLock)
			{
				reqLock = true;
				new Thread(new ThreadStart(SendRequestInThread)).Start();
			}
		}


		// Works on requests to send them to HiveNet
		private void SendRequestInThread()
		{

			while (msgQueue.Count > 0)
			{
				// No connection
				if (!connected)
				{
					continue;
				}

				byte[] arr = msgQueue.Dequeue();

				try
				{
					client.GetStream().Write(arr, 0, arr.Length);
				}
				catch (SocketException ex)
				{
					OxideUtils.PrintError("Hive","Couldn't write to HiveNet");
					OxideUtils.PrintWarning(ex.StackTrace);
				}
				finally
				{
					reqLock = false;
				}

			}
			reqLock = false;
		}


		// Creates a HiveNet request
		public static string createHiveNetReq(string type, object obj, string id = "-1")
		{
			Dictionary<string, string> header = new Dictionary<string, string>();
			header.Add("Hivename", OxideConfiguration.getConfigKey("hive", "hive_name"));
			header.Add("Password", OxideConfiguration.getConfigKey("hive", "hive_password"));
			header.Add("Port", ConVar.Server.port.ToString());
			header.Add("ID", id);
			header.Add("Type", type);

			string body = JsonConvert.SerializeObject(obj);

			string msg = JsonConvert.SerializeObject(new HiveNetPackage(header, body));

			return msg;
		}
	}
}
