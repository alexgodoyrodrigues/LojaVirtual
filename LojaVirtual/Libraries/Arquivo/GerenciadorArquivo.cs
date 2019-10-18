using LojaVirtual.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Arquivo
{
    public class GerenciadorArquivo
    {
        public static string CadastrarImagemProduto(IFormFile file)
        {
            var NomeArquivo = Path.GetFileName(file.FileName);

            var Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", NomeArquivo);

            using (var stream = new FileStream(Caminho, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine("/uploads/temp", NomeArquivo).Replace("\\", "/");
        }

        public static bool ExcluirImagemProduto(string caminho)
        {
            string Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminho.TrimStart('/'));

            if (File.Exists(Caminho))
            {
                File.Delete(Caminho);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Imagem> MoverImagensProduto(List<string> ListaCaminhoTemp, int ProdutoId)
        {
            var CaminhoDefinitivoPastaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", ProdutoId.ToString());

            if (! Directory.Exists(CaminhoDefinitivoPastaProduto))
            {
                Directory.CreateDirectory(CaminhoDefinitivoPastaProduto);
            }

            List<Imagem> ListaImagensDef = new List<Imagem>();

            foreach (var CaminhoTemp in ListaCaminhoTemp)
            {
                if (! string.IsNullOrEmpty(CaminhoTemp))
                {
                    var nomeArquivo = Path.GetFileName(CaminhoTemp);
                    var caminhoAbsolutoTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", nomeArquivo);

                    var caminhoAbsolutoDef = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", ProdutoId.ToString(), nomeArquivo);

                    if (File.Exists(caminhoAbsolutoTemp))
                    {
                        File.Copy(caminhoAbsolutoTemp, caminhoAbsolutoDef);

                        if (File.Exists(caminhoAbsolutoDef))
                        {
                            File.Delete(caminhoAbsolutoTemp);
                        }

                        ListaImagensDef.Add(new Imagem() { Caminho = Path.Combine("/uploads", ProdutoId.ToString(), nomeArquivo).Replace("\\", "/"), ProdutoId = ProdutoId});
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return ListaImagensDef;
        }
    }
}
