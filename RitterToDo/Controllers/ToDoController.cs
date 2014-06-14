using System.Linq;
using RitterToDo.Core;
using RitterToDo.Repos;
using System;
using System.Web.Mvc;
using RitterToDo.Models;
using Moo;

namespace RitterToDo.Controllers
{
	public class ToDoController : Controller
	{
		public ToDoController(
			IRepository<ToDo> todoRepo,
			ILookupHelper<ToDoCategory, ToDoCategoryViewModel> categoryHelper,
			IMappingRepository mappingRepository)
		{
			ToDoRepo = todoRepo;
			CategoryHelper = categoryHelper;
			MappingRepository = mappingRepository;
		}

		public ActionResult Index()
		{
			var entities = ToDoRepo.GetAll();
			var mapper = MappingRepository.ResolveMapper<ToDo, ToDoViewModel>();
			var models = mapper.MapMultiple(entities);
			return View(models);
		}

		public IRepository<ToDo> ToDoRepo { get; private set; }
		
		public ILookupHelper<ToDoCategory, ToDoCategoryViewModel> CategoryHelper { get; private set; }

		public IMappingRepository MappingRepository { get; private set; }

		public ActionResult Edit(Guid id)
		{
			var mapper = MappingRepository.ResolveMapper<ToDo, ToDoEditViewModel>();
			var model = mapper.Map(ToDoRepo.GetById(id));
			ViewData["Categories"] = CategoryHelper.GetAll();
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(ToDoEditViewModel item)
		{
			var mapper = MappingRepository.ResolveMapper<ToDoEditViewModel, ToDo>();
			var entity = mapper.Map(item);
			ToDoRepo.Update(entity);
			return RedirectToAction("Index", "Todo");
		}

        public ActionResult Details(Guid id)
        {
            var entity = ToDoRepo.GetById(id);
            var mapper = MappingRepository.ResolveMapper<ToDo, ToDoViewModel>();
            var model = mapper.Map(entity);
            return View(model);
        }

		public ActionResult GetStarred()
		{
			var entities = ToDoRepo.GetAll().Where(x => x.Starred);
			var mapper = MappingRepository.ResolveMapper<ToDo, ToDoViewModel>();
			var models = mapper.MapMultiple(entities);
			return View("Index", models);
		}

	}
}