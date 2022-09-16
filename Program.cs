using Sres.Net.EEIP;

namespace SMCTest
{
    class Program
    {
	static void Main(string[] args)
	    {
		Console.WriteLine("=== Register session === ");
		EEIPClient e = new EEIPClient();
		string ip = "10.32.100.108";
		e.TCPPort = 49666;
		e.T_O_Length = 4;
		e.O_T_Length = 2;
		if (args.Length == 1)
		    ip = args[0];
		e.RegisterSession(ip);

		Console.WriteLine("=== Explicit messaging === ");
		// Console.WriteLine(BitConverter.ToString(e.GetAttributeSingle(0x66, 0x01, 0x6D)));

		Console.WriteLine("=== Open implicit messaging === ");
		e.ForwardOpen();
		
		Console.WriteLine("=== Read data ===");
		Console.WriteLine(BitConverter.ToString(e.O_T_IOData));
		Console.WriteLine(BitConverter.ToString(e.T_O_IOData));
		
		Console.WriteLine("=== Turn valves on === ");
		for (int i = 0; i < e.T_O_IOData.Length; i++)
		    e.T_O_IOData[i] = 0xFF;
		for (int i = 0; i < e.O_T_IOData.Length; i++)
		    e.O_T_IOData[i] = 0xFF;
		Console.WriteLine(BitConverter.ToString(e.O_T_IOData));
		Console.WriteLine(BitConverter.ToString(e.T_O_IOData));
		System.Threading.Thread.Sleep(1000);
		
		Console.WriteLine("=== Turn valves off === ");
		for (int i = 0; i < e.T_O_IOData.Length; i++)
		    e.T_O_IOData[i] = 0x00;
		for (int i = 0; i < e.O_T_IOData.Length; i++)
		    e.O_T_IOData[i] = 0x00;
		
		Console.WriteLine("=== Close implicit messaging === ");
		e.ForwardClose();

		Console.WriteLine("=== Unregister session === ");
		Console.WriteLine(e.TCPPort);
		e.UnRegisterSession();
	    }
    }
}
