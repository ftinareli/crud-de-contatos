using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ControleDeContatos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            var usuarios = _usuarioRepositorio.BuscarTodos();

            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }

                return View(usuario);
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Erro ao cadastrar usuário, tente novamente! Detalhe do erro: {error.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Editar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário editado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }

                return View(usuario);
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Erro ao editar usuário, tente novamente! Detalhe do erro: {error.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
