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
		public ToDoController(IRepository<ToDo> todoRepo, IMapper<ToDo, ToDoViewModel> entityMapper)
		{
            this.ToDoRepo = todoRepo;
            this.EntityMapper = entityMapper;
		}
		//
		// GET: /ToDo/
		public ActionResult Index(IPrincipal user)
		{
            var entities = ToDoRepo.GetAll(user);
			return View(EntityMapper.MapMultiple(entities));
		}

        public IRepository<ToDo> ToDoRepo { get; private set; }

        public IMapper<ToDo, ToDoViewModel> EntityMapper { get; private set; }
    }
}