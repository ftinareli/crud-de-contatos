using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            // Se usuário estiver logado, redireciona para home
            if (_sessao.BuscarSessao() != null)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessao();
            return RedirectToAction(nameof(Index), "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.ValidaSenha(loginModel.Senha))
                        {
                            _sessao.CriarSessao(usuario);
                            return RedirectToAction(nameof(Index), "Home");
                        }

                        TempData["MensagemErro"] = "Senha inválida. Por favor, tente novamente!";
                    }

                    TempData["MensagemErro"] = "Usuário e/ou senha inválido(s). Por favor, tente novamente!";
                }

                return View(nameof(Index));
            }
            catch (Exception error)
            {
                TempData["MensagemErro"] = $"Erro ao entrar, tente novamente! Detalhe do erro: {error.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
