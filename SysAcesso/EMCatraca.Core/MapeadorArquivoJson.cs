using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Utilidades;
using Newtonsoft.Json;
using System;
using System.IO;

namespace EMCatraca.Core
{
	public static class MapeadorArquivoJson
	{
		private static string _nomeLog;

        public static T CarreguerArquivoJson<T>(string arquivo) where T : new()
		{
			var caminhoArquivo = 
				$"{System.AppDomain.CurrentDomain.BaseDirectory}" +
				$"{arquivo}";

			_nomeLog = arquivo;

			LogAuditoria.Escreva($"Caminho do arquivo CFG: {caminhoArquivo}", 
				nameof(MapeadorArquivoJson));

			if (!File.Exists(caminhoArquivo))
			{
				LogAuditoria.Escreva("Caminho do Arquivo não existe verificar se CFG Acesso", 
					nameof(MapeadorArquivoJson));

				if (typeof(T).Name == "Acesso")
				{
					LogAuditoria.Escreva($"E um CFG de Acesso", nameof(MapeadorArquivoJson));
					var acesso = new RegrasAcesso();

					return (T)Convert.ChangeType(acesso.Cria(), typeof(T));
				}

				return new T();
			}

			if (typeof(T).Name == "InformacaoConexao")
			{
				var json = File.ReadAllText(caminhoArquivo);
				return JsonConvert.DeserializeObject<T>(json);
			}
			else
			{
				var json = File.ReadAllBytes(caminhoArquivo).Decriptografa();

				//LogGeral.WriteLog($"2.0.5-Json Deserializado com sucesso", _nomeLog);
				return JsonConvert.DeserializeObject<T>(json);
			}
		}

		public static T CarreguerJson<T>(string arquivo, ConfiguracoesDto paramentros) where T : new()
		{
			var caminhoArquivo = $"{AppDomain.CurrentDomain.BaseDirectory}{arquivo}";
			_nomeLog = arquivo;

			LogAuditoria.Escreva($"Caminho do arquivo CFG: {caminhoArquivo}", nameof(MapeadorArquivoJson));


			if (!File.Exists(caminhoArquivo))
			{
				LogAuditoria.Escreva("Caminho do Arquivo não existe verificar se CFG Acesso", 
					nameof(MapeadorArquivoJson));

				if (typeof(T).Name == "Acesso")
				{
					LogAuditoria.Escreva($"E um CFG de Acesso", 
						nameof(MapeadorArquivoJson));

					var acesso = new RegrasAcesso();

					return (T)Convert.ChangeType(acesso.Cria(), typeof(T));
				}

				if (typeof(T).Name == "CustomizacaoTipoPessoa")
                {
					LogAuditoria.Escreva($"Cria arquivo CFG de Customizacao Tipo Pessoa",
						nameof(MapeadorArquivoJson));

					var customizacaoTipoPessoa = new CustomizacaoTipoPessoa();

					var json = JsonConvert.SerializeObject(customizacaoTipoPessoa.Cria());

					File.WriteAllBytes(caminhoArquivo, json.Criptografa());

					json = File.ReadAllBytes(caminhoArquivo).Decriptografa();

					return JsonConvert.DeserializeObject<T>(json);
				}

				return new T();
			}

			if (typeof(T).Name == "InformacaoConexao")
			{
				var json = File.ReadAllText(caminhoArquivo);

				if (paramentros.EhDesenvolvedor)
				{
					return new T();
				}

				return JsonConvert.DeserializeObject<T>(json);
			}
			else
			{
				var json = File.ReadAllBytes(caminhoArquivo).Decriptografa();
					
				//LogAuditoria.Escreva("Json Deserializado com sucesso", nameof(MapeadorArquivoJson));
				return JsonConvert.DeserializeObject<T>(json);
			}
		}

		public static void Gravar<T>(string c, T classe)
		{
			LogAuditoria.Escreva($"Iniciando o processo de salvar arquivo CFG",
				nameof(MapeadorArquivoJson));

			var path = Path.Combine(Environment.CurrentDirectory, c);
			LogAuditoria.Escreva($"CFG:{c} sera salvo no Caminho:{path}", 
				nameof(MapeadorArquivoJson));

			var json = JsonConvert.SerializeObject(classe);
			LogAuditoria.Escreva($"Foi serializado o CFG:{c} {json}", 
				nameof(MapeadorArquivoJson));

			if (classe.GetType().Name == "InformacaoConexao")
			{
				LogAuditoria.Escreva($"CFG Json emcatraca.servidor salvar descriptografado: |{json}|",
					nameof(MapeadorArquivoJson));

				File.WriteAllText(path, json);
			}
			else
			{
				File.WriteAllBytes(path, json.Criptografa());
				LogAuditoria.Escreva($"CFG {c} foi Criptografado", 
					nameof(MapeadorArquivoJson));
			}
		}

		public static void BackUpConfiguracao()
		{
			LogAuditoria.Escreva($"Iniciando o processo de BackUp CFG", 
				nameof(MapeadorArquivoJson));
			var destino = Path.Combine(Environment.CurrentDirectory, "BackUpConfiguracao");

			destino = Path.Combine(destino, DateTime.Now.ToString("yyyyMMdd_HHmm"));
			LogAuditoria.Escreva($"BackUp sera salvo no Destino:{destino}", 
				nameof(MapeadorArquivoJson));

			if (!Directory.Exists(destino))
			{
				LogAuditoria.Escreva($"Criando o diretorio do BackUp", nameof(MapeadorArquivoJson));
				Directory.CreateDirectory(destino);
			}

			foreach (var origem in Directory.GetFiles(Environment.CurrentDirectory, "*.cfg"))
			{
				var destinoFinal = Path.Combine(destino, Path.GetFileName(origem));
				File.Copy(origem, destinoFinal, true);
				LogAuditoria.Escreva($"O processo de BackUp execultado com sucesso!", 
					nameof(MapeadorArquivoJson));
			}
		}
	}
}
