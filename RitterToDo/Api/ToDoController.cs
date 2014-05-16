﻿using RitterToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RitterToDo.Api
{
    public class ToDoController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<ToDoViewModel> Get()
        {
            return new ToDoViewModel[]
            {
                new ToDoViewModel() { CategoryName = "General", Description = "Dummy desc", Name = "Dummy name" },
            };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        /*
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
         * */
    }
}