using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ClienteRepository : IClienteRepository
    {

        private IConfiguration _configuration;
        private LojaVirtualContext _banco;

        public ClienteRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _configuration = configuration;
        }

        public void Atualizar(Cliente cliente)
        {
            _banco.Clientes.Update(cliente);
            _banco.SaveChanges();
        }

        public void Cadastrar(Cliente cliente)
        {
            _banco.Clientes.Add(cliente);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Cliente cliente = ObterCliente(Id);

            _banco.Clientes.Remove(cliente);
            _banco.SaveChanges();
        }

        public Cliente Login(string Email, string senha)
        {
            Cliente cliente = _banco.Clientes.Where(m => m.Email == Email && m.Senha == senha).FirstOrDefault();

            return cliente;
        }

        public Cliente ObterCliente(int Id)
        {
            return _banco.Clientes.Find(Id);
        }

        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa)
        {
            int NumeroPagina = pagina ?? 1;

            var bancoCliente = _banco.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                bancoCliente = bancoCliente.Where(a => a.Nome.Contains(pesquisa.Trim()) || a.Email.Contains(pesquisa.Trim()));
            }

            return bancoCliente.ToPagedList<Cliente>(NumeroPagina, _configuration.GetValue<int>("RegistroPorPagina"));
        }
    }
}
