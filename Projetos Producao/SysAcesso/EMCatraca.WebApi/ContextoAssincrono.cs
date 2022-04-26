using System;
using System.Threading;
using System.Threading.Tasks;
using TeraByte.Core;

namespace EMCatraca.WebApi
{
	public static class ContextoAssincrono
	{
		public static Task ExecuteTarefa(Action acaoTarefa)
		{
			return Task.Factory.StartNew(() =>
			{
				try
				{
					acaoTarefa();
				}
				catch (ThreadAbortException)
				{
					//
				}
			});
		}

		public static Task<T> ExecuteTarefa<T>(Func<T> acaoTarefa)
		{
			return Task.Factory.StartNew(() =>
			{
				try
				{
					return acaoTarefa();
				}
				catch (ThreadAbortException)
				{
					//
				}

				return default;
			});
		}
	}
}