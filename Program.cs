using Sres.Net.EEIP;

namespace SMCTest
{
    class Program
    {
	static void Main(string[] args)
	    {
		Console.WriteLine("=== Register session === ");
		EEIPClient e = new EEIPClient();
		string ip = "10.32.100.121";
		if (args.Length == 1)
		    ip = args[0];
		e.RegisterSession(ip);

		Console.WriteLine("=== Explicit messaging === ");
		Console.WriteLine(BitConverter.ToString(e.GetAttributeSingle(0x66, 0x01, 0x65)));

		Console.WriteLine("=== Open implicit messaging === ");
		e.ForwardOpen();
		Console.WriteLine("=== Turn valves on === ");
		byte[] bs = new Byte[e.T_O_Length];
		for (int i = 0; i < e.T_O_Length; i++)
		    bs[i] = 0xFF;
		e.T_O_IOData = bs;
		e.O_T_IOData = bs;
		System.Threading.Thread.Sleep(500);
		Console.WriteLine("=== Turn valves off === ");
		for (int i = 0; i < e.T_O_Length; i++)
		    bs[i] = 0x00;
		e.T_O_IOData = bs;
		e.O_T_IOData = bs;
		Console.WriteLine("=== Close implicit messaging === ");
		e.ForwardClose();

		Console.WriteLine("=== Unregister session === ");
		e.UnRegisterSession();
	    }
    }
}
