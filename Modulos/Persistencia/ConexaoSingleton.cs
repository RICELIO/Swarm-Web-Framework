﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Utilitarios.Library.Seguranca.Criptografia;

namespace Swarm.Persistencia
{
    public sealed class Conexão
    {
        #region Atributos

        private volatile static Conexão instance;
        private static object syncRoot = new Object();

        #endregion

        #region Propriedades

        private string Servidor { get; set; }
        private string BancodeDados { get; set; }
        private string Usuario { get; set; }
        private string Senha { get; set; }
        public EnumPersistencia.SGBD SGBD { get; private set; }

        public static Conexão Instance
        {
            get
            {
                if (Checar.IsNull(instance))
                {
                    lock (syncRoot)
                        instance = new Conexão();
                }
                return instance;
            }
        }

        #endregion

        private Conexão()
        {
            Criptografia objCriptografia = new CriptografiaDES3();
            this.Servidor = objCriptografia.Descriptografar(Configuracao.Obter("Servidor"));
            this.BancodeDados = objCriptografia.Descriptografar(Configuracao.Obter("BancoDeDados"));
            this.Usuario = objCriptografia.Descriptografar(Configuracao.Obter("Usuario"));
            this.Senha = objCriptografia.Descriptografar(Configuracao.Obter("Senha"));
            this.SGBD = (EnumPersistencia.SGBD)Conversoes.ToInt32(Configuracao.Obter("SGBD"));
        }

        #region Métodos

        public string GetConfiguracoes()
        {
            return string.Format(SGBDFactory.Criar().GetStringdeConexao(), this.Servidor, this.BancodeDados, this.Usuario, this.Senha);
        }

        public string GetLimitadordeRegistrosSQL(int registros)
        {
            return string.Format(SGBDFactory.Criar().GetSqlLimitadordeRegistros(registros));
        }

        #endregion
    }
}
