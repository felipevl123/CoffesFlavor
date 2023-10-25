using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoffesFlavor.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IActionResult List(string categoria)
        {
            //ViewData["Titulo"] = "Todos os Produtos";
            //ViewData["Data"] = DateTime.Now;
            //var produtos = _produtoRepository.Produtos;
            //var totalProdutos = produtos.Count();
            //ViewBag.Total = "Total de produtos";
            //ViewBag.TotalProdutos = totalProdutos;
            //return View(produtos);

            IEnumerable<Produto> produtos;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrWhiteSpace(categoria))
            {
                produtos = _produtoRepository.Produtos.OrderBy(p => p.ProdutoId);
                categoriaAtual = "Todos os produtos";
            }
            else
            {
                //if(string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                //{
                //    produtos = _produtoRepository.Produtos
                //        .Where(p => p.Categoria.CategoriaNome.Equals("Normal"))
                //        .OrderBy(p => p.Nome);
                //}
                //else
                //{
                //    produtos = _produtoRepository.Produtos
                //        .Where(p => p.Categoria.CategoriaNome.Equals("Natural"))
                //        .OrderBy(p => p.Nome);
                //}

                produtos = _produtoRepository.Produtos
                    .Where(c => c.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(c => c.Nome);

                categoriaAtual = categoria;
            }

            var produtoListViewModel = new ProdutoListViewModel
            {
                Produtos = produtos,
                CategoriaAtual = categoriaAtual,
            };
            

            return View(produtoListViewModel);

        }

        public IActionResult Details(int produtoId)
        {
            var produto = _produtoRepository.Produtos
                .FirstOrDefault(p => p.ProdutoId == produtoId);

            return View(produto);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Produto> produtos;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                produtos = _produtoRepository.Produtos.OrderBy(p => p.ProdutoId);
                categoriaAtual = "Todos os Produtos";
            }
            else
            {
                produtos = _produtoRepository.Produtos
                    .Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));
                if (produtos.Any())
                    categoriaAtual = "Produtos";
                else
                    categoriaAtual = "Nenhum lanche foi encontrado";
            }



            return View("~/Views/Produto/List.cshtml", new ProdutoListViewModel
            {
                Produtos= produtos,
                CategoriaAtual= categoriaAtual,
            });
        }

    }
}
