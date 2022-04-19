using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PruebaClinica.Interfaces;
using PruebaClinica.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaClinica.Controllers
{
    public class MedicamentoController : Controller
    {
        private readonly IClinicaRepository _clinicaRepository;
        public MedicamentoController(IClinicaRepository clinicaRepository)
        {
            _clinicaRepository = clinicaRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Medicamento> medicamentos;
            try
            {
                medicamentos = _clinicaRepository.GetMedicamentos();

            }
            catch (Exception)
            {

                medicamentos = Enumerable.Empty<Medicamento>();
            }
            return View(medicamentos);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medicamento medicamento)
        {
            try
            {
                if (!_clinicaRepository.InsertMedicamentos(medicamento).Result)
                {
                    ModelState.AddModelError(string.Empty, "Se presentaron errores al registrar");
                    return View("Create", medicamento);
                    
                }
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Se presentaron errores al registrar");
                return View("Create", medicamento);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Medicamento medicamento = new Medicamento();
                medicamento = _clinicaRepository.GetMedicamento((int)id).Result;
                return View(medicamento);
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Se presentaron errores al recuperar el medicamento seleccionado");
                return RedirectToAction("Index");
            }

        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Medicamento medicamento)
        {
            try
            {
                Medicamento medicamentoActualizado = new Medicamento();
                medicamentoActualizado = _clinicaRepository.UpdateMedicamento(medicamento).Result;
                if (medicamentoActualizado == null) throw new Exception();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Se presentaron errores al editar");
                return View("Edit", medicamento);
            }


        }


        public IActionResult Dispatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Medicamento medicamento = new Medicamento();
                medicamento = _clinicaRepository.GetMedicamento((int)id).Result;
                return View(medicamento);
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Se presentaron errores al recuperar el medicamento seleccionado");
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dispatch(Medicamento medicamento)
        {
            try
            {
                int cantidadDisponible;
                int cantidadDespachada;
                Medicamento medicamentoActual = new Medicamento();
                medicamentoActual = _clinicaRepository.GetMedicamento(medicamento.Id).Result;
                cantidadDisponible = medicamentoActual.Cantidad;
                cantidadDespachada = medicamento.Cantidad;

                if (cantidadDisponible == 0)
                {
                    ModelState.AddModelError(string.Empty, "Este medicamento no tiene cantidad disponible");
                    return View("Dispatch", medicamentoActual);
                }

                if (cantidadDisponible < cantidadDespachada)
                {
                    ModelState.AddModelError(string.Empty, $"La cantidad ingresada por despachar es mayor a la disponible, solo puede despachar maximo {cantidadDisponible}");
                    return View("Dispatch", medicamentoActual);
                }
                medicamento.Cantidad = cantidadDisponible - cantidadDespachada;

                medicamentoActual = _clinicaRepository.UpdateMedicamento(medicamento).Result;
                if (medicamentoActual == null) throw new Exception();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Se presentaron errores al despachar");
                return View("Dispatch", medicamento);
            }


        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                Medicamento medicamento = new Medicamento();
                medicamento = _clinicaRepository.GetMedicamento((int)id).Result;
                return View(medicamento);
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Se presentaron errores al recuperar el medicamento seleccionado");
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Medicamento medicamento)
        {
            try
            {
                if (!_clinicaRepository.DeleteMedicamento(medicamento.Id).Result)
                {
                    ModelState.AddModelError(string.Empty, "Se presentaron errores al borrar");
                    return View("Delete", medicamento);

                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Se presentaron errores al borrar");
                return View("Delete", medicamento);
            }


        }

    }
}
