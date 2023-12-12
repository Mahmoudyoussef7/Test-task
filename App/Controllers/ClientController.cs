using App.DTO;
using AutoMapper;
using DAL.Interfaces;
using EFL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;

namespace App.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ClientController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
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
            ViewBag.MaritalStatuses = _unitOfWork.MSRepository.GetAll().Select(ms => new SelectListItem { Value = ms.Id.ToString(), Text = ms.Status }).ToList();
            if (id == null)
            {
                return View(new ClientDTO());
            }
            else
            {
                var client = _unitOfWork.ClientRepository.GetById(id.Value);
                if (client == null)
                {
                    return NotFound();
                }
                var clientDto = _mapper.Map<ClientDTO>(client);
                return View(clientDto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(ClientDTO model)
        {
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }

                model.ImagePath = "/images/" + uniqueFileName;
            }

            var client = _mapper.Map<Client>(model);

            _ = (client.Id == 0) ? _unitOfWork.ClientRepository.Add(client) : _unitOfWork.ClientRepository.Update(client);
            var result = _unitOfWork.Complete();
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.MaritalStatuses = _unitOfWork.MSRepository.GetAll()
                    .Select(ms => new SelectListItem { Value = ms.Id.ToString(), Text = ms.Status }).ToList();
                return View(client);
            }
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
