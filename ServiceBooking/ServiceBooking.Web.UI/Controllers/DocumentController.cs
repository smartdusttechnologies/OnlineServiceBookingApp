﻿using Microsoft.AspNetCore.Mvc;

namespace ServiceBooking.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        //private readonly IDocumentService _documentService;

        //public DocumentController(IDocumentService documentService)
        //{
        //    _documentService = documentService;
        //}

        /// <summary>
        /// Method To Upload Document 
        /// </summary>
        [HttpPost]
        [Route("FileUpload")]
        public IActionResult FileUpload()
        {
            var uploadedFileIds = "";//documentService.UploadFiles(Request.Form.Files);
            return Ok(uploadedFileIds);
        }

        /// <summary>
        /// Method To download Document 
        /// </summary>
        [HttpGet]
        [Route("DownloadDocument/{documentID}")]
        public IActionResult DownloadDocument(int documentID)
        {

           // DocumentModel attachment = _documentService.DownloadDocument(documentID);

            //if (attachment != null)
            //{
            //    //return File(new MemoryStream(attachment.DataFiles), Helpers.GetMimeTypes()[attachment.FileType], attachment.Name);
            //}
            return Ok("Can't find the Document");
        }
    }
}
