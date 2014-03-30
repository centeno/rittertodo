using RitterToDo.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using RitterToDo.Models;
using Moo;

namespace RitterToDo.Controllers
{
	public class ToDoController : Controller
	{
		public ToDoController(
            IRepository<ToDo> todoRepo, 
            IMappingRepository mappingRepository)
		{
		    this.ToDoRepo = todoRepo;
		    MappingRepository = mappingRepository;
		}

	    //
		// GET: /ToDo/
		public ActionResult Index()
		{
            var entities = ToDoRepo.GetAll();
		    var mapper = MappingRepository.ResolveMapper<ToDo, ToDoViewModel>();
		    var models = mapper.MapMultiple(entities);
			return View(models);
		}

        public IRepository<ToDo> ToDoRepo { get; private set; }
	    public IMappingRepository MappingRepository { get; private set; }

        public ActionResult Update(Guid id)
        {
            var entity = ToDoRepo.GetById(id);
            var mapper = MappingRepository.ResolveMapper<ToDo, ToDoEditViewModel>();
            var model = mapper.Map(entity);
            return View(model);
        }
    }
}