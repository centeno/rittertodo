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
			IRepository<ToDoCategory> todoCategoryRepo,
			IMappingRepository mappingRepository)
		{
			ToDoRepo = todoRepo;
			TodoCategoryRepo = todoCategoryRepo;
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

		public IRepository<ToDoCategory> TodoCategoryRepo { get; private set; }

		public IMappingRepository MappingRepository { get; private set; }

		public ActionResult Edit(Guid id)
		{
			var entity = ToDoRepo.GetById(id);
			var mapper = MappingRepository.ResolveMapper<ToDo, ToDoEditViewModel>();
			var model = mapper.Map(entity);
			var catList = TodoCategoryRepo.GetAll();
			var catMapper = MappingRepository.ResolveMapper<ToDoCategory, ToDoCategoryViewModel>();
			var catModels = catMapper.MapMultiple(catList);
			ViewData["Categories"] = catModels;
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
	}
}