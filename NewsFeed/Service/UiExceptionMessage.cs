using System;
using System.Collections.Generic;

namespace Api.Service {
	public static class UiExceptionMessage {
		public enum Error {
			InputParseFailure
		};

		static Dictionary<Error, string> errorMapping = new Dictionary<Error, string>() {
			{  Error.InputParseFailure, "Unable to parse input" }
		};

		public static string Message(Error error, Exception exception) {
#if DEBUG
			return exception.Message;
#else
			if (errorMapping.TryGetValue(error, out var message)) {
				return message;
			}
			return "Badness.";
#endif
		}
	}
}
