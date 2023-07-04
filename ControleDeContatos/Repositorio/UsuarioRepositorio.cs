using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _context;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _context = bancoContext;
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _context.Usuarios.ToList();
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            var usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) throw new System.Exception("Houve um erro na atualização do Usuário!");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            var usuarioDB = ListarPorId(id);

            if (usuarioDB == null) throw new System.Exception("Houve um erro na exclusão do Usuário!");

            _context.Usuarios.Remove(usuarioDB);
            _context.SaveChanges();

            return true;
        }
    }
}
