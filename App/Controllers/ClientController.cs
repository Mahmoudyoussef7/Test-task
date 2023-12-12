using App.DTO;
using AutoMapper;
using DAL.Interfaces;
using EFL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllClients(int pageSize = 10, int pageNumber = 1, string search = null, int maritalStatus = 0, DateTime? birthDate = null)
        {
            var clients = _unitOfWork.ClientRepository.GetClients(pageSize, pageNumber, search, maritalStatus, birthDate).ToList();
            clients.ForEach(c =>
            {
                c.MaritalStatus = _unitOfWork.MSRepository.GetById(c.MaritalStatusId);
            });
            var result = _mapper.Map<List<ClientDTO>>(clients);
            return Json(result);
        }

        public IActionResult Details(int id)
        {
            var client = _unitOfWork.ClientRepository.GetById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        public IActionResult CreateOrEdit(int? id)
        {
            ViewBag.MaritalStatuses = _unitOfWork.MSRepository.GetAll().ToList();
            if (id == null)
            {
                return View(new Client());
            }
            else
            {
                var client = _unitOfWork.ClientRepository.GetById(id.Value);
                if (client == null)
                {
                    return NotFound();
                }
                return View(client);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(Client client)
        {
            if (ModelState.IsValid)
            {
               _ = (client.Id == 0) ? _unitOfWork.ClientRepository.Add(client) : _unitOfWork.ClientRepository.Update(client);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.MaritalStatuses = _unitOfWork.MSRepository.GetAll().ToList();
            return View(client);
        }

        public IActionResult Delete(int id)
        {
            var client = _unitOfWork.ClientRepository.GetById(id);
            if (client == null)
            {
                return NotFound();
            }
            _unitOfWork.Complete();
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteClient(int id)
        {
            var client = _unitOfWork.ClientRepository.GetById(id);
            if (client != null)
            {
                _unitOfWork.ClientRepository.Delete(client);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


    }
}
