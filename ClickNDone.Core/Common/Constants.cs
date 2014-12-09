using System;

namespace ClickNDone.Core
{


	public enum UserType
	{
		CONSUMER,
		SUPPLIER
	};

	public enum ServiceState 
	{
		UNKNOWN=0, 
		ABIERTO=1, 
		CONFIRMADO=2, 
		FINALIZADO=3, 
		TIME_OUT_PROVEEDOR=4, 
		RECHAZADO_PROVEEDOR=5, 
		RECHAZADO_USUARIO=6, 
		ORDEN_INICIADA=7
	};

	public class Constants
	{
		public static int GET_ORDER_STATUS_WAIT_TIME = 10000; //Seconds
		public static int GET_ORDER_STATUS_ATTEMPTS = 100; //Calls
		public const string WebServiceHost = "http://click-n-done.com/WebApp/service/api.php?rquest=";
	}
}

