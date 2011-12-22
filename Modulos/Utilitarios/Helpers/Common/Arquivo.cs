using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Swarm.Utilitarios
{
    public abstract class Arquivo
    {
        public static bool Existe(string path, string nome)
        {
            if (!path.EndsWith("/")) path = string.Concat(path, "/");
            string pathCompleto = string.Concat(path, nome);

            return Existe(pathCompleto);
        }

        public static bool Existe(string pathCompleto)
        {
            return File.Exists(pathCompleto);
        }
    }
}
