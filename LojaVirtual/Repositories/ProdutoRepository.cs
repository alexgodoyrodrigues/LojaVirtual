﻿using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private IConfiguration _configuration;
        private LojaVirtualContext _banco;

        public ProdutoRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _configuration = configuration;
        }

        public void Atualizar(Produto produto)
        {
            _banco.Update(produto);
            _banco.SaveChanges();
        }

        public void Cadastrar(Produto produto)
        {
            _banco.Add(produto);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Produto produto = ObterProduto(Id);
            _banco.Remove(produto);
            _banco.SaveChanges();
        }

        public Produto ObterProduto(int Id)
        {
            return _banco.Produtos.Include(a => a.Imagens).Where(a => a.Id == Id).FirstOrDefault();
        }

        public IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa)
        {
            int NumeroPagina = pagina ?? 1;

            var bancoProduto = _banco.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                bancoProduto = bancoProduto.Where(a => a.Nome.Contains(pesquisa.Trim()));
            }

            return bancoProduto.Include(a => a.Imagens).ToPagedList<Produto>(NumeroPagina, _configuration.GetValue<int>("RegistroPorPagina"));
        }
    }
}
