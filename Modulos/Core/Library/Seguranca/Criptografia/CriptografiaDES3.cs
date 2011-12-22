using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Swarm.Utilitarios;

namespace Swarm.Core.Library.Seguranca.Criptografia
{
    public class CriptografiaDES3 : Criptografia
    {
        private readonly string Chave = "F@mil1a-M.S-@m0r"; // Chave base para criptografia

        public CriptografiaDES3()
            : base(EnumCriptografia.Tipo.DES3)
        {
        }

        #region Métodos

        public override string Criptografar(string texto)
        {
            TripleDESCryptoServiceProvider provider = this.ProviderDES3();
            byte[] bytes = base.Encoder.GetBytes(texto);
            return Conversoes.ToBase64String(provider.CreateEncryptor().TransformFinalBlock(bytes, Valor.Zero, bytes.Length));
        }

        public override string Descriptografar(string texto)
        {
            TripleDESCryptoServiceProvider provider = this.ProviderDES3();
            byte[] inputBuffer = Conversoes.FromBase64String(texto);
            return Encoder.GetString(provider.CreateDecryptor().TransformFinalBlock(inputBuffer, Valor.Zero, inputBuffer.Length));
        }

        public override bool Comparar(string texto, string hash)
        {
            string textoCriptografado = this.Criptografar(texto);
            return textoCriptografado.Equals(hash);
        }

        private TripleDESCryptoServiceProvider ProviderDES3()
        {
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            provider.Key = this.Encoder.GetBytes(this.Chave.ToCharArray());
            provider.Mode = CipherMode.ECB;
            return provider;
        }

        #endregion
    }
}
