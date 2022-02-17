using System;
using System.Runtime.Serialization;

namespace KTA.Model.ExceptionHelper
{
	[Serializable]
	public class DataValidateFailedException: Exception, ISerializable
	{
		public DataValidateFailedException(string message)
			: base(message) { }
		public DataValidateFailedException(string message, Exception inner)
			: base(message, inner) { }
		protected DataValidateFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }		
	}
}
