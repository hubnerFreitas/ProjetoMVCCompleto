using AppMvc.Models;
using AutoMapper;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.App.Controllers
{

    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _forncedorRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository,
                                        IMapper mapper)
        {
            _forncedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _forncedorRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null)
                return NotFound();

            return View(fornecedorViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _forncedorRepository.Adicionar(fornecedor);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);
            if (fornecedorViewModel == null)
                return NotFound();

            return View(fornecedorViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();

            if (ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _forncedorRepository.Atualizar(fornecedor);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null) return NotFound();

            await _forncedorRepository.Remover(id);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if (fornecedor == null) return NotFound();
            return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _forncedorRepository.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _forncedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}
