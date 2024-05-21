﻿using Contracts.Employee;
using Contracts.Position;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Mappers;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;

namespace InternshipProject.Controllers
{
    public class UserPositionController : Controller
    {
        private readonly IUserPositionService _userPositionService;
        public UserPositionController(IUserPositionService userPositionService)
        {
            _userPositionService = userPositionService;
        }

        [Route("AllUserPositions")]
        public async Task<IActionResult> AllUserPositions()
        {
            var users = await _userPositionService.GetAllUserPositionsAsync();

            return View(users);
        }

        [Route("CreateUserPosition")]
        public IActionResult CreateUserPosition()
        {
            return View();
        }
        [Route("CreateUserPosition")]
        [HttpPost]
        public async Task<IActionResult> CreateUserPosition(UserPositionCreateRequest createRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _userPositionService.CreateUserPositionAsync(createRequest);

                if (!result)
                {
                    ModelState.AddModelError("", "There was an error while creating the user position. Please try again!");

                    return View(createRequest);
                }
                ModelState.Clear();

                return RedirectToAction("AllUserPositions", "UserPosition");


            }
            return View();
        }


        [Route("UpdateUserPosition")]
        [HttpGet]
        public async Task<IActionResult> UpdateUserPosition(int id)
        {
            var position = await _userPositionService.GetUserPositionByIdAsync(id);

            var updateRequest = position.ToEmployeeUpdateRequest();

            return View(updateRequest);
        }


        [Route("UpdateUserPosition")]
        public IActionResult UpdateUserPosition()
        {
            return View();
        }

        [Route("UpdateUserPosition")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserPosition(UserPositionUpdateRequest updateRequest)
        {
            if (ModelState.IsValid)
            {

                var result = await _userPositionService.UpdateUserPositionAsync(updateRequest);

                if (!result)
                {
                    ModelState.AddModelError("", "There was an error while updating the user position. Please try again!");

                    return View(updateRequest);
                }

                ModelState.Clear();

                return RedirectToAction("AllUserPositions", "UserPosition");

            }
            return View();
        }

    }
}
