using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Mercado.Model;
using System.Threading.Tasks;

namespace Mercado.Helper
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection conexao;

        public SQLiteDatabaseHelper(string path)
        {
            conexao = new SQLiteAsyncConnection(path);
            conexao.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return conexao.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            string sql ="UPDATE produto Set Descricao=?, Quantidade=?, Preco=? WHERE id=?";
            return conexao.QueryAsync<Produto>(sql, p.descricao, p.quantidade, p.preco, p.id);
        }
        

        //Não precisa do getbyid pq o select já vai selecionar todos
        public Task<List<Produto>> select()
        {
            return conexao.Table<Produto>().ToListAsync();
        }

        public Task<int> Delete(int id)
        {
            return conexao.Table<Produto>().DeleteAsync(i => i.id == id);
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM produto WHERE Descricao LIKE '%" + q + "%' ";
           return conexao.QueryAsync<Produto>(sql);
        }
    }
}
