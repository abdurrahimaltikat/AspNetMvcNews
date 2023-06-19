﻿using App.Entities.Concrete;
using App.Entities.Dtos.NewsCommentDtos;
using App.Service.Abstract;
using App.Shared.Utilities.Results.ComplexTypes;
using App.Web.Mvc.Helpers.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class NewsCommentController : Controller
    {


        private readonly INewsCommentService _newsCommentService;
        private readonly UserManager<User> _userManager;
        private readonly IImageHelper _imageHelper;
        private readonly IMapper _mapper;

        public NewsCommentController(INewsCommentService newsCommentService, IImageHelper imageHelper, IMapper mapper, UserManager<User> userManager)
        {
            _newsCommentService = newsCommentService;
            _imageHelper = imageHelper;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _newsCommentService.GetAllAsync();
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int commentId)
        {
            await _newsCommentService.DeleteAsync(commentId, "");
            return View(nameof(Index));
        }

		public async Task<IActionResult> Approve(int commentId)
		{
			await _newsCommentService.ApproveAsync(commentId);
			return RedirectToAction("Index","NewsComment");
		}
		[HttpGet]
        public async Task<IActionResult> Edit(int commentId)
        {
            var result = await _newsCommentService.GetCommentUpdateDtoAsync(commentId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(NewsCommentUpdateDto commentUpdateDto)
        {
            var model = await _newsCommentService.UpdateAsync(commentUpdateDto, "");

            return View(model);
        }
		public async Task<IActionResult> Detail(int commentId)
		{
            var model = await _newsCommentService.GetAsync(commentId);

			return View(model);
		}


	}
}