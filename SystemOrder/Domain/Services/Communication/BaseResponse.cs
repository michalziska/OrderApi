﻿namespace SystemOrder.Domain.Services.Communication
{
	public abstract class BaseResponse<T> where T : class
	{
		public bool Success { get; private set; }
		public string Message { get; private set; }
		public T Resource { get; private set; }

		protected BaseResponse(T resource)
		{
			Success = true;
			Message = string.Empty;
			Resource = resource;
		}

		protected BaseResponse(string message)
		{
			Success = false;
			Message = message;
			Resource = default;
		}
	}
}
