﻿using Machine.Specifications;
using RitterToDo.Controllers;
using RitterToDo.IntegratedTests.TestHelpers;
using RitterToDo.Models;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentAssertions;

namespace RitterToDo.IntegratedTests
{
    [Subject(typeof(ToDoController), "To-do")]
    public class When_user_requests_his_to_do_list
    {
        private static ToDoController Sut;

        private static ActionResult Result;

        Establish context = () =>
            {
                var ctr = new Container();
                Sut = TestContainer.GetInstance<ToDoController>();
            };

        Because of = () => Result = Sut.Index();

        It should_be_a_view_result = () => Result.ShouldBeOfExactType<ViewResult>();

        It should_return_a_list_of_to_dos = () => 
            ((ViewResult)Result)
            .Model
            .ShouldBeAssignableTo<IEnumerable<ToDoViewModel>>();

        It should_return_all_data = () =>
            {
                var models = (IEnumerable<ToDoViewModel>)((ViewResult)Result).Model;

                models
                    .ShouldAllBeEquivalentTo(
                        DataHelper.DummyToDoViewModels
                        , opt => opt
                            .Excluding(e => e.Id)
                            .Excluding(e => e.DueDate)
                    );

            };
    }
}
